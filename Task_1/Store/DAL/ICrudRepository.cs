using System;
using System.Collections.Generic;
using Store.DAL.Model;

namespace Store.DAL
{
    public interface ICrudRepository
    {
        void AddClient(Client client);
        void AddEvent(Event evt);
        void AddOffer(Offer offer);
        void AddProduct(Product product);
        void DeleteClient(string email);
        void DeleteEvent(Guid eventId);
        void DeleteOffer(Guid productId);
        void DeleteProduct(Guid productId);
        IEnumerable<Client> GetAllClients();
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Offer> GetAllOffers();
        IEnumerable<Product> GetAllProducts();
        Client GetClient(string email);
        Event GetEvent(Guid eventId);
        Offer GetOffer(Guid productId);
        Product GetProduct(Guid productId);
        void UpdateClient(string email, Client client);
        void UpdateEvent(Guid eventId, Event evt);
        void UpdateOffer(Guid productId, Offer offer);
        void UpdateProduct(Guid productId, Product product);
    }
}