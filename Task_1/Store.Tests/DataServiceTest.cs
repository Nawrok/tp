using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.BLL;
using Store.DAL;
using Store.Tests.Filler;

namespace Store.Tests
{
    [TestClass]
    public class DataServiceTest
    {
        private DataService _dataService;

        [TestInitialize]
        public void TestInitialize()
        {
            _dataService = new DataService(new DataRepository(new ConstDataFiller()));
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