using System;
using System.Windows.Input;
using Model;
using ViewModel.Common;
using ViewModel.Interface;

namespace ViewModel
{
    public class CreditCardViewModel : ViewModelBase, IViewModel
    {
        private CreditCardModel _creditCardModel;
        private CreditCardService _creditCardService;
        private CreditCardViewModel _originalValue;

        public IWindowResolver WindowResolver { get; set; }

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

        public byte ExpMonth
        {
            get => _creditCardModel.ExpMonth;
            set
            {
                _creditCardModel.ExpMonth = value;
                OnPropertyChanged("ExpMonth");
            }
        }

        public short ExpYear
        {
            get => _creditCardModel.ExpYear;
            set
            {
                _creditCardModel.ExpYear = value;
                OnPropertyChanged("ExpYear");
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

        public CreditCardViewModel(CreditCardModel creditCardModel, CreditCardService creditCardService)
        {
            _creditCardModel = creditCardModel;
            _creditCardService = creditCardService;
        }

        public CreditCardViewModel()
        {
            _creditCardModel = new CreditCardModel();
            _creditCardService = new CreditCardService();
        }

        public ICommand CancelCommand => cancelCommand ?? (cancelCommand = new RelayCommand(Undo));

        public CreditCardListViewModel Container => CreditCardListViewModel.Instance();

        private void ShowEditDialog()
        {
            this.Mode = Mode.Edit;
            IOperationWindow dialog = WindowResolver.GetWindow();
            dialog.BindViewModel(this);
            dialog.Show();
        }

        public Action CloseWindow { get; set; }

        private void Update()
        {
            CreditCardModel card = new CreditCardModel
            {
                CardNumber = this.CardNumber,
                CardType = this.CardType,
                ExpMonth = this.ExpMonth,
                ExpYear = this.ExpYear
            };
            if (this.Mode == Mode.Add)
            {
                _creditCardService.AddCreditCard(card);
                this.Container.CreditCardList = this.Container.GetCreditCards();
            }
            else if(this.Mode == Mode.Edit)
            {
                _creditCardService.UpdateCreditCard(this.CardNumber, card);
                this._originalValue = (CreditCardViewModel)this.MemberwiseClone();
            }
            CloseWindow();
        }

        private void Delete()
        {
            _creditCardService.DeleteCreditCard(this.CardNumber);
            this.Container.CreditCardList = this.Container.GetCreditCards();
        }

        private void Undo()
        {
            if(this.Mode == Mode.Edit)
            {
                this.CardNumber = _originalValue.CardNumber;
                this.CardType = _originalValue.CardType;
                this.ExpMonth = _originalValue.ExpMonth;
                this.ExpYear = _originalValue.ExpYear;
            }
            CloseWindow();
        }
    }
}