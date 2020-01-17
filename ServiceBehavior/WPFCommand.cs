namespace ServiceBehavior
{
    using System;
    using System.Windows.Input;

    public class WPFCommand : IWPFCommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public WPFCommand(Action execute) :
            this(execute, () => true)
        {
        }

        public WPFCommand(Action<object> execute) :
            this(execute, o => true)
        {
        }

        public WPFCommand(Action execute, Func<bool> canExecute) :
            this(o => execute?.Invoke(), o => canExecute?.Invoke() ?? true)
        {
        }

        public WPFCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public interface IWPFCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }

    public class CommandFactory
    {
        public static IWPFCommand Create(Action execute)
        {
            return new WPFCommand(execute);
        }

        public static IWPFCommand Create(Action<object> execute)
        {
            return new WPFCommand(execute);
        }

        public static IWPFCommand Create(Action execute, Func<bool> canExecute)
        {
            return new WPFCommand(execute, canExecute);
        }

        public static IWPFCommand Create(Action<object> execute, Func<object, bool> canExecute)
        {
            return new WPFCommand(execute, canExecute);
        }
    }
}
