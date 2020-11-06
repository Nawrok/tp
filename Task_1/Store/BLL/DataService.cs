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

        public Facture BuyProducts(Client client, Offer offer, int productCount)
        {
            if (offer.ProductsInStock == 0)
            {
                throw new InvalidOperationException("Currently, there is no products in stock, we are sorry!");
            }

            if (offer.ProductsInStock - productCount < 0)
            {
                //TODO MESSAGE - CANNOT BUY MORE PRODUCTS THAN IN STOCK
                throw new InvalidOperationException();
            }

            var facture = new Facture(Guid.NewGuid(), client, offer, productCount, DateTimeOffset.Now);

            offer.ProductsInStock -= productCount;
            _dataRepository.UpdateOffer(offer.Id, offer);
            _dataRepository.AddEvent(facture);

            return facture;
        }

        public void UpdateOfferState(Product product, int productsInStock)
        {
            var offer = GetOffers().FirstOrDefault(o => o.Product.Id.Equals(product.Id));
            if (offer == null)
            {
                //TODO MESSAGE - NO OFFER FOR THIS PRODUCT
                throw new ArgumentException();
            }

            offer.ProductsInStock = productsInStock;

            _dataRepository.UpdateOffer(offer.Id, offer);
        }

        public IEnumerable<Event> GetFacturesForClient(Client client)
        {
            return GetFactures().Where(f => f.Client.Email.Equals(client.Email));
        }

        public IEnumerable<Client> GetClientsFromCity(string city)
        {
            return GetClients().Where(c => c.City.Equals(city));
        }

        public IEnumerable<Product> GetBoughtProducts(Client client)
        {
            return GetFacturesForClient(client).Select(f => f.Offer.Product);
        }

        public IEnumerable<Product> GetTheSameTypeProducts(string type)
        {
            return GetProducts().Where(p => p.Type.Equals(type));
        }

        public IEnumerable<Facture> GetFacturesForProduct(Product product)
        {
            return GetFactures().Where(f => f.Offer.Product.Id.Equals(product.Id));
        }

        public IEnumerable<Event> GetFacturesInTime(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return GetFactures().Where(f => f.PurchaseDate >= startDate && f.PurchaseDate <= endDate);
        }

        public IEnumerable<Client> GetClientsForProduct(Product product)
        {
            var clientEmails = GetFacturesForProduct(product).Select(f => f.Client.Email).Distinct();
            return clientEmails.Select(email => GetClients().First(c => c.Email.Equals(email)));
        }

        public ValueTuple<int, decimal> GetProductSales(Product product)
        {
            var productCount = GetFacturesForProduct(product).Sum(f => f.ProductCount);
            var totalSales = GetFacturesForProduct(product).Sum(f => f.GrossPrice);
            return (productCount, totalSales);
        }

        public void AddClient(Client client)
        {
            _dataRepository.AddClient(client);
        }

        public void AddOffer(Offer offer)
        {
            _dataRepository.AddOffer(offer);
        }

        public void AddProduct(Product product)
        {
            _dataRepository.AddProduct(product);
        }

        public void DeleteEvent(Event evt)
        {
            _dataRepository.DeleteEvent(evt);
        }

        public void DeleteClient(Client client)
        {
            _dataRepository.DeleteClient(client);
        }

        public void DeleteOffer(Offer offer)
        {
            _dataRepository.DeleteOffer(offer);
        }

        public void DeleteProduct(Product product)
        {
            _dataRepository.DeleteProduct(product);
        }

        public IEnumerable<Client> GetClients()
        {
            return _dataRepository.GetAllClients();
        }

        public IEnumerable<Facture> GetFactures()
        {
            return _dataRepository.GetAllEvents().Where(e => e.GetType().IsInstanceOfType(typeof(Facture))).Cast<Facture>();
        }

        public IEnumerable<Return> GetReturns()
        {
            return _dataRepository.GetAllEvents().Where(e => e.GetType().IsInstanceOfType(typeof(Return))).Cast<Return>();
        }

        public IEnumerable<Event> GetEvents()
        {
            return _dataRepository.GetAllEvents();
        }

        public IEnumerable<Offer> GetOffers()
        {
            return _dataRepository.GetAllOffers();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dataRepository.GetAllProducts();
        }

        public void UpdateEvent(Guid eventId, Event evt)
        {
            _dataRepository.UpdateEvent(eventId, evt);
        }

        public void UpdateClient(string email, Client client)
        {
            _dataRepository.UpdateClient(email, client);
        }

        public void UpdateOffer(Guid offerId, Offer offer)
        {
            _dataRepository.UpdateOffer(offerId, offer);
        }

        public void UpdateProduct(Guid productId, Product product)
        {
            _dataRepository.UpdateProduct(productId, product);
        }
    }
}