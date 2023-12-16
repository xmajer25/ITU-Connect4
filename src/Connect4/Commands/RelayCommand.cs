using System;
using System.Windows.Input;

namespace Connect4.Commands
{
    /*
* Author   : Dušan Slúka (xsluka00)
* File     : RelayCommand
* Brief    : A generic command handler that implements the ICommand interface for MVVM pattern. 
*            This class allows for parameterized actions to be bound to command objects, 
*            enabling UI elements like buttons and menu items to interact with application logic.
*/
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute) : this(execute, null) { }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is T typedParameter)
            {
                return _canExecute == null || _canExecute(typedParameter);
            }
            return _canExecute == null || _canExecute(default);
        }

        public void Execute(object parameter)
        {
            if (parameter is T typedParameter)
            {
                _execute(typedParameter);
            }
            else
            {
                if (typeof(T).IsValueType || parameter == null)
                {
                    _execute(default);
                }
            }
        }
    }
}
