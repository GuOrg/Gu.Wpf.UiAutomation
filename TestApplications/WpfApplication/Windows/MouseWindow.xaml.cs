namespace WpfApplication.Windows
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;

    public partial class MouseWindow : Window
    {
        public MouseWindow()
        {
            this.InitializeComponent();
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            this.Events.Items.Clear();
        }

        private void OnMouseButtonEvent(object sender, MouseButtonEventArgs e)
        {
            this.Events.Items.Add($"{e.RoutedEvent.Name} Position: {e.GetPosition((IInputElement)e.Source).ToString(CultureInfo.InvariantCulture)} Button: {e.ChangedButton} {e.ButtonState}");
        }

        private void OnMouseEvent(object sender, MouseEventArgs e)
        {
            this.Events.Items.Add($"{e.RoutedEvent.Name} Position: {e.GetPosition((IInputElement)e.Source).ToString(CultureInfo.InvariantCulture)}");
        }
    }
}
