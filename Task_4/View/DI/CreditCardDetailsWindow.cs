using ViewModel.Common;
using ViewModel.Interface;

namespace View.DI
{
    public class CreditCardDetailsWindow : IOperationWindow
    {
        private readonly CreditCardView _view;
        public event VoidHandler OnClose;

        public CreditCardDetailsWindow()
        {
            _view = new CreditCardView();
        }

        public void BindViewModel<T>(T viewModel) where T : IViewModel
        {
            _view.DataContext = viewModel;
            viewModel.CloseWindow = () =>
            {
                OnClose?.Invoke();
                _view.Close();
            };
        }

        public void Show()
        {
            _view.Show();
        }
    }
}
