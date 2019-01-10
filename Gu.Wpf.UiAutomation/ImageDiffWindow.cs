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
            var expectedImage = CreateImage(expected);
            root.Children.Add(expectedImage);

            var actualImage = CreateImage(actual);
            root.Children.Add(actualImage);

            var buttonGrid = new UniformGrid
            {
                Rows = 1,
            };
            Grid.SetRow(buttonGrid, 1);
            var expectedButton = CreateToggleButton("Expected", expectedImage);
            buttonGrid.Children.Add(expectedButton);

            var actualButton = CreateToggleButton("Actual", actualImage);
            buttonGrid.Children.Add(actualButton);
            root.Children.Add(buttonGrid);

            BindOpacity(expectedImage, expectedButton, actualButton);
            BindOpacity(actualImage, expectedButton, actualButton);
            this.Content = root;
        }

        public static void Show(Bitmap expected, Bitmap actual)
        {
            using (var startedEvent = new ManualResetEventSlim(initialState: false))
            {
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
                    _ = window.ShowDialog();
                });

                dispatcher.InvokeShutdown();
                _ = uiThread.Join(1000);
            }
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

        private static System.Windows.Controls.Image CreateImage(Bitmap bitmap)
        {
            var image = new System.Windows.Controls.Image
            {
                Source = CreateBitmapSource(bitmap),
                Opacity = 0.5,
            };

            Grid.SetRow(image, 0);
            return image;
        }

        private static System.Windows.Controls.Primitives.ToggleButton CreateToggleButton(string content, System.Windows.Controls.Image image)
        {
            var actualButton = new System.Windows.Controls.Primitives.ToggleButton
            {
                Content = content,
                IsChecked = true,
            };
            _ = BindingOperations.SetBinding(
                                    image,
                                    VisibilityProperty,
                                    new Binding
                                    {
                                        Path = new PropertyPath("IsChecked"),
                                        Mode = BindingMode.TwoWay,
                                        Converter = new BooleanToVisibilityConverter(),
                                        Source = actualButton,
                                    });
            return actualButton;
        }

        private static void BindOpacity(
            System.Windows.Controls.Image image,
            System.Windows.Controls.Primitives.ToggleButton expectedButton,
            System.Windows.Controls.Primitives.ToggleButton actualButton)
        {
            _ = BindingOperations.SetBinding(
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
                                             Source = expectedButton,
                                         },
                                         new Binding
                                         {
                                             Path = new PropertyPath("IsChecked"),
                                             Mode = BindingMode.OneWay,
                                             Source = actualButton,
                                         },
                                      },
                                  });
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
