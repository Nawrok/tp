using ViewModel.Interface;

namespace View.DI
{
    public class CreditCardDetailsResolver : IWindowResolver
    {
        public IOperationWindow GetWindow()
        {
            return new CreditCardDetailsWindow();
        }
    }
}