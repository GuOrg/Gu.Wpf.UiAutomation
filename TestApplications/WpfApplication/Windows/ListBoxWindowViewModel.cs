namespace WpfApplication.Windows
{
    using System.Collections.ObjectModel;

    public class ListBoxWindowViewModel
    {
        public ObservableCollection<ListBoxItemViewModel> Items { get; } = new ObservableCollection<ListBoxItemViewModel>
            {
                new ListBoxItemViewModel { Name = "Johan" },
                new ListBoxItemViewModel { Name = "Erik" },
            };
    }
}
