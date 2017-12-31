namespace WpfApplication.Windows
{
    using System.Windows;

    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            this.InitializeComponent();
        }

        private void OnShowMessageBoxClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Message text", "Caption text", MessageBoxButton.OK);
        }

        private void OnShowDialogClick(object sender, RoutedEventArgs e)
        {
            var window = new Dialog { Owner = this };
            window.ShowDialog();
        }
    }
}
