using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL;
using Store.DAL.Model;

namespace Store.BLL
{
    public class DataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public Facture BuyProducts(Client client, Offer offer, int productCount)
        {
            if (offer.ProductsInStock == 0)
            {
                throw new InvalidOperationException("Currently, there are no products in stock, we are sorry!");
            }

            if (offer.ProductsInStock - productCount < 0)
            {
                throw new InvalidOperationException("You want to buy more products than we have in stock!");
            }

            var facture = new Facture(Guid.NewGuid(), client, offer, productCount, DateTimeOffset.Now);

            offer.ProductsInStock -= productCount;
            _dataRepository.UpdateOffer(offer.Product.Id, offer);
            _dataRepository.AddEvent(facture);

            return facture;
        }

        public Return ReturnProducts(Facture facture, int returningProducts)
        {
            var curFacture = _dataRepository.GetEvent(facture.Id);
            if (curFacture == null)
            {
                throw new ArgumentException("You cannot return products with not existing facture!");
            }

            Return returned = curFacture as Return;
            returned.ReturnDate = DateTimeOffset.Now;
            facture.Offer.ProductsInStock += returningProducts;

            _dataRepository.UpdateEvent(returned.Id, returned);
            _dataRepository.UpdateOffer(facture.Offer.Product.Id, facture.Offer);

            return returned;
        }

            public void UpdateOfferState(Product product, int productsInStock)
        {
            var offer = _dataRepository.GetAllOffers().FirstOrDefault(o => o.Product.Id.Equals(product.Id));
            if (offer == null)
            {
                throw new ArgumentException("No offer is available for this product!");
            }

            offer.ProductsInStock = productsInStock;

            _dataRepository.UpdateOffer(offer.Product.Id, offer);
        }

        public IEnumerable<Facture> GetFacturesForClient(Client client)
        {
            return _dataRepository.GetFactures().Where(f => f.Client.Email.Equals(client.Email));
        }

        public IEnumerable<Return> GetReturnsForClient(Client client)
        {
            return _dataRepository.GetReturns().Where(f => f.Client.Email.Equals(client.Email));
        }

        public IEnumerable<Facture> GetFacturesForProduct(Product product)
        {
            return _dataRepository.GetFactures().Where(f => f.Offer.Product.Id.Equals(product.Id));
        }

        public IEnumerable<Client> GetClientsForProduct(Product product)
        {
            var clientEmails = GetFacturesForProduct(product).Select(f => f.Client.Email).Distinct();
            return clientEmails.Select(email => _dataRepository.GetAllClients().First(c => c.Email.Equals(email)));
        }

        public IEnumerable<Product> GetBoughtProductsForClient(Client client)
        {
            return GetFacturesForClient(client).Select(f => f.Offer.Product);
        }

        public ValueTuple<int, decimal> GetProductSales(Product product)
        {
            var productCount = GetFacturesForProduct(product).Sum(f => f.ProductCount);
            var totalSales = GetFacturesForProduct(product).Sum(f => f.GrossPrice);
            return (productCount, totalSales);
        }

        //TODO RESEARCH ABOUT DELEGATING MEMBERS TO REPOSITORY
    }
}