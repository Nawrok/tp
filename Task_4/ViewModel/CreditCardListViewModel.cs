using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Common;
using ViewModel.Interface;

namespace ViewModel
{
    public class CreditCardListViewModel : ViewModelBase
    {
        private static CreditCardListViewModel instance;

        private CreditCardService _creditCardService = new CreditCardService();

        private ObservableCollection<CreditCardModel> creditCardList;

        private CreditCardViewModel selectedCreditCard;

        private ICommand showAddCommand;
        private ICommand showEditCommand;

        public IWindowResolver WindowResolver { get; set; }

        public CreditCardListViewModel()
        {
            CreditCardList = GetCreditCards();
        }

        public CreditCardListViewModel(CreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        public ObservableCollection<CreditCardModel> CreditCardList
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

        public ObservableCollection<CreditCardModel> GetCreditCards()
        {
            if (CreditCardList == null)
                CreditCardList = new ObservableCollection<CreditCardModel>();
            CreditCardList.Clear();
            foreach (CreditCardModel card in _creditCardService.GetAllCreditCards())
            {
                CreditCardList.Add(card);
            }
            return CreditCardList;
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