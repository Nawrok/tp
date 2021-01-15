using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModel.Common;

namespace ViewModel
{
    class CreditCardViewModel : ViewModelBase
    {
        private string cardNumber;
        private string cardType;
        private int expMonth;
        private int expYear;

        private ICommand showEditCommand;

        private ICommand updateCommand;

        private ICommand deleteCommand;

        private ICommand cancelCommand;

        private CreditCardViewModel originalValue;

        public int CreditCardId
        {
            get;
            set;
        }

        public string CardNumber
        {
            get { return cardNumber; }
            set
            {
                cardNumber = value;
                OnPropertyChanged("CardNumber");
            }
        }

        public string CardType
        {
            get { return cardType; }
            set
            {
                cardType = value;
                OnPropertyChanged("CardType");
            }
        }

        public int ExpMonth
        {
            get { return expMonth; }
            set
            {
                expMonth = value;
                OnPropertyChanged("ExpirationMonth");
            }
        }

        public int ExpYear
        {
            get { return expYear; }
            set
            {
                expYear = value;
                OnPropertyChanged("ExpirationYear");
            }
        }

        public Mode Mode
        {
            get;
            set;
        }

        public CreditCardListViewModel Container
        {
            get { return CreditCardListViewModel.Instance(); }
        }

        public ICommand ShowEditCommand
        {
            get
            {
                if (showEditCommand == null)
                {
                    showEditCommand = new CommandBase(i => this.ShowEditDialog(), null);
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
                    updateCommand = new CommandBase(i => this.Update(), null);
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
                    deleteCommand = new CommandBase(i => this.Delete(), null);
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
                    cancelCommand = new CommandBase(i => this.Undo(), null);
                }
                return cancelCommand;
            }
        }
        private void ShowEditDialog()
        {

        }
        private void Update()
        {

        }

        private void Delete()
        {

        }

        private void Undo()
        {

        }
    }
}
