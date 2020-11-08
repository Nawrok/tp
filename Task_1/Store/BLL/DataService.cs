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
            if (netPrice <= 0 || tax < 0 || productsInStock < 0)
            {
                throw new ArgumentException("Price, tax and products in stock must be positive numbers!");
            }

            var curProduct = _dataRepository.GetProduct(productId);
            var offer = new Offer(curProduct, netPrice, tax, productsInStock);
            _dataRepository.AddOffer(offer);
            return offer;
        }

        public void DeleteProduct(Guid productId)
        {
            var curProduct = _dataRepository.GetProduct(productId);
            _dataRepository.DeleteProduct(curProduct);
        }

        public void DeleteOffer(Guid productId)
        {
            var curOffer = _dataRepository.GetOffer(productId);
            _dataRepository.DeleteOffer(curOffer);
        }

        public Facture BuyProducts(Client client, Guid productId, int productCount)
        {
            var curOffer = _dataRepository.GetOffer(productId);
            if (curOffer.ProductsInStock == 0)
            {
                throw new InvalidOperationException("Currently, there are no products in stock, we are sorry!");
            }

            if (curOffer.ProductsInStock < productCount)
            {
                throw new InvalidOperationException("You want to buy more products than we have in stock!");
            }

            curOffer.ProductsInStock -= productCount;

            var curClient = _dataRepository.GetClient(client.Email);
            var facture = new Facture(Guid.NewGuid(), curClient, curOffer, productCount, DateTimeOffset.Now);

            _dataRepository.UpdateOffer(productId, curOffer);
            _dataRepository.AddEvent(facture);

            return facture;
        }

        public Return ReturnProducts(Facture facture, int productCount)
        {
            if (facture.ProductCount < productCount)
            {
                throw new InvalidOperationException("You want to return more products than you bought!");
            }

            var curFacture = _dataRepository.GetEvent(facture.Id);
            var returned = new Return(curFacture as Facture, DateTimeOffset.Now);
            _dataRepository.UpdateEvent(returned.Id, returned);

            var curOffer = curFacture.Offer;
            curOffer.ProductsInStock += productCount;
            _dataRepository.UpdateOffer(curFacture.Offer.Product.Id, curOffer);

            return returned;
        }

        public void UpdateOfferState(Guid productId, int productsInStock)
        {
            if (productsInStock < 0)
            {
                throw new ArgumentException("Products in stock must be positive number!");
            }

            var offer = _dataRepository.GetOffer(productId);
            offer.ProductsInStock = productsInStock;
            _dataRepository.UpdateOffer(productId, offer);
        }

        public IEnumerable<Facture> GetFacturesForClient(string email)
        {
            return _dataRepository.GetAllFactures().Where(f => f.Client.Email.Equals(email));
        }

        public IEnumerable<Return> GetReturnsForClient(string email)
        {
            return _dataRepository.GetAllReturns().Where(f => f.Client.Email.Equals(email));
        }

        public IEnumerable<Facture> GetFacturesForProduct(Guid productId)
        {
            return _dataRepository.GetAllFactures().Where(f => f.Offer.Product.Id.Equals(productId));
        }

        public IEnumerable<Client> GetClientsForProduct(Guid productId)
        {
            var clientEmails = GetFacturesForProduct(productId).Select(f => f.Client.Email).Distinct();
            return clientEmails.Select(email => _dataRepository.GetAllClients().First(c => c.Email.Equals(email)));
        }

        public IEnumerable<Product> GetBoughtProductsForClient(string email)
        {
            return GetFacturesForClient(email).Select(f => f.Offer.Product);
        }

        public ValueTuple<int, decimal> GetProductSales(Guid productId)
        {
            var productCount = GetFacturesForProduct(productId).Sum(f => f.ProductCount);
            var totalSales = GetFacturesForProduct(productId).Sum(f => f.GrossPrice);
            return (productCount, totalSales);
        }
    }
}