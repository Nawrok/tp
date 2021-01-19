using System.Linq;
using Logic.Tests.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic.Tests
{
    [TestClass]
    public class CreditCardServiceTest
    {
        private CreditCard _creditCard;
        private ICreditCardService _creditCardService;

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
            _creditCardService = new InMemoryCreditCardService();
        }

        [TestMethod]
        public void AddCreditCardTest()
        {
            Assert.AreEqual(2, _creditCardService.GetAllCreditCards().Count());
            _creditCardService.AddCreditCard(_creditCard);
            Assert.AreEqual(3, _creditCardService.GetAllCreditCards().Count());
        }

        [TestMethod]
        public void GetCreditCardTest()
        {
            _creditCardService.AddCreditCard(_creditCard);
            Assert.AreEqual(_creditCard.CardNumber, _creditCardService.GetCreditCard(_creditCard.CardNumber).CardNumber);
        }

        [TestMethod]
        public void GetAllCreditCardsTest()
        {
            Assert.AreEqual(2, _creditCardService.GetAllCreditCards().Count());
        }

        [TestMethod]
        public void UpdateCreditCardTest()
        {
            Assert.AreEqual(2, _creditCardService.GetAllCreditCards().Count());
            _creditCardService.UpdateCreditCard(_creditCardService.GetAllCreditCards().Last().CardNumber, _creditCard);
            Assert.AreEqual(2, _creditCardService.GetAllCreditCards().Count());
            Assert.AreEqual(_creditCard.CardNumber, _creditCardService.GetAllCreditCards().Last().CardNumber);
        }

        [TestMethod]
        public void DeleteCreditCardTest()
        {
            Assert.AreEqual(2, _creditCardService.GetAllCreditCards().Count());
            _creditCardService.AddCreditCard(_creditCard);
            Assert.AreEqual(3, _creditCardService.GetAllCreditCards().Count());
            _creditCardService.DeleteCreditCard(_creditCardService.GetAllCreditCards().Last().CardNumber);
            Assert.AreEqual(2, _creditCardService.GetAllCreditCards().Count());
        }
    }
}