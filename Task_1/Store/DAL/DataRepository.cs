using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL.Model;

namespace Store.DAL
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _dataContext = new DataContext();

        public DataRepository(IDataFiller dataFiller)
        {
            dataFiller.Fill(this);
        }

        public void AddClient(Client client)
        {
            if (_dataContext.Clients.Any(c => c.Email.Equals(client.Email)))
            {
                throw new ArgumentException($"Client '{client.Email}' already exists!");
            }

            _dataContext.Clients.Add(client);
        }

        public void AddFacture(Facture facture)
        {
            if (_dataContext.Factures.Any(f => f.Id.Equals(facture.Id)))
            {
                throw new ArgumentException($"Facture '{facture.Id}' already exists!");
            }

            _dataContext.Factures.Add(facture);
        }

        public void AddOffer(Offer offer)
        {
            if (_dataContext.Offers.Any(o => o.Id.Equals(offer.Id)))
            {
                throw new ArgumentException($"Offer '{offer.Id}' already exists!");
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

        public void DeleteFacture(Facture facture)
        {
            if (!_dataContext.Factures.Remove(facture))
            {
                throw new ArgumentException($"Facture '{facture.Id}' does not exist!");
            }
        }

        public void DeleteOffer(Offer offer)
        {
            if (!_dataContext.Offers.Remove(offer))
            {
                throw new ArgumentException($"Offer '{offer.Id}' does not exist!");
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

        public IEnumerable<Facture> GetAllFactures()
        {
            return _dataContext.Factures;
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

        public Facture GetFacture(Guid factureId)
        {
            return _dataContext.Factures.FirstOrDefault(f => f.Id.Equals(factureId));
        }

        public Offer GetOffer(Guid offerId)
        {
            return _dataContext.Offers.FirstOrDefault(o => o.Id.Equals(offerId));
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

        public void UpdateFacture(Guid factureId, Facture facture)
        {
            var curFacture = _dataContext.Factures.FirstOrDefault(f => f.Id.Equals(facture.Id));
            if (curFacture == null)
            {
                throw new ArgumentException($"Facture '{factureId}' does not exist!");
            }

            var id = _dataContext.Factures.IndexOf(curFacture);
            _dataContext.Factures[id] = facture;
        }

        public void UpdateOffer(Guid offerId, Offer offer)
        {
            var id = _dataContext.Offers.FindIndex(o => o.Id.Equals(offer.Id));
            if (id == -1)
            {
                throw new ArgumentException($"Offer '{offerId}' does not exist!");
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