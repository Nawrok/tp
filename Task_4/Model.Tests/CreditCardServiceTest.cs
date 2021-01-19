using System.Linq;
using Logic.Tests.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Model.Tests
{
    [TestClass]
    public class CreditCardServiceTest
    {
        private CreditCardService _cardService;
        private CreditCardModel _creditCard;

        [TestInitialize]
        public void TestInitialize()
        {
            _creditCard = new CreditCardModel
            {
                CardNumber = "12345678902137",
                CardType = "MasterCard",
                ExpMonth = 12,
                ExpYear = 23
            };
            _cardService = new CreditCardService(new InMemoryCreditCardService());
        }

        [TestMethod]
        public void AddCreditCardTest()
        {
            Assert.AreEqual(2, _cardService.GetAllCreditCards().Count());
            _cardService.AddCreditCard(_creditCard);
            Assert.AreEqual(3, _cardService.GetAllCreditCards().Count());
        }

        [TestMethod]
        public void GetCreditCardTest()
        {
            _cardService.AddCreditCard(_creditCard);
            Assert.AreEqual(_creditCard.CardNumber, _cardService.GetCreditCard(_creditCard.CardNumber).CardNumber);
        }

        [TestMethod]
        public void GetAllCreditCardsTest()
        {
            Assert.AreEqual(2, _cardService.GetAllCreditCards().Count());
        }

        [TestMethod]
        public void UpdateCreditCardTest()
        {
            Assert.AreEqual(2, _cardService.GetAllCreditCards().Count());
            _cardService.UpdateCreditCard(_cardService.GetAllCreditCards().Last().CardNumber, _creditCard);
            Assert.AreEqual(2, _cardService.GetAllCreditCards().Count());
            Assert.AreEqual(_creditCard.CardNumber, _cardService.GetAllCreditCards().Last().CardNumber);
        }

        [TestMethod]
        public void DeleteCreditCardTest()
        {
            Assert.AreEqual(2, _cardService.GetAllCreditCards().Count());
            _cardService.AddCreditCard(_creditCard);
            Assert.AreEqual(3, _cardService.GetAllCreditCards().Count());
            _cardService.DeleteCreditCard(_cardService.GetAllCreditCards().Last().CardNumber);
            Assert.AreEqual(2, _cardService.GetAllCreditCards().Count());
        }
    }
}