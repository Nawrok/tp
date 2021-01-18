using Model;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Common;
using ViewModel.Interface;

namespace ViewModel
{
    public class CreditCardListViewModel : ViewModelBase, IViewModel
    {
        private static CreditCardListViewModel instance;

        private CreditCardService _creditCardService = new CreditCardService();

        private ObservableCollection<CreditCardViewModel> creditCardList;

        private CreditCardViewModel selectedCreditCard;

        private ICommand showAddCommand;
        private ICommand showEditCommand;

        public IWindowResolver WindowResolver { get; set; }

        public CreditCardListViewModel()
        {
            CreditCardList = GetCreditCards();
        }

        public ObservableCollection<CreditCardViewModel> CreditCardList
        {
            get => GetCreditCards();
            set
            {
                creditCardList = value;
                OnPropertyChanged("CreditCardList");
            }
        }

        public CreditCardViewModel SelectedCreditCard
        {
            get => selectedCreditCard;
            set
            {
                selectedCreditCard = value;
                OnPropertyChanged("SelectedCreditCard");
            }
        }

        public ICommand ShowAddCommand
        {
            get
            {
                if (showAddCommand == null)
                {
                    showAddCommand = new RelayCommand(ShowAddDialog);
                }

                return showAddCommand;
            }
        }

        public ICommand ShowEditCommand
        {
            get
            {
                if (showEditCommand == null)
                {
                    showEditCommand = new RelayCommand(ShowEditDialog);
                }

                return showEditCommand;
            }
        }

        public Action CloseWindow { get; set; }

        public static CreditCardListViewModel Instance()
        {
            if (instance == null)
            {
                instance = new CreditCardListViewModel();
            }

            return instance;
        }

        public ObservableCollection<CreditCardViewModel> GetCreditCards()
        {
            if (creditCardList == null)
                creditCardList = new ObservableCollection<CreditCardViewModel>();
            creditCardList.Clear();
            foreach (CreditCardViewModel card in _creditCardService.GetAllCreditCards().Select(card => new CreditCardViewModel(card, _creditCardService)))
            {
                creditCardList.Add(card);
            }
            return creditCardList;
        }

        private void ShowAddDialog()
        {
            CreditCardViewModel card = new CreditCardViewModel
            {
                Mode = Mode.Add
            };

            IOperationWindow dialog = WindowResolver.GetWindow();
            dialog.BindViewModel(card);
            dialog.Show();
            card.Container.CreditCardList = GetCreditCards();
        }

        private void ShowEditDialog()
        {
            selectedCreditCard.Mode = Mode.Edit;

            IOperationWindow dialog = WindowResolver.GetWindow();
            dialog.BindViewModel(selectedCreditCard);
            dialog.Show();
        }
    }
}