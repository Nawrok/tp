using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL;
using Store.DAL.Model;

namespace Store.BLL
{
    public class DataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public Client AddClient(string name, string surname, string email, string city)
        {
            var client = new Client(name, surname, email, city);
            _dataRepository.AddClient(client);
            return client;
        }

        public Product AddProduct(Guid id, string name, string description, string type)
        {
            var product = new Product(id, name, description, type);
            _dataRepository.AddProduct(product);
            return product;
        }

        public Offer AddOffer(Guid productId, decimal netPrice, decimal tax, int productsInStock)
        {
            if (netPrice <= 0)
            {
                throw new ArgumentException("Net price must be positive value!");
            }

            if (tax < 0)
            {
                throw new ArgumentException("Tax must be non-negative value!");
            }

            if (productsInStock < 0)
            {
                throw new ArgumentException("Products in stock must be non-negative value!");
            }

            var curProduct = _dataRepository.GetProduct(productId);
            var offer = new Offer(curProduct, netPrice, tax, productsInStock);
            _dataRepository.AddOffer(offer);
            return offer;
        }

        public void DeleteClient(string email)
        {
            _dataRepository.DeleteClient(email);
        }

        public void DeleteProduct(Guid productId)
        {
            _dataRepository.DeleteProduct(productId);
        }

        public void DeleteOffer(Guid productId)
        {
            _dataRepository.DeleteOffer(productId);
        }

        public Facture BuyProducts(string email, Guid productId, int productCount)
        {
            var curClient = _dataRepository.GetClient(email);
            if (curClient == null)
            {
                throw new ArgumentException($"Client with email '{email}' does not exists!");
            }

            var curOffer = _dataRepository.GetOffer(productId);
            if (curOffer == null)
            {
                throw new ArgumentException($"Offer for this product with id '{productId}' does not exists!");
            }

            if (curOffer.ProductsInStock <= 0)
            {
                throw new InvalidOperationException("Currently, there are no products in stock, we are sorry!");
            }

            if (curOffer.ProductsInStock < productCount)
            {
                throw new InvalidOperationException("You want to buy more products than we have in stock!");
            }

            curOffer.ProductsInStock -= productCount;
            _dataRepository.UpdateOffer(productId, curOffer);

            var facture = new Facture(Guid.NewGuid(), curClient, curOffer, DateTimeOffset.Now, productCount);
            _dataRepository.AddEvent(facture);

            return facture;
        }

        public Return ReturnProducts(Guid factureId, int productCount)
        {
            if (!(_dataRepository.GetEvent(factureId) is Facture curFacture))
            {
                throw new ArgumentException($"Facture with id '{factureId}' does not exists!");
            }

            if (curFacture.Date > DateTimeOffset.Now)
            {
                throw new InvalidOperationException("You cannot return non-purchased products!");
            }

            if (GetActualClientProductsNumber(curFacture.Client.Email, curFacture.Offer.Product.Id) < productCount)
            {
                throw new InvalidOperationException("You want to return more products than you bought!");
            }

            var curProduct = _dataRepository.GetProduct(curFacture.Offer.Product.Id);
            if (curProduct == null)
            {
                _dataRepository.AddProduct(curFacture.Offer.Product);
                curProduct = _dataRepository.GetProduct(curFacture.Offer.Product.Id);
            }

            var curOffer = _dataRepository.GetOffer(curFacture.Offer.Product.Id);
            if (curOffer == null)
            {
                _dataRepository.AddOffer(curFacture.Offer);
                curOffer = _dataRepository.GetOffer(curFacture.Offer.Product.Id);
            }

            curOffer.ProductsInStock += productCount;
            _dataRepository.UpdateOffer(curProduct.Id, curOffer);

            var returned = new Return(Guid.NewGuid(), curFacture, DateTimeOffset.Now, productCount);
            _dataRepository.AddEvent(returned);

            return returned;
        }

        public void UpdateOfferState(Guid productId, int newProductsNumber)
        {
            if (newProductsNumber < 0)
            {
                throw new ArgumentException("Products in stock must be non-negative number!");
            }

            var offer = _dataRepository.GetOffer(productId);
            offer.ProductsInStock = newProductsNumber;
            _dataRepository.UpdateOffer(productId, offer);
        }

        public IEnumerable<Facture> GetFacturesForClient(string email)
        {
            return _dataRepository.GetAllFactures().Where(f => f.Client.Email.Equals(email));
        }

        public IEnumerable<Return> GetReturnsForClient(string email)
        {
            return _dataRepository.GetAllReturns().Where(r => r.Client.Email.Equals(email));
        }

        public IEnumerable<Facture> GetFacturesForProduct(Guid productId)
        {
            return _dataRepository.GetAllFactures().Where(f => f.Offer.Product.Id.Equals(productId));
        }

        public IEnumerable<Return> GetReturnsForProduct(Guid productId)
        {
            return _dataRepository.GetAllReturns().Where(r => r.Offer.Product.Id.Equals(productId));
        }

        public IEnumerable<Client> GetClientsForProduct(Guid productId)
        {
            var clientFactureEmails = GetFacturesForProduct(productId).Select(f => f.Client.Email).Distinct();
            var clientReturnEmails = GetReturnsForProduct(productId).Where(r => IsReturningAllProducts(r.Id)).Select(r => r.Client.Email).Distinct();
            var clientEmails = clientFactureEmails.Where(email => clientReturnEmails.All(e => email != e));
            return clientEmails.Select(email => _dataRepository.GetAllClients().First(c => c.Email.Equals(email)));
        }

        public IEnumerable<Product> GetBoughtProductsForClient(string email)
        {
            return GetFacturesForClient(email).Select(f => f.Offer.Product).Distinct();
        }

        public ValueTuple<int, decimal> GetProductSales(Guid productId)
        {
            var productCount = GetFacturesForProduct(productId).Sum(f => f.BoughtProducts) - GetReturnsForProduct(productId).Sum(r => r.ReturnedProducts);
            var totalSales = GetFacturesForProduct(productId).Sum(f => f.GrossPrice) - GetReturnsForProduct(productId).Sum(r => r.ReturnedPrice);
            return (productCount, totalSales);
        }

        public int GetActualClientProductsNumber(string email, Guid productId)
        {
            var bought = GetFacturesForClient(email).Where(f => f.Offer.Product.Id.Equals(productId)).Sum(f => f.BoughtProducts);
            var returned = GetReturnsForClient(email).Where(r => r.Offer.Product.Id.Equals(productId)).Sum(r => r.ReturnedProducts);
            return bought - returned;
        }
        
        private bool IsReturningAllProducts(Guid returnId)
        {
            var curReturn = (Return) _dataRepository.GetEvent(returnId);
            var curFacture = (Facture) _dataRepository.GetEvent(curReturn.FactureId);
            return curFacture.BoughtProducts == curReturn.ReturnedProducts;
        }
    }
}