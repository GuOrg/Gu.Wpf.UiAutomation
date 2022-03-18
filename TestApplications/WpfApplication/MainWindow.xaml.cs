namespace WpfApplication
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();

            Task.Delay(2000).ContinueWith(_
                => this.Dispatcher.Invoke(()
                    => AddMenuItem("DelayedMenuItem")));

            void AddMenuItem(string header)
                => this.TopMenu.Items.Add(new MenuItem() { Header = header });
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                var textBlock = (TextBlock)item;
                if (textBlock.Text == "Item 4")
                {
                    _ = MessageBox.Show("Do you really want to do it?");
                }
            }
        }
    }
}
