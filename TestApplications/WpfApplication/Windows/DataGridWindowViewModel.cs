namespace WpfApplication.Windows
{
    using System.Collections.ObjectModel;

    public class DataGridWindowViewModel
    {
        public ObservableCollection<DataGridItem> Items { get; } = new ObservableCollection<DataGridItem>
        {
            new DataGridItem { Id = 1, Name = "Item 1" },
            new DataGridItem { Id = 2, Name = "Item 2" },
            new DataGridItem { Id = 3, Name = "Item 3" },
        };
    }
}
