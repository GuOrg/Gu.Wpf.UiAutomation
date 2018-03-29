// ReSharper disable RedundantNameQualifier
namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    public class ImageDiffWindow : System.Windows.Window
    {
        private readonly System.Windows.Controls.Image expectedImage;
        private readonly System.Windows.Controls.Image actualImage;
        private readonly System.Windows.Controls.Primitives.ToggleButton expectedButton;
        private readonly System.Windows.Controls.Primitives.ToggleButton actualButton;

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
            this.Title = "Image diff";
            var root = new Grid();
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.expectedImage = this.CreateImage(expected);
            root.Children.Add(this.expectedImage);

            this.actualImage = this.CreateImage(actual);
            root.Children.Add(this.actualImage);

            var buttonGrid = new UniformGrid
            {
                Rows = 1,
            };
            Grid.SetRow(buttonGrid, 1);
            this.expectedButton = new System.Windows.Controls.Primitives.ToggleButton
            {
                Content = "Expected",
                IsChecked = true,
            };
            BindingOperations.SetBinding(
                this.expectedImage,
                VisibilityProperty,
                new Binding
                {
                    Path = new PropertyPath("IsChecked"),
                    Mode = BindingMode.TwoWay,
                    Converter = new BooleanToVisibilityConverter(),
                    Source = this.expectedButton,
                });
            buttonGrid.Children.Add(this.expectedButton);

            this.actualButton = new System.Windows.Controls.Primitives.ToggleButton
            {
                Content = "actual",
                IsChecked = true,
            };
            BindingOperations.SetBinding(
                this.actualImage,
                VisibilityProperty,
                new Binding
                {
                    Path = new PropertyPath("IsChecked"),
                    Mode = BindingMode.TwoWay,
                    Converter = new BooleanToVisibilityConverter(),
                    Source = this.actualButton,
                });
            buttonGrid.Children.Add(this.actualButton);
            root.Children.Add(buttonGrid);

            this.BindOpacity(this.expectedImage);
            this.BindOpacity(this.actualImage);
            this.Content = root;
        }

        private void BindOpacity(System.Windows.Controls.Image image)
        {
            BindingOperations.SetBinding(
                image,
                OpacityProperty,
                new MultiBinding
                {
                    Converter = new AndToOpacityConverter(),
                    Bindings =
                    {
                        new Binding
                        {
                            Path = new PropertyPath("IsChecked"),
                            Mode = BindingMode.OneWay,
                            Source = this.expectedButton,
                        },
                        new Binding
                        {
                            Path = new PropertyPath("IsChecked"),
                            Mode = BindingMode.OneWay,
                            Source = this.actualButton,
                        },
                    }
                });
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
                // ReSharper disable once AccessToDisposedClosure
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

        private class AndToOpacityConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (values.Length == 2 &&
                    values[0] is bool b0 &&
                    values[1] is bool b1)
                {
                    return b0 && b1 ? 0.5 : 1;
                }

                return 1;
            }

            object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
