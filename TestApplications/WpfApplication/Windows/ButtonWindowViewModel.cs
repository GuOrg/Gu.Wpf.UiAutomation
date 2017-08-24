namespace WpfApplication.Windows
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class ButtonWindowViewModel : INotifyPropertyChanged
    {
        private int count;

        public ButtonWindowViewModel()
        {
            this.IncreaseCommand = new RelayCommand(_ => this.Count++);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand IncreaseCommand { get; }

        public int Count
        {
            get
            {
                return this.count;
            }

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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
