namespace WpfApplication.Windows
{
    using System.Collections.ObjectModel;
    using System.Linq;

    public class DataGridWindowViewModel
    {
        public ObservableCollection<DataGridItemViewModel> ThreeItems { get; } = new ObservableCollection<DataGridItemViewModel>
        {
            new DataGridItemViewModel { IntValue = 1, StringValue = "Item 1" },
            new DataGridItemViewModel { IntValue = 2, StringValue = "Item 2" },
            new DataGridItemViewModel { IntValue = 3, StringValue = "Item 3" },
        };

        public ObservableCollection<DataGridItemViewModel> HundredItems { get; } = new ObservableCollection<DataGridItemViewModel>(
            Enumerable.Range(1, 100)
                      .Select(x => new DataGridItemViewModel { IntValue = x, StringValue = $"Item {x}" }));
    }
}
