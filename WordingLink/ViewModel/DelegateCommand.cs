using System;
using System.Windows.Input;

namespace WordingLink
{
    public sealed class DelegateCommand<T> : ICommand
    {
        private Action<T> _ExecuteMethod { get; set; }
        private Func<T, bool> _CanExecuteMethod { get; set; }


        public DelegateCommand(Action<T> execute)
        {
            _ExecuteMethod = execute;
        }
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _ExecuteMethod = execute;
            _CanExecuteMethod = canExecute;
        }

        private void ExecuteMethod(T parameter)
        {
            if (_ExecuteMethod != null)
            {
                _ExecuteMethod(parameter);
            }            
        }

        private bool CanExecuteMethod(T parameter)
        {
            if (_CanExecuteMethod != null)
            {
                return _CanExecuteMethod(parameter);
            }
            return true;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value ;}
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null )
            {
                return (_CanExecuteMethod == null);
            }
            return CanExecuteMethod((T)parameter);
        }
      
        public void Execute(object parameter)
        {
            ExecuteMethod((T)parameter);
        }
    }
}
