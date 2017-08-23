namespace WpfApplication
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        private readonly Action<object> methodToExecute;
        private readonly Func<object, bool> canExecuteEvaluator;

        public RelayCommand(Action<object> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public RelayCommand(Action<object> methodToExecute, Func<object, bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute ?? throw new ArgumentNullException(nameof(methodToExecute));
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecuteEvaluator == null || this.canExecuteEvaluator.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke(parameter);
        }
    }
}
