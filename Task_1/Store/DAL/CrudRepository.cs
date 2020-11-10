using System;
using System.Collections.Generic;
using System.IO;
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
                throw new ArgumentException($"Client with email '{client.Email}' already exists!");
            }

            _dataContext.Clients.Add(client);
        }

        public void AddEvent(Event evt)
        {
            if (_dataContext.Events.Any(e => e.Id.Equals(evt.Id)))
            {
                throw new ArgumentException($"{evt.GetType().Name} with id '{evt.Id}' already exists!");
            }

            if (!IsReferringToClient(evt) || !IsReferringToOffer(evt))
            {
                throw new InvalidDataException("Event refers to client/offer that is not in repository!");
            }

            _dataContext.Events.Add(evt);
        }

        public void AddOffer(Offer offer)
        {
            if (_dataContext.Offers.Any(o => o.Product.Id.Equals(offer.Product.Id)))
            {
                throw new ArgumentException($"Offer for product '{offer.Product.Name}' already exists!");
            }

            if (!IsReferringToProduct(offer))
            {
                throw new InvalidDataException("Offer refers to product that is not in repository!");
            }

            _dataContext.Offers.Add(offer);
        }

        public void AddProduct(Product product)
        {
            if (_dataContext.Products.ContainsKey(product.Id))
            {
                throw new ArgumentException($"Product with id '${product.Id}' already exists!");
            }

            _dataContext.Products.Add(product.Id, product);
        }

        public void DeleteClient(string email)
        {
            var curClient = GetClient(email);
            if (curClient == null)
            {
                throw new ArgumentException($"Client with email '{email}' does not exist!");
            }

            _dataContext.Clients.Remove(curClient);
        }

        public void DeleteEvent(Guid eventId)
        {
            var curEvent = GetEvent(eventId);
            if (curEvent == null)
            {
                throw new ArgumentException($"Event with id '{eventId}' does not exist!");
            }

            _dataContext.Events.Remove(curEvent);
        }

        public void DeleteOffer(Guid productId)
        {
            var curOffer = GetOffer(productId);
            if (curOffer == null)
            {
                throw new ArgumentException($"Offer for product with id '{productId}' does not exist!");
            }

            _dataContext.Offers.Remove(curOffer);
        }

        public void DeleteProduct(Guid productId)
        {
            var curProduct = GetProduct(productId);
            if (curProduct == null)
            {
                throw new ArgumentException($"Product with id '{productId}' does not exist!");
            }

            if (IsReferringToOffer(curProduct))
            {
                throw new InvalidDataException($"Product with id '{productId}' refers to offer that is in repository!");
            }

            _dataContext.Products.Remove(productId);
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
                throw new ArgumentException($"Cannot change email '{email}' for client with email '{client.Email}'!");
            }

            var curClient = GetClient(email);
            if (curClient == null)
            {
                throw new ArgumentException($"Client with email '{email}' does not exists!");
            }

            var id = _dataContext.Clients.IndexOf(curClient);
            _dataContext.Clients[id] = client;
        }

        public void UpdateEvent(Guid eventId, Event evt)
        {
            if (!eventId.Equals(evt.Id))
            {
                throw new ArgumentException($"Cannot change id '{eventId}' for event with id '{evt.Id}'!");
            }

            if (!IsReferringToOffer(evt) || !IsReferringToClient(evt))
            {
                throw new InvalidDataException("Event refers to client/offer that is not in repository!");
            }

            var curEvent = GetEvent(eventId);
            if (curEvent == null)
            {
                throw new ArgumentException($"Event with id '{eventId}' does not exists!");
            }

            var id = _dataContext.Events.IndexOf(curEvent);
            _dataContext.Events[id] = evt;
        }

        public void UpdateOffer(Guid productId, Offer offer)
        {
            if (!productId.Equals(offer.Product.Id))
            {
                throw new ArgumentException($"Cannot change product id '{productId}' for this offer!");
            }

            if (!IsReferringToProduct(offer))
            {
                throw new InvalidDataException("Offer refers to product that is not in repository!");
            }

            var curOffer = GetOffer(productId);
            if (curOffer == null)
            {
                throw new ArgumentException($"Offer for this product with id '{productId}' does not exists!");
            }

            var id = _dataContext.Offers.IndexOf(curOffer);
            _dataContext.Offers[id] = offer;
        }

        public void UpdateProduct(Guid productId, Product product)
        {
            if (!productId.Equals(product.Id))
            {
                throw new ArgumentException($"Cannot change id '{productId}' for product with id '{product.Id}'!");
            }

            if (!_dataContext.Products.ContainsKey(productId))
            {
                throw new ArgumentException($"Product with id '{productId}' does not exist!");
            }

            _dataContext.Products[productId] = product;
        }

        private bool IsReferringToOffer(Event evt)
        {
            return _dataContext.Offers.Any(o => o.Product.Id.Equals(evt.Offer.Product.Id));
        }

        private bool IsReferringToClient(Event evt)
        {
            return _dataContext.Clients.Any(c => c.Email.Equals(evt.Client.Email));
        }

        private bool IsReferringToProduct(Offer offer)
        {
            return _dataContext.Products.ContainsKey(offer.Product.Id);
        }

        private bool IsReferringToOffer(Product product)
        {
            return _dataContext.Offers.Any(o => o.Product.Id.Equals(product.Id));
        }
    }
}