using System;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatBot_Client
{
    public class CommandHandler : ICommand
    {
        private Action<ImageSource> _action;
        private bool _canExecute;
        private ImageSource _imageSource;

        public CommandHandler(Action<ImageSource> action, ImageSource imageSource, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute;

        public void Execute(object parameter) => _action(_imageSource);

        public event EventHandler CanExecuteChanged;
    }
}