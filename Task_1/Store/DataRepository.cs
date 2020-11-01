using Store.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _dataContext = new DataContext();

        public void AddClient(Client client)
        {
            if(_dataContext.Clients.Any(c => c.Email == client.Email))
            {
                throw new ArgumentException($"Client with email {client.Email} already exists!");
            }
        }

        public void AddFacture(Facture facture)
        {
            if(GetFacture(facture.Id) != null)
            {
                throw new ArgumentException($"Facture with guid {facture.Id} already exists!");
            }
            _dataContext.Factures.Add(facture);
        }

        public void AddOffer(Offer offer)
        {
            if(GetOffer(offer.Id) != null)
            {
                throw new ArgumentException($"Offer with guid {offer.Id} already exists!");
            }
            _dataContext.Offers.Add(offer);
        }

        public void AddProduct(Product product)
        {
            if (GetProduct(product.Id) != null)
            {
                throw new ArgumentException($"Product with key '${product.Id}' already exists!");
            }
            _dataContext.Products.Add(product.Id, product);
        }

        public void DeleteClient(Client client)
        {
            if (!_dataContext.Clients.Remove(client))
            {
                throw new ArgumentException($"Client does not exist!");
            }
        }

        public void DeleteFacture(Facture facture)
        {
            if(!_dataContext.Factures.Remove(facture))
            {
                throw new ArgumentException($"Facture does not exist!");
            }
        }

        public void DeleteOffer(Offer offer)
        {
            if (!_dataContext.Offers.Remove(offer))
            {
                throw new ArgumentException($"Offer does not exist!");
            }
        }

        public void DeleteProduct(Product product)
        {
            if (!_dataContext.Products.ContainsKey(product.Id))
            {
                throw new ArgumentException($"Product does not exist!");
            }
            _dataContext.Products.Remove(product.Id);
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
            return _dataContext.Clients.FirstOrDefault(c => c.Email == email);
        }

        public Facture GetFacture(Guid factureId)
        {
            return _dataContext.Factures.FirstOrDefault(f => f.Id == factureId);
        }

        public Offer GetOffer(Guid offerId)
        {
            return _dataContext.Offers.FirstOrDefault(o => o.Id == offerId);
        }

        public Product GetProduct(Guid productId)
        {
            try
            {
                return _dataContext.Products[productId];
            }
            catch
            {
                return null;
            }
        }

        public void UpdateClient(string email, Client client)
        {
            if(email != client.Email)
            {
                throw new ArgumentException("Can't change client's email!");
            }

            int id = _dataContext.Clients.FindIndex(c => c.Email == email);
            if(id == -1)
            {
                throw new ArgumentException($"Client with email {email} does not exist!");
            }

            _dataContext.Clients[id] = client;
        }

        public void UpdateFacture(Guid factureId, Facture facture)
        {
            Facture presentFacture = _dataContext.Factures.FirstOrDefault(f => f.Id == facture.Id);
            if(presentFacture == null)
            {
                throw new ArgumentException("Facture does not exist!");
            }

            int id = _dataContext.Factures.IndexOf(presentFacture);
            _dataContext.Factures[id] = facture;
        }

        public void UpdateOffer(Guid offerId, Offer offer)
        {
            int id = _dataContext.Offers.FindIndex(o => o.Id == offer.Id);
            if(id == -1)
            {
                throw new ArgumentException("Offer does not exist!");
            }

            _dataContext.Offers[id] = offer;
        }

        public void UpdateProduct(Guid productId, Product product)
        {
            if(productId != product.Id)
            {
                throw new ArgumentException("Can't change product's Guid!");
            }

            if(_dataContext.Products.ContainsKey(productId))
            {
                throw new ArgumentException($"Product with key '${productId}' does not exist!");
            }

            _dataContext.Products[productId] = product;
        }
    }
}
