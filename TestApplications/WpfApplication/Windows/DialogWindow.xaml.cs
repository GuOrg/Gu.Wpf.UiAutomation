namespace WpfApplication.Windows
{
    using System.Windows;
    using Microsoft.Win32;

    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            this.InitializeComponent();
        }

        private void OnShowMessageBoxOKCancelClick(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("Message text", "Caption text", MessageBoxButton.OKCancel);
        }

        private void OnShowMessageBoxYesNoCancelClick(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("Message text", "Caption text", MessageBoxButton.YesNoCancel);
        }

        private void OnShowDialogClick(object sender, RoutedEventArgs e)
        {
            var window = new Dialog { Owner = this };
            _ = window.ShowDialog();
        }

        private void OnSaveFileDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            _ = dialog.ShowDialog(this);
        }

        private void OnOpenFileDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            _ = dialog.ShowDialog(this);
        }
    }
}
