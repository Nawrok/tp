using Logic.Tests.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Linq;

namespace ViewModel.Tests
{
    [TestClass]
    public class ViewModelTest
    {
        private CreditCardViewModel _creditCardViewModel;
        private CreditCardListViewModel _creditCardListViewModel;

        [TestInitialize]
        public void TestInitialize()
        {
            CreditCardService service = new CreditCardService(new InMemoryCreditCardService());
            CreditCardModel cardModel = new CreditCardModel
            {
                CardNumber = "12345678902137",
                CardType = "MasterCard",
                ExpMonth = 12,
                ExpYear = 23
            };
            _creditCardViewModel = new CreditCardViewModel(cardModel, service);
            _creditCardListViewModel = new CreditCardListViewModel(service);
        }

        [TestMethod]
        public void CreditCardViewModelCtorTest()
        {
            Assert.IsNotNull(_creditCardViewModel.CardNumber);
            Assert.IsNotNull(_creditCardViewModel.CardType);
            Assert.IsNotNull(_creditCardViewModel.ExpMonth);
            Assert.IsNotNull(_creditCardViewModel.ExpYear);
        }

        [TestMethod]
        public void CreditCardListViewModelCtorTest()
        {
            Assert.IsNotNull(_creditCardListViewModel.CreditCardList);
        }

        [TestMethod]
        public void CreditCardViewModelCommandsTest()
        {
            Assert.IsTrue(_creditCardViewModel.UpdateCommand.CanExecute(null));
            Assert.IsTrue(_creditCardViewModel.CancelCommand.CanExecute(null));
            Assert.IsTrue(_creditCardViewModel.DeleteCommand.CanExecute(null));
            Assert.IsTrue(_creditCardViewModel.ShowEditCommand.CanExecute(null));
        }

        [TestMethod]
        public void CreditCardListViewModelCommandsTest()
        {
            Assert.IsTrue(_creditCardListViewModel.ShowEditCommand.CanExecute(null));
            Assert.IsTrue(_creditCardListViewModel.ShowAddCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCreditCardViewModelTest()
        {
            Assert.AreEqual(2, _creditCardListViewModel.CreditCardList.Count);
            _creditCardViewModel.Mode = Mode.Add;
            _creditCardViewModel.UpdateCommand.Execute(null);
            Assert.AreEqual(3, _creditCardListViewModel.CreditCardList.Count);
        }

        [TestMethod]
        public void EditCreditCardViewModelTest()
        {
            AddCreditCardViewModelTest();
            Assert.AreEqual(3, _creditCardListViewModel.CreditCardList.Count);
            Assert.AreEqual("MasterCard", _creditCardListViewModel.CreditCardList.Last().CardType);
            _creditCardViewModel.Mode = Mode.Edit;
            _creditCardViewModel.CardType = "Visa";
            _creditCardViewModel.UpdateCommand.Execute(null);
            Assert.AreEqual("Visa", _creditCardListViewModel.CreditCardList.Last().CardType);
            Assert.AreEqual(3, _creditCardListViewModel.CreditCardList.Count);
        }

        [TestMethod]
        public void DeleteCreditCardViewModelTest()
        {
            AddCreditCardViewModelTest();
            _creditCardViewModel.DeleteCommand.Execute(null);
            Assert.AreEqual(2, _creditCardListViewModel.CreditCardList.Count);
        }
    }
}