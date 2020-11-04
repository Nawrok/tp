using Store.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public class DataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public Facture BuyProduct(Client client, Offer offer)
        {
            Facture facture = new Facture(Guid.NewGuid(), client, offer, DateTimeOffset.Now);
            if(offer.Count <= 1)
            {
                throw new InvalidOperationException("Currently, there is no product in stock, we are sorry!");
            }

            offer.Count -= 1;
            _dataRepository.UpdateOffer(offer.Id, offer);
            _dataRepository.AddFacture(facture);

            return facture;
        }

        public void UpdateOfferState(Product product, int count)
        {
            Offer offer = GetOffers().FirstOrDefault(o => o.Product.Id.Equals(product.Id));
            offer.Count = count;

            _dataRepository.UpdateOffer(offer.Id, offer);
        }

        public IEnumerable<Facture> GetFacturesForClient(Client client)
        {
            IEnumerable<Facture> factures = GetFactures();

            return factures.Where(f => f.Client.Email == client.Email);
        }

        public IEnumerable<Client> GetClientsFromCity(string city)
        {
            return GetClients().Where(c => c.Address.City.Equals(city));
        }

        public IEnumerable<Product> GetBoughtProducts(Client client)
        {
            IEnumerable<Facture> factures = GetFacturesForClient(client);
            IEnumerable<Product> products = factures.Select(f => f.Offer.Product);

            return products;
        }

        public IEnumerable<Product> GetTheSameTypeProducts(string type)
        {
            return GetProducts().Where(p => p.Type.Equals(type));
        }

        public IEnumerable<Facture> GetFacturesForProduct(Product product)
        {
            return GetFactures().Where(f => f.Offer.Product.Id.Equals(product.Id));
        }

        public IEnumerable<Facture> GetFacturesInTime(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return GetFactures().Where(f => f.PurchaseDate >= startDate && f.PurchaseDate <= endDate);
        }

        public IEnumerable<Client> GetClientsForProduct(Product product)
        {
            IEnumerable<Facture> factures = GetFacturesForProduct(product);
            Dictionary<string, Client> clients = new Dictionary<string, Client>();
            foreach(Facture f in factures)
            {
                clients[f.Client.Email] = f.Client;
            }

            return clients.Values;
        }

        public ValueTuple<int, decimal> GetProductSales(Product product)
        {
            IEnumerable<Facture> productFactures = GetFactures().Where(f => f.Offer.Product.Id.Equals(product.Id));
            int count = productFactures.Count();
            decimal summedUpValue = productFactures.Sum(f => f.Offer.NetPrice);
            
            return (count, summedUpValue);
        }

        public void AddClient(Client client) => _dataRepository.AddClient(client);
        public void AddOffer(Offer offer) => _dataRepository.AddOffer(offer);
        public void AddProduct(Product product) => _dataRepository.AddProduct(product);
        public void DeleteFacture(Facture facture) => _dataRepository.DeleteFacture(facture);
        public void DeleteClient(Client client) => _dataRepository.DeleteClient(client);
        public void DeleteOffer(Offer offer) => _dataRepository.DeleteOffer(offer);
        public void DeleteProduct(Product product) => _dataRepository.DeleteProduct(product);
        public IEnumerable<Client> GetClients() => _dataRepository.GetAllClients();
        public IEnumerable<Facture> GetFactures() => _dataRepository.GetAllFactures();
        public IEnumerable<Offer> GetOffers() => _dataRepository.GetAllOffers();
        public IEnumerable<Product> GetProducts() => _dataRepository.GetAllProducts();
        public void UpdateFacture(Guid factureId, Facture facture) => _dataRepository.UpdateFacture(factureId, facture);
        public void UpdateClient(string email, Client client) => _dataRepository.UpdateClient(email, client);
        public void UpdateOffer(Guid offerId, Offer offer) => _dataRepository.UpdateOffer(offerId, offer);
        public void UpdateProduct(Guid productId, Product product) => _dataRepository.UpdateProduct(productId, product);
    }
}
