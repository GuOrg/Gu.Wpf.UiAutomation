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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.SetValue(ToolTipService.IsOpenProperty, !(bool)button.GetValue(ToolTipService.IsOpenProperty));
            }
        }
    }
}
