namespace WpfApplication.Windows
{
    using System.Collections.ObjectModel;
    using System.Linq;

    public class DataGridWindowViewModel
    {
        public ObservableCollection<DataGridItem> ThreeItems { get; } = new ObservableCollection<DataGridItem>
        {
            new DataGridItem { Id = 1, Name = "Item 1" },
            new DataGridItem { Id = 2, Name = "Item 2" },
            new DataGridItem { Id = 3, Name = "Item 3" },
        };

        public ObservableCollection<DataGridItem> HundredItems { get; } = new ObservableCollection<DataGridItem>(
            Enumerable.Range(1, 100)
                      .Select(x => new DataGridItem { Id = x, Name = $"Item {x}" }));
    }
}
