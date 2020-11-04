using Store.Entities;
using System;
using System.Collections.Generic;

namespace Store
{
    public class DataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void AddClient(Client client) => _dataRepository.AddClient(client);
        public void AddOffer(Offer offer) => _dataRepository.AddOffer(offer);
        public void AddProduct(Product product) => _dataRepository.AddProduct(product);
        public void DeleteInvoice(Facture facture) => _dataRepository.DeleteFacture(facture);
        public void DeleteClient(Client client) => _dataRepository.DeleteClient(client);
        public void DeleteOffer(Offer offer) => _dataRepository.DeleteOffer(offer);
        public void DeleteProduct(Product product) => _dataRepository.DeleteProduct(product);
        public IEnumerable<Client> GetClients() => _dataRepository.GetAllClients();
        public IEnumerable<Facture> GetFactures() => _dataRepository.GetAllFactures();
        public IEnumerable<Offer> GetOffers() => _dataRepository.GetAllOffers();
        public IEnumerable<Product> GetProducts() => _dataRepository.GetAllProducts();
        public void UpdateInvoice(Guid factureId, Facture facture) => _dataRepository.UpdateFacture(factureId, facture);
        public void UpdateClient(string email, Client client) => _dataRepository.UpdateClient(email, client);
        public void UpdateOffer(Guid offerId, Offer offer) => _dataRepository.UpdateOffer(offerId, offer);
        public void UpdateProduct(Guid productId, Product product) => _dataRepository.UpdateProduct(productId, product);
    }
}
