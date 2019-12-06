// ReSharper disable RedundantNameQualifier
namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Gu.Wpf.UiAutomation.Internals;
    using Color = System.Drawing.Color;

    public class ImageDiffWindow : System.Windows.Window
    {
        private static readonly SolidColorBrush[] Backgrounds = new[]
        {
            System.Windows.Media.Brushes.Gray,
            System.Windows.Media.Brushes.White,
            System.Windows.Media.Brushes.Black,
            System.Windows.Media.Brushes.LightGreen,
        };

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
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
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
                            CreateImage(Diff(expected, actual), nameof(ImageDiffViewModel.DiffVisibility)),
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

                    case System.Windows.Input.Key.Up:
                        this.Background = NextBackground(1);
                        break;

                    case System.Windows.Input.Key.Down:
                        this.Background = NextBackground(-1);
                        break;
                }
            };
            this.ToolTip = new System.Windows.Controls.ToolTip
            {
                Content = new System.Windows.Controls.TextBlock
                {
                    Text = "Use left and right arrows to change image. Up and down to change background.",
                },
            };

            SolidColorBrush NextBackground(int increment)
            {
                var index = Array.IndexOf(Backgrounds, this.Background) + increment;
                if (index >= Backgrounds.Length)
                {
                    index = 0;
                }

                if (index < 0)
                {
                    index = Backgrounds.Length - 1;
                }

                return Backgrounds[index];
            }
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

            var dispatcher = WpfDispatcher.Create();
            dispatcher!.Invoke(() =>
            {
                var window = new ImageDiffWindow(expected, actual);
                _ = window.ShowDialog();
            });

            dispatcher.InvokeShutdown();
            _ = dispatcher.Thread.Join(1000);
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
                ContextMenu = new System.Windows.Controls.ContextMenu
                {
                    Items =
                    {
                        new System.Windows.Controls.MenuItem
                        {
                            Header = "Save",
                            Command = ApplicationCommands.Save,
                            CommandBindings =
                            {
                                SaveBinding(),
                            },
                        },
                    },
                },
                CommandBindings =
                {
                    SaveBinding(),
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

            CommandBinding SaveBinding()
            {
                return new CommandBinding(
                    ApplicationCommands.Save,
                    (sender, args) =>
                    {
                        var dialog = new Microsoft.Win32.SaveFileDialog();
                        if (dialog.ShowDialog() == true)
                        {
                            bitmap.Save(dialog.FileName);
                        }
                    });
            }
        }

        private static UniformGrid CreateButtonGrid()
        {
            var grid = new UniformGrid
            {
                Rows = 1,
                Background = System.Windows.Media.Brushes.White,
                Children =
                {
                    CreateButton(nameof(ImageDiffViewModel.Expected)),
                    CreateButton(nameof(ImageDiffViewModel.Actual)),
                    CreateButton(nameof(ImageDiffViewModel.Both)),
                    CreateButton(nameof(ImageDiffViewModel.Diff)),
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
            using var memory = new MemoryStream();
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            memory.Position = 0;
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        private static Bitmap Diff(Bitmap expected, Bitmap actual)
        {
            var diff = new Bitmap(Math.Min(expected.Width, actual.Width), Math.Min(expected.Height, actual.Height));
            for (var x = 0; x < diff.Size.Width; x++)
            {
                for (var y = 0; y < diff.Size.Height; y++)
                {
                    var ep = expected.GetPixel(x, y);
                    var ap = actual.GetPixel(x, y);
                    diff.SetPixel(x, y, System.Drawing.Color.FromArgb(
                        Diff(x => x.A),
                        Diff(x => x.R),
                        Diff(x => x.G),
                        Diff(x => x.B)));

                    int Diff(Func<Color, byte> func)
                    {
                        return Math.Abs(func(ep) - func(ap));
                    }
                }
            }

            return diff;
        }

#pragma warning disable CA1034 // Nested types should not be visible
        public class ImageDiffViewModel : INotifyPropertyChanged
#pragma warning restore CA1034 // Nested types should not be visible
        {
            private bool expected;
            private bool actual;
            private bool both = true;
            private bool diff;

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

            public bool Diff
            {
                get => this.diff;
                set
                {
                    if (value == this.diff)
                    {
                        return;
                    }

                    this.diff = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(this.DiffVisibility));
                }
            }

            public Visibility ExpectedVisibility => this.expected || this.both ? Visibility.Visible : Visibility.Hidden;

            public Visibility ActualVisibility => this.actual || this.both ? Visibility.Visible : Visibility.Hidden;

            public Visibility DiffVisibility => this.diff ? Visibility.Visible : Visibility.Hidden;

            public double Opacity => this.Both ? 0.5 : 1;

            protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
