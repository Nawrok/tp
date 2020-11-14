using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.BLL;
using Store.DAL;
using Store.DAL.Model;
using Store.Tests.Filler;

namespace Store.Tests
{
    [TestClass]
    public class DataServiceTest
    {
        private Client _c1;
        private DataService _dataService;
        private List<string> _emails;
        private List<Guid> _factures;
        private Offer _o1;
        private Product _p1;
        private List<Guid> _products;

        [TestInitialize]
        public void TestInitialize()
        {
            IDataRepository dataRepository = new DataRepository(new ConstDataFiller());
            _dataService = new DataService(dataRepository);
            _c1 = new Client("Norbert", "Gierczak", "krzycz@disunio.pl", "Katowice");
            _p1 = new Product(Guid.NewGuid(), "Topór", "Broń ostra, można nią rzucać", "Broń");
            _o1 = new Offer(_p1, 450.00m, 0.23m, 5);
            _emails = dataRepository.GetAllClients().Select(c => c.Email).ToList();
            _products = dataRepository.GetAllProducts().Select(p => p.Id).ToList();
            _factures = dataRepository.GetAllFactures().Select(f => f.Id).ToList();
        }

        [TestMethod]
        public void AddClient_Test_Succesful()
        {
            Assert.AreEqual(_c1.Email, _dataService.AddClient(_c1.Name, _c1.Surname, _c1.Email, _c1.City).Email);
        }

        [TestMethod]
        public void AddProduct_Test_Succesful()
        {
            Assert.AreEqual(_p1.Id, _dataService.AddProduct(_p1.Id, _p1.Name, _p1.Description, _p1.Type).Id);
        }

        [TestMethod]
        public void AddOffer_Test_Succesful()
        {
            AddProduct_Test_Succesful();
            Assert.AreEqual(_p1.Id, _dataService.AddOffer(_o1.Product.Id, _o1.NetPrice, _o1.Tax, _o1.ProductsInStock).Product.Id);
        }

        [TestMethod]
        public void AddOffer_Test_NotPositiveNetPriceValue()
        {
            AddProduct_Test_Succesful();
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddOffer(_o1.Product.Id, -1m, 0.05m, 5));
        }

        [TestMethod]
        public void AddOffer_Test_NotNonNegativeTaxValue()
        {
            AddProduct_Test_Succesful();
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddOffer(_o1.Product.Id, 1m, -0.05m, 5));
        }

        [TestMethod]
        public void AddOffer_Test_NotNonNegativeProductsInStockValue()
        {
            AddProduct_Test_Succesful();
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddOffer(_o1.Product.Id, 1m, 0.05m, -5));
        }

        [TestMethod]
        public void DeleteProduct_Test_Successful()
        {
            AddProduct_Test_Succesful();
            Assert.ThrowsException<ArgumentException>(AddProduct_Test_Succesful);
            _dataService.DeleteProduct(_p1.Id);
            AddProduct_Test_Succesful();
        }

        [TestMethod]
        public void DeleteOffer_Test()
        {
            AddOffer_Test_Succesful();
            Assert.ThrowsException<ArgumentException>(AddOffer_Test_Succesful);
            _dataService.DeleteOffer(_o1.Product.Id);
            _dataService.DeleteProduct(_p1.Id);
            AddOffer_Test_Succesful();
        }

        [TestMethod]
        public void BuyProducts_Test_NoProductsInStock()
        {
            _dataService.UpdateOfferState(_products[2], 0);
            Assert.ThrowsException<InvalidOperationException>(() => _dataService.BuyProducts(_emails[0], _products[2], 5));
        }

        [TestMethod]
        public void BuyProducts_Test_NotEnoughProductsInStock()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _dataService.BuyProducts(_emails[0], _products[2], 3));
        }

        [TestMethod]
        public void BuyProducts_Test_Successful()
        {
            Assert.AreEqual(2, _dataService.GetFacturesForProduct(_products[0]).Count());
            Assert.AreEqual(3, _dataService.GetFacturesForClient(_emails[0]).Count());
            Assert.AreEqual(7, _dataService.GetProductSales(_products[0]).Item1);
            _dataService.BuyProducts(_emails[0], _products[0], 15);
            Assert.AreEqual(3, _dataService.GetFacturesForProduct(_products[0]).Count());
            Assert.AreEqual(4, _dataService.GetFacturesForClient(_emails[0]).Count());
            Assert.AreEqual(22, _dataService.GetProductSales(_products[0]).Item1);
        }

        [TestMethod]
        public void ReturnProducts_Test_MoreProductThanBought()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _dataService.ReturnProducts(_factures[1], 2));
        }

        [TestMethod]
        public void ReturnProducts_Test_Successful()
        {
            Assert.AreEqual(0, _dataService.GetReturnsForClient(_emails[0]).Count());
            Assert.AreEqual(2, _dataService.GetFacturesForProduct(_products[1]).Count());
            Assert.AreEqual(3, _dataService.GetFacturesForClient(_emails[0]).Count());
            Assert.AreEqual(6, _dataService.GetProductSales(_products[1]).Item1);
            _dataService.ReturnProducts(_factures[1], 1);
            Assert.AreEqual(1, _dataService.GetReturnsForClient(_emails[0]).Count());
            Assert.AreEqual(1, _dataService.GetReturnsForProduct(_products[1]).Count());
            Assert.AreEqual(1, _dataService.GetReturnsForClient(_emails[0]).Count());
            Assert.AreEqual(5, _dataService.GetProductSales(_products[1]).Item1);
        }

        [TestMethod]
        public void UpdateOfferState_Test_Successful()
        {
            Assert.AreEqual(7, _dataService.GetProductSales(_products[0]).Item1);
            Assert.ThrowsException<InvalidOperationException>(() => _dataService.BuyProducts(_emails[0], _products[0], 41));
            _dataService.UpdateOfferState(_products[0], 45);
            _dataService.BuyProducts(_emails[0], _products[0], 41);
        }

        [TestMethod]
        public void UpdateOfferState_Test_NotPositiveParameters()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataService.UpdateOfferState(_products[0], -1));
        }

        [TestMethod]
        public void GetFacturesForClient_Test()
        {
            Assert.AreEqual(3, _dataService.GetFacturesForClient(_emails[0]).Count());
            Assert.AreEqual(2, _dataService.GetFacturesForClient(_emails[1]).Count());
        }

        [TestMethod]
        public void GetReturnsForClient_Test()
        {
            Assert.AreEqual(0, _dataService.GetReturnsForClient(_emails[0]).Count());
            Assert.AreEqual(1, _dataService.GetReturnsForClient(_emails[1]).Count());
        }

        [TestMethod]
        public void GetFacturesForProduct_Test()
        {
            Assert.AreEqual(2, _dataService.GetFacturesForProduct(_products[0]).Count());
            Assert.AreEqual(2, _dataService.GetFacturesForProduct(_products[1]).Count());
            Assert.AreEqual(1, _dataService.GetFacturesForProduct(_products[2]).Count());
        }

        [TestMethod]
        public void GetClientsForProduct_Test()
        {
            Assert.AreEqual(2, _dataService.GetClientsForProduct(_products[0]).Count());
            Assert.AreEqual(2, _dataService.GetClientsForProduct(_products[1]).Count());
            Assert.AreEqual(1, _dataService.GetClientsForProduct(_products[2]).Count());
        }

        [TestMethod]
        public void GetBoughtProductsForClient_Test()
        {
            Assert.AreEqual(3, _dataService.GetBoughtProductsForClient(_emails[0]).Count());
            Assert.AreEqual(2, _dataService.GetBoughtProductsForClient(_emails[1]).Count());
        }

        [TestMethod]
        public void GetProductSales_Test()
        {
            Assert.AreEqual(7, _dataService.GetProductSales(_products[0]).Item1);
            Assert.AreEqual(10 * 14.50m * 1.05m - 3 * 14.50m * 1.05m, _dataService.GetProductSales(_products[0]).Item2);
            Assert.AreEqual(6, _dataService.GetProductSales(_products[1]).Item1);
            Assert.AreEqual(6 * 450.00m * 1.23m, _dataService.GetProductSales(_products[1]).Item2);
            Assert.AreEqual(3, _dataService.GetProductSales(_products[2]).Item1);
            Assert.AreEqual(3 * 1500.00m * 1.23m, _dataService.GetProductSales(_products[2]).Item2);
        }
    }
}