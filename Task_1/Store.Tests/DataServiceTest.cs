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
            _dataService = new DataService(new DataRepository(new RandomDataFiller()));
        }

        [TestMethod]
        public void DataService_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void BuyProduct_Test()
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
        public void GetClientsFromCity_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetBoughtProducts_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetTheSameTypeProducts_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetFacturesForProduct_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetFacturesInTime_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetClientsForProduct_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetProductSales_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void AddClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void AddOffer_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void AddProduct_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteFacture_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteOffer_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteProduct_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetClients_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetFactures_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetOffers_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetProducts_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateFacture_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateOffer_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateProduct_Test()
        {
            Assert.Inconclusive();
        }
    }
}