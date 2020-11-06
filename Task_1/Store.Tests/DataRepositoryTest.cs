using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.DAL;
using Store.Tests.Filler;

namespace Store.Tests
{
    [TestClass]
    public class DataRepositoryTest
    {
        private IDataRepository _dataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _dataRepository = new DataRepository(new RandomDataFiller(5, 10, 15));
        }

        [TestMethod]
        public void AddClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void AddFacture_Test()
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
        public void DeleteClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteFacture_Test()
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
        public void GetAllClients_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetAllFactures_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetAllOffers_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetAllProducts_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetFacture_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetOffer_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetProduct_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateClient_Test()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateFacture_Test()
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