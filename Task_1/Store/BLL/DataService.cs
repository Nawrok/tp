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

        public Event BuyProduct(Client client, Offer offer)
        {
            var facture = new Facture(Guid.NewGuid(), client, offer, DateTimeOffset.Now);
            if (offer.Count <= 1)
            {
                throw new InvalidOperationException("Currently, there is no product in stock, we are sorry!");
            }

            offer.Count -= 1;
            _dataRepository.UpdateOffer(offer.Id, offer);
            _dataRepository.AddEvent(facture);

            return facture;
        }

        public void UpdateOfferState(Product product, int count)
        {
            var offer = GetOffers().FirstOrDefault(o => o.Product.Id.Equals(product.Id));
            offer.Count = count;

            _dataRepository.UpdateOffer(offer.Id, offer);
        }

        public IEnumerable<Event> GetFacturesForClient(Client client)
        {
            var factures = GetFactures();

            return factures.Where(f => f.Client.Email == client.Email);
        }

        public IEnumerable<Client> GetClientsFromCity(string city)
        {
            return GetClients().Where(c => c.City.Equals(city));
        }

        public IEnumerable<Product> GetBoughtProducts(Client client)
        {
            var factures = GetFacturesForClient(client);
            var products = factures.Select(f => f.Offer.Product);

            return products;
        }

        public IEnumerable<Product> GetTheSameTypeProducts(string type)
        {
            return GetProducts().Where(p => p.Type.Equals(type));
        }

        public IEnumerable<Event> GetFacturesForProduct(Product product)
        {
            return GetFactures().Where(f => f.Offer.Product.Id.Equals(product.Id));
        }

        public IEnumerable<Event> GetFacturesInTime(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return GetFactures().Where(f => f.PurchaseDate >= startDate && f.PurchaseDate <= endDate);
        }

        public IEnumerable<Client> GetClientsForProduct(Product product)
        {
            var factures = GetFacturesForProduct(product);
            var clients = new Dictionary<string, Client>();
            foreach (var f in factures)
            {
                clients[f.Client.Email] = f.Client;
            }

            return clients.Values;
        }

        public ValueTuple<int, decimal> GetProductSales(Product product)
        {
            var productFactures = GetFactures().Where(f => f.Offer.Product.Id.Equals(product.Id));
            var count = productFactures.Count();
            var summedUpValue = productFactures.Sum(f => f.Offer.NetPrice);

            return (count, summedUpValue);
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

        public void DeleteFacture(Event facture)
        {
            _dataRepository.DeleteEvent(facture);
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

        public IEnumerable<Event> GetFactures()
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

        public void UpdateFacture(Guid factureId, Event facture)
        {
            _dataRepository.UpdateEvent(factureId, facture);
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