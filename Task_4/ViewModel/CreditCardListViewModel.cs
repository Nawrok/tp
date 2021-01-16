using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Common;

namespace ViewModel
{
    public class CreditCardListViewModel : ViewModelBase
    {
        private static CreditCardListViewModel instance;

        private ObservableCollection<CreditCardViewModel> creditCardList;

        private CreditCardViewModel selectedCreditCard;

        private ICommand showAddCommand;

        private CreditCardListViewModel()
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
            //if (CreditCardList == null)
            //    CreditCardList = new ObservableCollection<CreditCardViewModel>();
            //CreditCardList.Clear();
            //foreach ()
            //{
            //    CreditCardViewModel c = new CreditCardViewModel(i);
            //    CreditCardList.Add(c);
            //}
            return CreditCardList;
        }

        private void ShowAddDialog()
        {
            //CreditCardViewModel CreditCard = new CreditCardViewModel();
            //CreditCard.Mode = Mode.Add;

            //IModalDialog dialog = ServiceProvider.Instance.Get<IModalDialog>();
            //dialog.BindViewModel(CreditCard);
            //dialog.ShowDialog();
        }
    }
}