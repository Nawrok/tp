using Store.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public interface IDataRepository
    {
        void AddClient(Client client);
        void AddProduct(Product product);
        void AddOffer(Offer offer);
        void AddFacture(Facture facture);
        Client GetClient(string email);
        Product GetProduct(Guid productId);
        Offer GetOffer(Guid offerId);
        Facture GetFacture(Guid factureId);
        IEnumerable<Client> GetClients();
        IEnumerable<Product> GetProducts();
        IEnumerable<Offer> GetOffers();
        IEnumerable<Facture> GetFactures();
        void UpdateClient(string email, Client client);
        void UpdateProduct(Guid productId, Product product);
        void UpdateOffer(Guid offerId, Offer offer);
        void UpdateFacture(Guid factureId, Facture facture);
        void DeleteClient(Client client);
        void DeleteProduct(Product product);
        void DeleteOffer(Offer offer);
        void DeleteFacture(Facture facture);
    }
}
