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
                throw new ArgumentException($"{evt.GetType().Name} '{evt.Id}' already exists!");
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

        public void DeleteClient(Client client)
        {
            if (!_dataContext.Clients.Remove(client))
            {
                throw new ArgumentException($"Client with email '{client.Email}' does not exist!");
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
            if (IsReferringToOffer(product))
            {
                throw new InvalidDataException("Product refers to offer that is in repository!");
            }

            if (!_dataContext.Products.Remove(product.Id))
            {
                throw new ArgumentException($"Product with id '{product.Id}' does not exist!");
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
            var curClient = _dataContext.Clients.FirstOrDefault(c => c.Email.Equals(email));
            if (curClient == null)
            {
                throw new ArgumentException($"Client with email '{email}' does not exist!");
            }

            return curClient;
        }

        public Event GetEvent(Guid eventId)
        {
            var curEvent = _dataContext.Events.FirstOrDefault(e => e.Id.Equals(eventId));
            if (curEvent == null)
            {
                throw new ArgumentException($"Event with id '{eventId}' does not exist!");
            }

            return curEvent;
        }

        public Offer GetOffer(Guid productId)
        {
            var curOffer = _dataContext.Offers.FirstOrDefault(o => o.Product.Id.Equals(productId));
            if (curOffer == null)
            {
                throw new ArgumentException($"Offer with product id '{productId}' does not exist!");
            }

            return curOffer;
        }

        public Product GetProduct(Guid productId)
        {
            var curProduct = _dataContext.Products.FirstOrDefault(p => p.Key.Equals(productId)).Value;
            if (curProduct == null)
            {
                throw new ArgumentException($"Product with id '{productId}' does not exist!");
            }

            return curProduct;
        }

        public void UpdateClient(string email, Client client)
        {
            if (!email.Equals(client.Email))
            {
                throw new ArgumentException($"Cannot change email '{email}' for client '{client.Email}'!");
            }

            var id = _dataContext.Clients.IndexOf(GetClient(email));
            _dataContext.Clients[id] = client;
        }

        public void UpdateEvent(Guid eventId, Event evt)
        {
            if (!eventId.Equals(evt.Id))
            {
                throw new ArgumentException($"Cannot change id '{eventId}' for event '{evt.Id}'!");
            }

            if (!IsReferringToOffer(evt) || !IsReferringToClient(evt))
            {
                throw new InvalidDataException("Event refers to client/offer that is not in repository!");
            }

            var curEvent = GetEvent(eventId);
            var id = _dataContext.Events.IndexOf(curEvent);
            _dataContext.Events[id] = evt;
        }

        public void UpdateOffer(Guid productId, Offer offer)
        {
            if (!productId.Equals(offer.Product.Id))
            {
                throw new ArgumentException($"Cannot change id '{productId}' for this offer!");
            }

            if (!IsReferringToProduct(offer))
            {
                throw new InvalidDataException("Offer refers to product that is not in repository!");
            }

            var curOffer = GetOffer(productId);
            var id = _dataContext.Offers.IndexOf(curOffer);
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