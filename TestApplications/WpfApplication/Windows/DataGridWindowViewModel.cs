namespace WpfApplication.Windows
{
    using System.Collections.ObjectModel;
    using System.Linq;

    public class DataGridWindowViewModel
    {
        public ObservableCollection<DataGridItemViewModel> ThreeItems { get; } = new ObservableCollection<DataGridItemViewModel>
        {
            new DataGridItemViewModel { Id = 1, Name = "Item 1" },
            new DataGridItemViewModel { Id = 2, Name = "Item 2" },
            new DataGridItemViewModel { Id = 3, Name = "Item 3" },
        };

        public ObservableCollection<DataGridItemViewModel> HundredItems { get; } = new ObservableCollection<DataGridItemViewModel>(
            Enumerable.Range(1, 100)
                      .Select(x => new DataGridItemViewModel { Id = x, Name = $"Item {x}" }));
    }
}
