using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL.Model;

namespace Store.DAL
{
    public class CrudRepository : ICrudRepository
    {
        private readonly DataContext _dataContext = new DataContext();

        protected CrudRepository(IDataFiller dataFiller)
        {
            dataFiller?.Fill(this);
        }

        public void AddClient(Client client)
        {
            if (_dataContext.Clients.Any(c => c.Email.Equals(client.Email)))
            {
                throw new ArgumentException($"Client '{client.Email}' already exists!");
            }

            _dataContext.Clients.Add(client);
        }

        public void AddEvent(Event evt)
        {
            if (_dataContext.Events.Any(e => e.Id.Equals(evt.Id)))
            {
                throw new ArgumentException($"{evt.GetType().Name} '{evt.Id}' already exists!");
            }

            _dataContext.Events.Add(evt);
        }

        public void AddOffer(Offer offer)
        {
            if (_dataContext.Offers.Any(o => o.Product.Id.Equals(offer.Product.Id)))
            {
                throw new ArgumentException($"Offer for product '{offer.Product.Name}' already exists!");
            }

            _dataContext.Offers.Add(offer);
        }

        public void AddProduct(Product product)
        {
            if (_dataContext.Products.ContainsKey(product.Id))
            {
                throw new ArgumentException($"Product '${product.Id}' already exists!");
            }

            _dataContext.Products.Add(product.Id, product);
        }

        public void DeleteClient(Client client)
        {
            if (!_dataContext.Clients.Remove(client))
            {
                throw new ArgumentException($"Client '{client.Email}' does not exist!");
            }
        }

        public void DeleteEvent(Event evt)
        {
            if (!_dataContext.Events.Remove(evt))
            {
                throw new ArgumentException($"{evt.GetType().Name} '{evt.Id}' does not exist!");
            }
        }

        public void DeleteOffer(Offer offer)
        {
            if (!_dataContext.Offers.Remove(offer))
            {
                throw new ArgumentException($"Offer for product '{offer.Product.Name}' does not exist!");
            }
        }

        public void DeleteProduct(Product product)
        {
            if (!_dataContext.Products.Remove(product.Id))
            {
                throw new ArgumentException($"Product '{product.Id}' does not exist!");
            }
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _dataContext.Clients;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _dataContext.Events;
        }

        public IEnumerable<Offer> GetAllOffers()
        {
            return _dataContext.Offers;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dataContext.Products.Values;
        }

        public Client GetClient(string email)
        {
            return _dataContext.Clients.FirstOrDefault(c => c.Email.Equals(email));
        }

        public Event GetEvent(Guid eventId)
        {
            return _dataContext.Events.FirstOrDefault(e => e.Id.Equals(eventId));
        }

        public Offer GetOffer(Guid productId)
        {
            return _dataContext.Offers.FirstOrDefault(o => o.Product.Id.Equals(productId));
        }

        public Product GetProduct(Guid productId)
        {
            return _dataContext.Products.FirstOrDefault(p => p.Key.Equals(productId)).Value;
        }

        public void UpdateClient(string email, Client client)
        {
            if (!email.Equals(client.Email))
            {
                throw new ArgumentException($"Cannot change email '{email}' for client '{client.Email}'!");
            }

            var id = _dataContext.Clients.FindIndex(c => c.Email.Equals(email));
            if (id == -1)
            {
                throw new ArgumentException($"Client '{email}' does not exist!");
            }

            _dataContext.Clients[id] = client;
        }

        public void UpdateEvent(Guid eventId, Event evt)
        {
            var curEvent = _dataContext.Events.FirstOrDefault(e => e.Id.Equals(eventId));
            if (curEvent == null)
            {
                throw new ArgumentException($"{evt.GetType().Name} '{eventId}' does not exist!");
            }

            var id = _dataContext.Events.IndexOf(curEvent);
            _dataContext.Events[id] = evt;
        }

        public void UpdateOffer(Guid productId, Offer offer)
        {
            var id = _dataContext.Offers.FindIndex(o => o.Product.Id.Equals(productId));
            if (id == -1)
            {
                throw new ArgumentException($"Offer for product '{offer.Product.Name}' does not exist!");
            }

            _dataContext.Offers[id] = offer;
        }

        public void UpdateProduct(Guid productId, Product product)
        {
            if (!productId.Equals(product.Id))
            {
                throw new ArgumentException($"Cannot change id '{productId}' for product '{product.Id}'!");
            }

            if (!_dataContext.Products.ContainsKey(productId))
            {
                throw new ArgumentException($"Product '{productId}' does not exist!");
            }

            _dataContext.Products[productId] = product;
        }
    }
}