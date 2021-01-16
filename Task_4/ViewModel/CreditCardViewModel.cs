using System.Windows.Input;
using Model;
using ViewModel.Common;

namespace ViewModel
{
    public class CreditCardViewModel : ViewModelBase
    {
        private CreditCardModel _creditCardModel = new CreditCardModel();
        private CreditCardViewModel _originalValue;
        
        public string CardNumber
        {
            get => _creditCardModel.CardNumber;
            set
            {
                _creditCardModel.CardNumber = value;
                OnPropertyChanged("CardNumber");
            }
        }

        public string CardType
        {
            get => _creditCardModel.CardType;
            set
            {
                _creditCardModel.CardType = value;
                OnPropertyChanged("CardType");
            }
        }

        public int ExpMonth
        {
            get => _creditCardModel.ExpMonth;
            set
            {
                _creditCardModel.ExpMonth = (byte) value;
                OnPropertyChanged("ExpirationMonth");
            }
        }

        public int ExpYear
        {
            get => _creditCardModel.ExpYear;
            set
            {
                _creditCardModel.ExpYear = (short) value;
                OnPropertyChanged("ExpirationYear");
            }
        }

        public Mode Mode { get; set; }

        private ICommand showEditCommand;
        public ICommand ShowEditCommand => showEditCommand ?? (showEditCommand = new RelayCommand(ShowEditDialog));
        private ICommand updateCommand;
        public ICommand UpdateCommand => updateCommand ?? (updateCommand = new RelayCommand(Update));
        private ICommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand ?? (deleteCommand = new RelayCommand(Delete));
        private ICommand cancelCommand;
        public ICommand CancelCommand => cancelCommand ?? (cancelCommand = new RelayCommand(Undo));

        public CreditCardListViewModel Container => CreditCardListViewModel.Instance();

        private void ShowEditDialog() { }

        private void Update() { }

        private void Delete() { }

        private void Undo() { }
    }
}