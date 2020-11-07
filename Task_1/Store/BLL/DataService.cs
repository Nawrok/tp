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
            _dataRepository = dataRepository;
        }

        public Client RegisterClient(string name, string surname, string email, string city)
        {
            var client = _dataRepository.GetClient(email) ?? new Client(name, surname, email, city);
            return client;
        }

        public Facture BuyProducts(Client client, Offer offer, int productCount)
        {
            var curClient = _dataRepository.GetClient(client.Email);
            if (curClient == null)
            {
                throw new ArgumentException("Client is unregistered!");
            }

            var curOffer = _dataRepository.GetOffer(offer.Product.Id);
            if (curOffer == null)
            {
                throw new ArgumentException("No offer is available for this product!");
            }

            if (curOffer.ProductsInStock == 0)
            {
                throw new InvalidOperationException("Currently, there are no products in stock, we are sorry!");
            }

            if (curOffer.ProductsInStock < productCount)
            {
                throw new InvalidOperationException("You want to buy more products than we have in stock!");
            }

            curOffer.ProductsInStock -= productCount;
            _dataRepository.UpdateOffer(offer.Product.Id, curOffer);

            var facture = new Facture(Guid.NewGuid(), client, curOffer, productCount, DateTimeOffset.Now);
            _dataRepository.AddEvent(facture);

            return facture;
        }

        public Return ReturnProducts(Facture facture, int productCount)
        {
            var curFacture = _dataRepository.GetEvent(facture.Id);
            if (curFacture == null)
            {
                throw new ArgumentException("You cannot return products with not existing facture!");
            }

            if (facture.ProductCount < productCount)
            {
                throw new InvalidOperationException("You want to return more products than you bought!");
            }

            var returned = curFacture as Return;
            if (returned != null)
            {
                returned.ReturnDate = DateTimeOffset.Now;
                _dataRepository.UpdateEvent(returned.Id, returned);
            }

            var curOffer = curFacture.Offer;
            curOffer.ProductsInStock += productCount;
            _dataRepository.UpdateOffer(facture.Offer.Product.Id, facture.Offer);

            return returned;
        }

        public void UpdateOfferState(Product product, int productsInStock)
        {
            var offer = _dataRepository.GetOffer(product.Id);
            if (offer == null)
            {
                throw new ArgumentException("No offer is available for this product!");
            }

            offer.ProductsInStock = productsInStock;

            _dataRepository.UpdateOffer(offer.Product.Id, offer);
        }

        public IEnumerable<Facture> GetFacturesForClient(Client client)
        {
            return _dataRepository.GetFactures().Where(f => f.Client.Email.Equals(client.Email));
        }

        public IEnumerable<Return> GetReturnsForClient(Client client)
        {
            return _dataRepository.GetReturns().Where(f => f.Client.Email.Equals(client.Email));
        }

        public IEnumerable<Facture> GetFacturesForProduct(Product product)
        {
            return _dataRepository.GetFactures().Where(f => f.Offer.Product.Id.Equals(product.Id));
        }

        public IEnumerable<Client> GetClientsForProduct(Product product)
        {
            var clientEmails = GetFacturesForProduct(product).Select(f => f.Client.Email).Distinct();
            return clientEmails.Select(email => _dataRepository.GetAllClients().First(c => c.Email.Equals(email)));
        }

        public IEnumerable<Product> GetBoughtProductsForClient(Client client)
        {
            return GetFacturesForClient(client).Select(f => f.Offer.Product);
        }

        public ValueTuple<int, decimal> GetProductSales(Product product)
        {
            var productCount = GetFacturesForProduct(product).Sum(f => f.ProductCount);
            var totalSales = GetFacturesForProduct(product).Sum(f => f.GrossPrice);
            return (productCount, totalSales);
        }

        //TODO RESEARCH ABOUT DELEGATING MEMBERS TO REPOSITORY
    }
}