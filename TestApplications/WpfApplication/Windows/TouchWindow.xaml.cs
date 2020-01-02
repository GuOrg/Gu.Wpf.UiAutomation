namespace WpfApplication.Windows
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;

    public partial class TouchWindow : Window
    {
        public TouchWindow()
        {
            this.InitializeComponent();
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            this.Events.Items.Clear();
        }

        private void OnTouch(object sender, TouchEventArgs e)
        {
            this.Events.Items.Add($"{e.RoutedEvent.Name} Position: {e.GetTouchPoint((IInputElement)e.Source).Position.ToString(CultureInfo.InvariantCulture)}");
        }

        private void OnManipulation(object sender, InputEventArgs e)
        {
            this.Events.Items.Add($"{e.RoutedEvent.Name}");
        }

        private void OnStylusSystemGesture(object sender, StylusSystemGestureEventArgs e)
        {
            this.Events.Items.Add($"{e.RoutedEvent.Name} SystemGesture: {e.SystemGesture}");
        }
    }
}
