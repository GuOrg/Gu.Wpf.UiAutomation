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
            MessageBox.Show("Message text", "Caption text", MessageBoxButton.OKCancel);
        }

        private void OnShowMessageBoxYesNoCancelClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Message text", "Caption text", MessageBoxButton.YesNoCancel);
        }

        private void OnShowDialogClick(object sender, RoutedEventArgs e)
        {
            var window = new Dialog { Owner = this };
            window.ShowDialog();
        }

        private void OnSaveFileDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.ShowDialog(this);
        }

        private void OnOpenFileDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog(this);
        }
    }
}
