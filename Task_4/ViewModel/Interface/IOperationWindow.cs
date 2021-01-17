using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Interface
{
    public interface IOperationWindow
    {
        void BindViewModel<T>(T viewModel);
        void Show();
        event VoidHandler OnClose;
    }

    public delegate void VoidHandler();
}
