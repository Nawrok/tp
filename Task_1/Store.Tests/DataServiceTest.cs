using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.BLL;
using Store.DAL;
using Store.DAL.Model;
using Store.Tests.Filler;
using System;

namespace Store.Tests
{
    [TestClass]
    public class DataServiceTest
    {
        private DataService _dataService;
        private Client _c1;
        private Product _p1;
        private Offer _o1;
        private Event _e1;

        [TestInitialize]
        public void TestInitialize()
        {
            _dataService = new DataService(new DataRepository(new ConstDataFiller()));
            _c1 = new Client("Norbert", "Gierczak", "krzycz@disunio.pl", "Katowice");
            _p1 = new Product(Guid.NewGuid(), "Topór", "Broń ostra, można nią rzucać", "Broń");
            _o1 = new Offer(_p1, 450.00m, 0.23m, 2);
            _e1 = new Facture(Guid.NewGuid(), _c1, _o1, 1, DateTimeOffset.Now.AddDays(-2));
        }

        [TestMethod]
        public void AddClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void AddProduct_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void AddOffer_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteProduct_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteOffer_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void BuyProducts_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ReturnProducts_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateOfferState_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetFacturesForClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetReturnsForClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetFacturesForProduct_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetClientsForProduct_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetBoughtProductsForClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetProductSales_Test()
        {
            Assert.Inconclusive();
        }
    }
}