using System.Windows.Input;
using ViewModel.Common;

namespace ViewModel
{
    internal class CreditCardViewModel : ViewModelBase
    {
        private ICommand cancelCommand;
        private string cardNumber;
        private string cardType;

        private ICommand deleteCommand;
        private int expMonth;
        private int expYear;

        private CreditCardViewModel originalValue;

        private ICommand showEditCommand;

        private ICommand updateCommand;

        public int CreditCardId { get; set; }

        public string CardNumber
        {
            get => cardNumber;
            set
            {
                cardNumber = value;
                OnPropertyChanged("CardNumber");
            }
        }

        public string CardType
        {
            get => cardType;
            set
            {
                cardType = value;
                OnPropertyChanged("CardType");
            }
        }

        public int ExpMonth
        {
            get => expMonth;
            set
            {
                expMonth = value;
                OnPropertyChanged("ExpirationMonth");
            }
        }

        public int ExpYear
        {
            get => expYear;
            set
            {
                expYear = value;
                OnPropertyChanged("ExpirationYear");
            }
        }

        public Mode Mode { get; set; }

        public CreditCardListViewModel Container => CreditCardListViewModel.Instance();

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

        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(Update);
                }

                return updateCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(Delete);
                }

                return deleteCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(Undo);
                }

                return cancelCommand;
            }
        }

        private void ShowEditDialog() { }

        private void Update() { }

        private void Delete() { }

        private void Undo() { }
    }
}