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
        public void AddClient_Test() { }

        [TestMethod]
        public void AddProduct_Test() { }

        [TestMethod]
        public void AddOffer_Test() { }

        [TestMethod]
        public void DeleteProduct_Test() { }

        [TestMethod]
        public void DeleteOffer_Test() { }

        [TestMethod]
        public void BuyProducts_Test() { }

        [TestMethod]
        public void ReturnProducts_Test() { }

        [TestMethod]
        public void UpdateOfferState_Test() { }

        [TestMethod]
        public void GetFacturesForClient_Test() { }

        [TestMethod]
        public void GetReturnsForClient_Test() { }

        [TestMethod]
        public void GetFacturesForProduct_Test() { }

        [TestMethod]
        public void GetClientsForProduct_Test() { }

        [TestMethod]
        public void GetBoughtProductsForClient_Test() { }

        [TestMethod]
        public void GetProductSales_Test() { }
    }
}