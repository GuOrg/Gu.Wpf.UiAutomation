// ReSharper disable RedundantNameQualifier
namespace Gu.Wpf.UiAutomation
{
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    public class ImageDiffWindow : System.Windows.Window
    {
        private ImageDiffWindow(Bitmap expected, Bitmap actual)
        {
            AutomationProperties.SetAutomationId(this, "Gu.Wpf.UiAutomationOverlayWindow");
            AutomationProperties.SetName(this, "Gu.Wpf.UiAutomationOverlayWindow");
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.Topmost = true;
            this.ShowActivated = false;
            this.Title = "Image diff";
            this.Background = System.Windows.Media.Brushes.Gray;
            this.Content = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                },
                Children =
                {
                    new Grid
                    {
                        Margin = new Thickness(10),
                        Background = System.Windows.Media.Brushes.Transparent,
                        Children =
                        {
                            CreateImage(expected, nameof(ImageDiffViewModel.ExpectedVisibility)),
                            CreateImage(actual, nameof(ImageDiffViewModel.ActualVisibility)),
                        },
                    },
                    CreateButtonGrid(),
                },
            };

            var viewModel = new ImageDiffViewModel();
            this.DataContext = viewModel;
            this.KeyDown += (_, e) =>
            {
                switch (e.Key)
                {
                    case System.Windows.Input.Key.Left:
                        if (viewModel.Expected)
                        {
                            viewModel.Both = true;
                        }
                        else if (viewModel.Actual)
                        {
                            viewModel.Expected = true;
                        }
                        else if (viewModel.Both)
                        {
                            viewModel.Actual = true;
                        }

                        break;

                    case System.Windows.Input.Key.Right:
                        if (viewModel.Expected)
                        {
                            viewModel.Actual = true;
                        }
                        else if (viewModel.Actual)
                        {
                            viewModel.Both = true;
                        }
                        else if (viewModel.Both)
                        {
                            viewModel.Expected = true;
                        }

                        break;
                }
            };
        }

        public static void Show(Bitmap expected, Bitmap actual)
        {
            if (expected is null)
            {
                throw new System.ArgumentNullException(nameof(expected));
            }

            if (actual is null)
            {
                throw new System.ArgumentNullException(nameof(actual));
            }

            using var startedEvent = new ManualResetEventSlim(initialState: false);
            System.Windows.Threading.Dispatcher? dispatcher = null;
            var uiThread = new Thread(() =>
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));
                dispatcher = Dispatcher.CurrentDispatcher;
                //// ReSharper disable once AccessToDisposedClosure
                startedEvent.Set();
                Dispatcher.Run();
            });

            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = true;

            uiThread.Start();
            startedEvent.Wait();
            dispatcher!.Invoke(() =>
            {
                var window = new ImageDiffWindow(expected, actual);
                _ = window.ShowDialog();
            });

            dispatcher.InvokeShutdown();
            _ = uiThread.Join(1000);
        }

        private static System.Windows.Controls.Image CreateImage(Bitmap bitmap, string visibilityPropertyName)
        {
            var image = new System.Windows.Controls.Image
            {
                Source = CreateBitmapSource(bitmap),
                Height = bitmap.Height,
                Width = bitmap.Width,
                InputBindings =
                {
                    new MouseBinding(
                        ApplicationCommands.Save,
                        new MouseGesture(MouseAction.LeftDoubleClick)),
                },
                CommandBindings =
                {
                    new CommandBinding(
                        ApplicationCommands.Save,
                        (sender, args) =>
                        {
                            var dialog = new Microsoft.Win32.SaveFileDialog();
                            if (dialog.ShowDialog() == true)
                            {
                                bitmap.Save(dialog.FileName);
                            }
                        }),
                },
            };

            _ = BindingOperations.SetBinding(
                image,
                System.Windows.Controls.Image.VisibilityProperty,
                new Binding
                {
                    Path = new PropertyPath(visibilityPropertyName),
                    Mode = BindingMode.OneWay,
                });

            _ = BindingOperations.SetBinding(
                image,
                System.Windows.Controls.Image.OpacityProperty,
                new Binding
                {
                    Path = new PropertyPath(nameof(ImageDiffViewModel.Opacity)),
                    Mode = BindingMode.OneWay,
                });

            Grid.SetRow(image, 0);
            return image;
        }

        private static UniformGrid CreateButtonGrid()
        {
            var grid = new UniformGrid
            {
                Rows = 1,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = System.Windows.Media.Brushes.White,
                Children =
                {
                    CreateButton(nameof(ImageDiffViewModel.Expected)),
                    CreateButton(nameof(ImageDiffViewModel.Actual)),
                    CreateButton(nameof(ImageDiffViewModel.Both)),
                },
            };

            Grid.SetRow(grid, 1);
            return grid;

            static System.Windows.Controls.RadioButton CreateButton(string content)
            {
                var button = new System.Windows.Controls.RadioButton
                {
                    Content = content,
                };

                _ = BindingOperations.SetBinding(
                    button,
                    System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty,
                    new Binding
                    {
                        Path = new PropertyPath(content),
                        Mode = BindingMode.TwoWay,
                    });
                return button;
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

#pragma warning disable CA1034 // Nested types should not be visible
        public class ImageDiffViewModel : INotifyPropertyChanged
#pragma warning restore CA1034 // Nested types should not be visible
        {
            private bool expected;
            private bool actual;
            private bool both = true;

            public event PropertyChangedEventHandler? PropertyChanged;

            public bool Expected
            {
                get => this.expected;
                set
                {
                    if (value == this.expected)
                    {
                        return;
                    }

                    this.expected = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(this.ExpectedVisibility));
                }
            }

            public bool Actual
            {
                get => this.actual;
                set
                {
                    if (value == this.actual)
                    {
                        return;
                    }

                    this.actual = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(this.ActualVisibility));
                }
            }

            public bool Both
            {
                get => this.both;
                set
                {
                    if (value == this.both)
                    {
                        return;
                    }

                    this.both = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(this.Opacity));
                    this.OnPropertyChanged(nameof(this.ActualVisibility));
                    this.OnPropertyChanged(nameof(this.ExpectedVisibility));
                }
            }

            public Visibility ExpectedVisibility => this.Expected || this.Both ? Visibility.Visible : Visibility.Hidden;

            public Visibility ActualVisibility => this.Actual || this.Both ? Visibility.Visible : Visibility.Hidden;

            public double Opacity => this.Both ? 0.5 : 1;

            protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
