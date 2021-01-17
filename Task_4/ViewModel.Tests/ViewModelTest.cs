using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ViewModel;

namespace ViewModel.Tests
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void CreditCardViewModelCtorTest()
        {
            CreditCardViewModel creditCardViewModel = new CreditCardViewModel();
            Assert.IsNotNull(creditCardViewModel.CardNumber);
            Assert.IsNotNull(creditCardViewModel.CardType);
            Assert.IsNotNull(creditCardViewModel.ExpMonth);
            Assert.IsNotNull(creditCardViewModel.ExpYear);
        }

        [TestMethod]
        public void CreditCardListViewModelCtorTest()
        {
            CreditCardListViewModel creditCardListViewModel = new CreditCardListViewModel();
            Assert.IsNotNull(creditCardListViewModel.CreditCardList);
        }

        [TestMethod]
        public void CreditCardViewModelCommandsTest()
        {
            CreditCardViewModel creditCardViewModel = new CreditCardViewModel();
            Assert.IsTrue(creditCardViewModel.UpdateCommand.CanExecute(null));
            Assert.IsTrue(creditCardViewModel.CancelCommand.CanExecute(null));
            Assert.IsTrue(creditCardViewModel.DeleteCommand.CanExecute(null));
            Assert.IsTrue(creditCardViewModel.ShowEditCommand.CanExecute(null));
        }

        [TestMethod]
        public void CreditCardListViewModelCommandsTest()
        {
            CreditCardListViewModel creditCardListViewModel = new CreditCardListViewModel();
            Assert.IsTrue(creditCardListViewModel.ShowEditCommand.CanExecute(null));
            Assert.IsTrue(creditCardListViewModel.ShowAddCommand.CanExecute(null));
        }
    }
}
