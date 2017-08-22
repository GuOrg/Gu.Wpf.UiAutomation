namespace WpfApplication
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using WpfApplication.Infrastructure;

    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<DataGridItem> DataGridItems { get; }

        public ICommand InvokeButtonCommand { get; }

        public string InvokeButtonText { get => this.GetProperty<string>();
            set => this.SetProperty(value);
        }

        public MainViewModel()
        {
            this.DataGridItems = new ObservableCollection<DataGridItem>();
            this.DataGridItems.Add(new DataGridItem { Id = 1, Name = "Spongebob" });
            this.DataGridItems.Add(new DataGridItem { Id = 2, Name = "Patrick" });
            this.DataGridItems.Add(new DataGridItem { Id = 3, Name = "Tadeus" });

            this.InvokeButtonText = "Invoke me!";
            this.InvokeButtonCommand = new RelayCommand(o => this.InvokeButtonText = "Invoked!");
        }
    }
}
