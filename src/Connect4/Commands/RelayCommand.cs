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
        // Fields for storing the execute and canExecute logic passed to the command.
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        // Constructor for command creation with execute logic.
        public RelayCommand(Action<T> execute) : this(execute, null) { }

        // Constructor for command creation with both execute and canExecute logic.
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            // Throws an exception if execute logic is not provided.
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Event that is raised when the conditions for command execution change.
        public event EventHandler CanExecuteChanged
        {
            // Adds or removes the handler for re-evaluating whether the command can execute.
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Determines whether the command can execute in its current state.
        public bool CanExecute(object parameter)
        {
            // Casts the parameter to the appropriate type and checks if it can execute.
            if (parameter is T typedParameter)
            {
                return _canExecute == null || _canExecute(typedParameter);
            }
            return _canExecute == null || _canExecute(default);
        }

        // Executes the command.
        public void Execute(object parameter)
        {
            // Casts the parameter to the appropriate type and executes the command.
            if (parameter is T typedParameter)
            {
                _execute(typedParameter);
            }
            else
            {
                // Executes the command with a default value if the parameter type is a value type or null.
                if (typeof(T).IsValueType || parameter == null)
                {
                    _execute(default);
                }
            }
        }
    }
}
