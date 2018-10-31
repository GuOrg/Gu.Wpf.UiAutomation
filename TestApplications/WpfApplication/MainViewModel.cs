namespace WpfApplication
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class MainViewModel : INotifyPropertyChanged
    {
        private string invokeButtonText;

        public MainViewModel()
        {
            this.DataGridItems = new ObservableCollection<DataGridItemViewModel>
            {
                new DataGridItemViewModel { IntValue = 1, StringValue = "Spongebob" },
                new DataGridItemViewModel { IntValue = 2, StringValue = "Patrick" },
                new DataGridItemViewModel { IntValue = 3, StringValue = "Tadeus" },
            };

            this.invokeButtonText = "Invoke me!";
            this.InvokeButtonCommand = new RelayCommand(o => this.InvokeButtonText = "Invoked!");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<DataGridItemViewModel> DataGridItems { get; }

        public ICommand InvokeButtonCommand { get; }

        public string InvokeButtonText
        {
            get => this.invokeButtonText;

            set
            {
                if (value == this.invokeButtonText)
                {
                    return;
                }

                this.invokeButtonText = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
