namespace WpfApplication.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        private readonly Action<object> methodToExecute;
        readonly Func<object, bool> canExecuteEvaluator;

        public RelayCommand(Action<object> methodToExecute)
            : this(methodToExecute, null) { }

        public RelayCommand(Action<object> methodToExecute, Func<object, bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecuteEvaluator == null || this.canExecuteEvaluator.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke(parameter);
        }
    }
}
