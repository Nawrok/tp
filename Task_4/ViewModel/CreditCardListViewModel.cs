using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Model;
using ViewModel.Common;
using ViewModel.Interface;

namespace ViewModel
{
    public class CreditCardListViewModel : ViewModelBase, IViewModel
    {
        private static CreditCardListViewModel _instance;

        private readonly CreditCardService _creditCardService;

        private ObservableCollection<CreditCardViewModel> _creditCardList;

        private CreditCardViewModel _selectedCreditCard;

        private ICommand _showAddCommand;
        private ICommand _showEditCommand;

        public CreditCardListViewModel() : this(new CreditCardService()) { }

        public CreditCardListViewModel(CreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
            CreditCardList = GetCreditCards();
        }

        public IWindowResolver WindowResolver { get; set; }

        public ObservableCollection<CreditCardViewModel> CreditCardList
        {
            get => GetCreditCards();
            set
            {
                _creditCardList = value;
                OnPropertyChanged("CreditCardList");
            }
        }

        public CreditCardViewModel SelectedCreditCard
        {
            get => _selectedCreditCard;
            set
            {
                _selectedCreditCard = value;
                OnPropertyChanged("SelectedCreditCard");
            }
        }

        public ICommand ShowAddCommand => _showAddCommand ?? (_showAddCommand = new RelayCommand(ShowAddDialog));
        public ICommand ShowEditCommand => _showEditCommand ?? (_showEditCommand = new RelayCommand(ShowEditDialog));

        public Action CloseWindow { get; set; }

        public static CreditCardListViewModel Instance()
        {
            return _instance ?? (_instance = new CreditCardListViewModel());
        }

        public ObservableCollection<CreditCardViewModel> GetCreditCards()
        {
            if (_creditCardList == null)
            {
                _creditCardList = new ObservableCollection<CreditCardViewModel>();
            }

            _creditCardList.Clear();
            foreach (CreditCardViewModel card in _creditCardService.GetAllCreditCards().Select(card => new CreditCardViewModel(card, _creditCardService)))
            {
                _creditCardList.Add(card);
            }

            return _creditCardList;
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
            CreditCardViewModel.Container.CreditCardList = GetCreditCards();
        }

        private void ShowEditDialog()
        {
            _selectedCreditCard.Mode = Mode.Edit;

            IOperationWindow dialog = WindowResolver.GetWindow();
            dialog.BindViewModel(_selectedCreditCard);
            dialog.Show();
        }
    }
}