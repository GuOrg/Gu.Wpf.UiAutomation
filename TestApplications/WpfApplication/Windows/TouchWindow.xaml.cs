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

        private void OnTouch(object sender, TouchEventArgs e)
        {
            this.Events.Items.Add($"{e.RoutedEvent.Name} Position: {e.GetTouchPoint((IInputElement)e.Source).Position.ToString(CultureInfo.InvariantCulture)}");
        }

        private void OnManipulation(object sender, InputEventArgs e)
        {
            this.Events.Items.Add($"{e.RoutedEvent.Name}");
        }
    }
}
