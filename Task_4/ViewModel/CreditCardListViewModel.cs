using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using ViewModel.Common;

namespace ViewModel
{
    class CreditCardListViewModel : ViewModelBase
    {
        private static CreditCardListViewModel instance = null;

        private CreditCardViewModel selectedCreditCard = null;

        private ObservableCollection<CreditCardViewModel> creditCardList = null;

        private ICommand showAddCommand;

        public ObservableCollection<CreditCardViewModel> CreditCardList
        {
            get
            {
                return GetCreditCards();
            }
            set
            {
                creditCardList = value;
                OnPropertyChanged("CreditCardList");
            }
        }

        public CreditCardViewModel SelectedCreditCard
        {
            get
            {
                return selectedCreditCard;
            }
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

        private CreditCardListViewModel()
        {
            this.CreditCardList = GetCreditCards();
        }

        public static CreditCardListViewModel Instance()
        {
            if (instance == null)
                instance = new CreditCardListViewModel();
            return instance;
        }

        internal ObservableCollection<CreditCardViewModel> GetCreditCards()
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
