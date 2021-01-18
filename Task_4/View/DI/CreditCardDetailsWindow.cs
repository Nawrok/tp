using ViewModel.Interface;

namespace View.DI
{
    public class CreditCardDetailsWindow : IOperationWindow
    {
        private CreditCardView _view;
        public event VoidHandler OnClose;

        public CreditCardDetailsWindow()
        {
            _view = new CreditCardView();
        }

        public void BindViewModel<T>(T viewModel) where T : IViewModel
        {
            _view.DataContext = viewModel;
        }

        public void Show()
        {
            _view.Show();
        }
    }
}
