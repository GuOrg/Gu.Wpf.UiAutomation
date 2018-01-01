namespace Gu.Wpf.UiAutomation
{
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    public class ImageDiffWindow : System.Windows.Window
    {
        private readonly System.Windows.Controls.Image expectedImage;
        private readonly System.Windows.Controls.Image actualImage;

        private ImageDiffWindow(Bitmap expected, Bitmap actual)
        {
            AutomationProperties.SetAutomationId(this, "Gu.Wpf.UiAutomationOverlayWindow");
            AutomationProperties.SetName(this, "Gu.Wpf.UiAutomationOverlayWindow");
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.Topmost = true;
            this.ShowActivated = false;
            this.ShowInTaskbar = false;
            this.Height = 400;
            this.Width = 400;
            var root = new Grid();
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.expectedImage = this.CreateImage(expected);
            root.Children.Add(this.expectedImage);

            this.actualImage = this.CreateImage(actual);
            root.Children.Add(this.actualImage);

            var visibilityButton = new System.Windows.Controls.Primitives.RepeatButton { Content = "Toggle visibility" };
            visibilityButton.Click += this.OnVisibilityButtonClick;
            Grid.SetRow(visibilityButton, 1);
            root.Children.Add(visibilityButton);
            this.Content = root;
        }

        public static void Show(Bitmap expected, Bitmap actual)
        {
            var startedEvent = new ManualResetEventSlim(initialState: false);
            System.Windows.Threading.Dispatcher dispatcher = null;
            var uiThread = new Thread(() =>
            {
                // Create and install a new dispatcher context
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));
                dispatcher = Dispatcher.CurrentDispatcher;

                // Signal that it is initialized
                startedEvent.Set();

                // Start the dispatcher processing
                Dispatcher.Run();
            });

            // Set the apartment state
            uiThread.SetApartmentState(ApartmentState.STA);

            // Make the thread a background thread
            uiThread.IsBackground = true;

            // Start the thread
            uiThread.Start();
            startedEvent.Wait();
            dispatcher.Invoke(() =>
            {
                var window = new ImageDiffWindow(expected, actual);
                window.ShowDialog();
            });

            dispatcher.InvokeShutdown();
            uiThread.Join(1000);
            startedEvent.Dispose();
        }

        private static BitmapSource CreateBitmapSource(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private System.Windows.Controls.Image CreateImage(Bitmap bitmap)
        {
            var image = new System.Windows.Controls.Image
                        {
                            Source = CreateBitmapSource(bitmap),
                            Opacity = 0.5,
                        };

            Grid.SetRow(image, 0);
            return image;
        }

        private void OnVisibilityButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (this.expectedImage.Visibility == Visibility.Visible &&
                this.actualImage.Visibility == Visibility.Visible)
            {
                this.actualImage.Visibility = Visibility.Hidden;
                this.expectedImage.Opacity = 1;
            }
            else if (this.actualImage.Visibility == Visibility.Hidden)
            {
                this.actualImage.Visibility = Visibility.Visible;
                this.actualImage.Opacity = 1;
                this.expectedImage.Visibility = Visibility.Hidden;
            }
            else if (this.expectedImage.Visibility == Visibility.Hidden)
            {
                this.actualImage.Visibility = Visibility.Visible;
                this.actualImage.Opacity = 0.5;
                this.expectedImage.Visibility = Visibility.Visible;
                this.expectedImage.Opacity = 0.5;
            }
        }
    }
}