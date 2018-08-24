namespace WpfApplication
{
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
