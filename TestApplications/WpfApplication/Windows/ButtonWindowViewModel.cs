namespace WpfApplication.Windows
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    public class ButtonWindowViewModel : INotifyPropertyChanged
    {
        private int count;

        public ButtonWindowViewModel()
        {
            this.IncreaseCommand = new RelayCommand(_ => this.Count++);
            this.SleepCommand = new RelayCommand(_ =>
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                this.Count++;
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand IncreaseCommand { get; }

        public ICommand SleepCommand { get; }

        public int Count
        {
            get => this.count;

            set
            {
                if (value == this.count)
                {
                    return;
                }

                this.count = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
