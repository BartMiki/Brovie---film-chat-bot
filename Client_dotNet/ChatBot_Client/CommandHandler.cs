using System;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatBot_Client
{
    public class CommandHandler : ICommand
    {
        private readonly Action _action;
        private readonly bool _canExecute;

        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute;

        public void Execute(object parameter) => _action();

        public event EventHandler CanExecuteChanged;
    }
}