namespace WpfApplication.Windows
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class ToolTipWindow : Window
    {
        public ToolTipWindow()
        {
            this.InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button { ToolTip: ToolTip toolTip })
            {
                toolTip.IsOpen = !toolTip.IsOpen;
            }
        }
    }
}
