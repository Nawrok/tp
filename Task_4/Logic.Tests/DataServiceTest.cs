using Data;
using Logic.Tests.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Logic.Tests
{
    [TestClass]
    public class DataServiceTest
    {
        private CreditCard _creditCard;
        private DataService _dataService;

        [TestInitialize]
        public void TestInitialize()
        {
            _creditCard = new CreditCard
            {
                CardNumber = "12345678902137",
                CardType = "MasterCard",
                ExpMonth = 12,
                ExpYear = 23
            };
            _dataService = new DataService(new InMemoryDataRepository());
        }

        [TestMethod]
        public void AddCreditCardTest()
        {
            Assert.AreEqual(2, _dataService.GetAllCreditCards().Count());
            _dataService.AddCreditCard(_creditCard);
            Assert.AreEqual(3, _dataService.GetAllCreditCards().Count());
        }

        [TestMethod]
        public void GetCreditCardTest()
        {
            _dataService.AddCreditCard(_creditCard);
            Assert.AreEqual(_creditCard.CardNumber, _dataService.GetCreditCard(_creditCard.CardNumber).CardNumber);
        }

        [TestMethod]
        public void GetAllCreditCardsTest()
        {
            Assert.AreEqual(2, _dataService.GetAllCreditCards().Count());
        }

        [TestMethod]
        public void UpdateCreditCardTest()
        {
            Assert.AreEqual(2, _dataService.GetAllCreditCards().Count());
            _dataService.UpdateCreditCard(_dataService.GetAllCreditCards().Last().CardNumber, _creditCard);
            Assert.AreEqual(2, _dataService.GetAllCreditCards().Count());
            Assert.AreEqual(_creditCard.CardNumber, _dataService.GetAllCreditCards().Last().CardNumber);
        }

        [TestMethod]
        public void DeleteCreditCardTest()
        {
            Assert.AreEqual(2, _dataService.GetAllCreditCards().Count());
            _dataService.AddCreditCard(_creditCard);
            Assert.AreEqual(3, _dataService.GetAllCreditCards().Count());
            _dataService.DeleteCreditCard(_dataService.GetAllCreditCards().Last().CardNumber);
            Assert.AreEqual(2, _dataService.GetAllCreditCards().Count());
        }
    }
}