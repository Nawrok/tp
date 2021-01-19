using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Model.Tests
{
    [TestClass]
    public class CreditCardModelTest
    {
        private readonly CreditCardModel _card1 = null;
        private CreditCardModel _card2;

        [TestInitialize]
        public void TestInitialize()
        {
            _card2 = new CreditCardModel
            {
                CardNumber = "11122233344455",
                CardType = "Vista",
                ExpMonth = 6,
                ExpYear = 21
            };
        }

        [TestMethod]
        public void CreditCardModelNull()
        {
            Assert.AreEqual(null, _card1);
        }

        [TestMethod]
        public void CreditCardModelCorrect()
        {
            Assert.IsNotNull(_card2);
            Assert.AreEqual("11122233344455", _card2.CardNumber);
            Assert.AreEqual("Vista", _card2.CardType);
            Assert.AreEqual(6, _card2.ExpMonth);
            Assert.AreEqual(21, _card2.ExpYear);
        }
    }
}