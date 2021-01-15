using Data;
using Logic.Tests.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic.Tests
{
    [TestClass]
    public class DataServiceTest
    {
        private CreditCard _creditCard;
        private IDataRepository _dataRepository;

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
            _dataRepository = new InMemoryDataRepository();
        }

        [TestMethod]
        public void AddCreditCardTest() { }
    }
}