﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Interface
{
    public interface IOperationWindow
    {
        void BindViewModel<T>(T viewModel) where T : IViewModel;
        void Show();
        event VoidHandler OnClose;
    }

    public delegate void VoidHandler();
}
