using System;
using System.Windows.Input;

namespace ViewModel.Common
{
    public class RelayCommand : ICommand
    {
        #region API

        /// <summary>
        ///     Raises the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region constructors

        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _mExecute = execute ?? throw new ArgumentNullException(nameof(execute));
            _mCanExecute = canExecute;
        }

        #endregion

        #region ICommand

        public bool CanExecute(object parameter)
        {
            return _mCanExecute == null || _mCanExecute();
        }

        public virtual void Execute(object parameter)
        {
            _mExecute();
        }

        public event EventHandler CanExecuteChanged;

        #endregion

        #region private

        private readonly Action _mExecute;
        private readonly Func<bool> _mCanExecute;

        #endregion
    }
}