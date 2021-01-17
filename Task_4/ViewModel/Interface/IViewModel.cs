using System;

namespace ViewModel.Interface
{
    public interface IViewModel
    {
        Action CloseWindow { get; set; }
    }
}