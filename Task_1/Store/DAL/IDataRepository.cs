using System;
using System.Collections.Generic;
using Store.DAL.Model;

namespace Store.DAL
{
    public interface IDataRepository
    {
        void AddClient(Client client);
        void AddFacture(Facture facture);
        void AddOffer(Offer offer);
        void AddProduct(Product product);
        void DeleteClient(Client client);
        void DeleteFacture(Facture facture);
        void DeleteOffer(Offer offer);
        void DeleteProduct(Product product);
        IEnumerable<Client> GetAllClients();
        IEnumerable<Facture> GetAllFactures();
        IEnumerable<Offer> GetAllOffers();
        IEnumerable<Product> GetAllProducts();
        Client GetClient(string email);
        Facture GetFacture(Guid factureId);
        Offer GetOffer(Guid offerId);
        Product GetProduct(Guid productId);
        void UpdateClient(string email, Client client);
        void UpdateFacture(Guid factureId, Facture facture);
        void UpdateOffer(Guid offerId, Offer offer);
        void UpdateProduct(Guid productId, Product product);
    }
}