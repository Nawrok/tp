using System;
using System.Windows.Input;
using Model;
using ViewModel.Common;
using ViewModel.Interface;

namespace ViewModel
{
    public class CreditCardViewModel : ViewModelBase, IViewModel
    {
        private readonly CreditCardModel _creditCardModel;
        private readonly CreditCardService _creditCardService;

        private ICommand _cancelCommand;
        private ICommand _deleteCommand;
        private CreditCardModel _originalCardModel;
        private ICommand _showEditCommand;
        private ICommand _updateCommand;

        public CreditCardViewModel() : this(new CreditCardModel(), new CreditCardService()) { }

        public CreditCardViewModel(CreditCardModel creditCardModel, CreditCardService creditCardService)
        {
            _creditCardModel = creditCardModel;
            _originalCardModel = creditCardModel.Clone();
            _creditCardService = creditCardService;
        }

        private IWindowResolver WindowResolver { get; set; }

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
        public ICommand ShowEditCommand => _showEditCommand ?? (_showEditCommand = new RelayCommand(ShowEditDialog));
        public ICommand UpdateCommand => _updateCommand ?? (_updateCommand = new RelayCommand(Update));
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete));
        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(Undo));

        public static CreditCardListViewModel Container => CreditCardListViewModel.Instance();

        public Action CloseWindow { get; set; }

        private void ShowEditDialog()
        {
            Mode = Mode.Edit;
            IOperationWindow dialog = WindowResolver.GetWindow();
            dialog.BindViewModel(this);
            dialog.Show();
        }

        private void Update()
        {
            switch (Mode)
            {
                case Mode.Add:
                    _creditCardService.AddCreditCard(_creditCardModel);
                    Container.CreditCardList = Container.GetCreditCards();
                    break;
                case Mode.Edit:
                    _creditCardService.UpdateCreditCard(CardNumber, _creditCardModel);
                    _originalCardModel = _creditCardModel.Clone();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            CloseWindow();
        }

        private void Delete()
        {
            try
            {
                _creditCardService.DeleteCreditCard(CardNumber);
            }
            catch (Exception)
            {
                // ignored
            }

            Container.CreditCardList = Container.GetCreditCards();
        }

        private void Undo()
        {
            if (Mode == Mode.Edit)
            {
                CardNumber = _originalCardModel.CardNumber;
                CardType = _originalCardModel.CardType;
                ExpMonth = _originalCardModel.ExpMonth;
                ExpYear = _originalCardModel.ExpYear;
            }

            CloseWindow();
        }
    }
}