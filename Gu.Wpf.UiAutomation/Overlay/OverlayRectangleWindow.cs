namespace Gu.Wpf.UiAutomation.Overlay
{
    using System;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Gu.Wpf.UiAutomation.Internals;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class OverlayRectangleWindow : Window
    {
        private static OverlayRectangleWindow? window;

        private OverlayRectangleWindow(Rect rectangle, Color color, TimeSpan duration)
        {
            AutomationProperties.SetAutomationId(this, "Gu.Wpf.UiAutomationOverlayWindow");
            AutomationProperties.SetName(this, "Gu.Wpf.UiAutomationOverlayWindow");
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            this.Topmost = true;
            this.ShowActivated = false;
            this.ShowInTaskbar = false;
            this.Background = Brushes.Transparent;
            this.Top = rectangle.Top;
            this.Left = rectangle.Left;
            this.Width = rectangle.Width;
            this.Height = rectangle.Height;
            var borderBrush = new SolidColorBrush(color);
            borderBrush.Freeze();
            this.Content = new Border { BorderThickness = new Thickness(2), BorderBrush = borderBrush };
            var timer = new DispatcherTimer { Interval = duration };
            timer.Tick += this.TimerTick!;
            timer.Start();
        }

        public static void Show(Rect rectangle, Color color, TimeSpan duration)
        {
            Show(() =>
            {
                window?.Close();
                window = new OverlayRectangleWindow(rectangle, color, duration);
                window.Show();
            });
        }

        public static void ShowBlocking(Rect rectangle, Color color, TimeSpan duration)
        {
            Show(() =>
            {
                window?.Close();
                window = new OverlayRectangleWindow(rectangle, color, duration);
                _ = window.ShowDialog();
            });
        }

        public static void CloseCurrent()
        {
            window?.Dispatcher.Invoke(() => window.Close());
        }

        /// <inheritdoc/>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Make the window click-thru
            this.SetWindowTransparent();
        }

        private static void Show(Action action)
        {
            var dispatcher = WpfDispatcher.Create();
            dispatcher!.Invoke(action);
            dispatcher.InvokeShutdown();
            _ = dispatcher.Thread.Join(1000);
        }

        private void SetWindowTransparent()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            var extendedStyle = User32.GetWindowLong(hwnd, WindowLongParam.GWL_EXSTYLE);
            _ = User32.SetWindowLong(hwnd, WindowLongParam.GWL_EXSTYLE, extendedStyle | WindowStyles.WS_EX_TRANSPARENT);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var timer = (DispatcherTimer)sender;
            timer.Tick -= this.TimerTick!;
            timer.Stop();
            this.Close();
            window = null;
        }
    }
}
