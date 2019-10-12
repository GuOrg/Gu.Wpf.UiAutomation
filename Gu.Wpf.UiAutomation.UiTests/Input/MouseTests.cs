namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using NUnit.Framework;

    public class MouseTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string WindowName = "MouseWindow";

        [SetUp]
        public void SetUp()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                Wait.UntilInputIsProcessed();
                app.MainWindow.FindButton("Clear").Click();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Position()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.TopLeft;
                Wait.UntilInputIsProcessed();
                var expected = new List<string>
                {
                    "MouseEnter Position: 0,0",
                    "PreviewMouseMove Position: 0,0",
                    "MouseMove Position: 0,0",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());

                Mouse.Position = mouseArea.Bounds.Center();
                Wait.UntilInputIsProcessed();
                expected.AddRange(new[]
                {
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                });

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void MoveBy()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                Mouse.MoveBy(-20, 0);
                Mouse.MoveBy(0, -20);
                Mouse.MoveBy(20, 20);
                var expected = new[]
                {
                    "MouseEnter Position: 250,300",
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                    "PreviewMouseMove Position: 230,300",
                    "MouseMove Position: 230,300",
                    "PreviewMouseMove Position: 230,280",
                    "MouseMove Position: 230,280",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void MoveTo()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");

                Mouse.Position = mouseArea.Bounds.Center();
                Mouse.MoveTo(mouseArea.Bounds.Center() + new Vector(-20, 0));
                Mouse.MoveTo(mouseArea.Bounds.Center() + new Vector(-20, -20));
                Mouse.MoveTo(mouseArea.Bounds.Center());
                var expected = new[]
                {
                    "MouseEnter Position: 250,300",
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                    "PreviewMouseMove Position: 230,300",
                    "MouseMove Position: 230,300",
                    "PreviewMouseMove Position: 230,280",
                    "MouseMove Position: 230,280",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [TestCase(0)]
        [TestCase(200)]
        public void MoveToWithDuration(int milliseconds)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");

                Mouse.Position = mouseArea.Bounds.Center();
                Mouse.MoveTo(mouseArea.Bounds.Center() + new Vector(-20, 0), TimeSpan.FromMilliseconds(milliseconds));
                Mouse.MoveTo(mouseArea.Bounds.Center() + new Vector(-20, -20), TimeSpan.FromMilliseconds(milliseconds));
                Mouse.MoveTo(mouseArea.Bounds.Center(), TimeSpan.FromMilliseconds(milliseconds));
                Wait.UntilInputIsProcessed();
                var expected = new[]
                {
                    "MouseEnter Position: 250,300",
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                    "PreviewMouseMove Position: 230,300",
                    "MouseMove Position: 230,300",
                    "PreviewMouseMove Position: 230,280",
                    "MouseMove Position: 230,280",
                };

                if (milliseconds == 0)
                {
                    CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
                }
                else
                {
                    CollectionAssert.IsSubsetOf(expected, events.Items.Select(x => x.Text).ToArray());
                }
            }
        }

        [TestCase(2000)]
        [TestCase(double.PositiveInfinity)]
        public void MoveToWithSpeed(double speed)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");

                Mouse.Position = mouseArea.Bounds.Center();
                Mouse.MoveTo(mouseArea.Bounds.Center() + new Vector(-20, 0), speed);
                Mouse.MoveTo(mouseArea.Bounds.Center() + new Vector(-20, -20), speed);
                Mouse.MoveTo(mouseArea.Bounds.Center(), speed);
                Wait.UntilInputIsProcessed();

                if (double.IsInfinity(speed))
                {
                    var expected = new[]
                    {
                        "MouseEnter Position: 250,300",
                        "PreviewMouseMove Position: 250,300",
                        "MouseMove Position: 250,300",
                        "PreviewMouseMove Position: 230,300",
                        "MouseMove Position: 230,300",
                        "PreviewMouseMove Position: 230,280",
                        "MouseMove Position: 230,280",
                    };
                    CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
                }
                else
                {
                    var expected = new[]
                    {
                        "MouseEnter Position: 250,300",
                        "PreviewMouseMove Position: 250,300",
                        "MouseMove Position: 250,300",
                        "PreviewMouseMove Position: 230,300",
                        "MouseMove Position: 230,300",
                        "PreviewMouseMove Position: 230,280",
                        "MouseMove Position: 230,280",
                    };
                    CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
                }
            }
        }

        [TestCase(0)]
        [TestCase(200)]
        public void MoveByWithDuration(int milliseconds)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");

                Mouse.Position = mouseArea.Bounds.Center();
                Mouse.MoveBy(new Vector(-20, 0), TimeSpan.FromMilliseconds(milliseconds));
                Mouse.MoveBy(new Vector(0, -20), TimeSpan.FromMilliseconds(milliseconds));
                Mouse.MoveBy(new Vector(20, 20), TimeSpan.FromMilliseconds(milliseconds));
                Wait.UntilInputIsProcessed();

                if (milliseconds == 0)
                {
                    var expected = new[]
                    {
                        "MouseEnter Position: 250,300",
                        "PreviewMouseMove Position: 250,300",
                        "MouseMove Position: 250,300",
                        "PreviewMouseMove Position: 230,300",
                        "MouseMove Position: 230,300",
                        "PreviewMouseMove Position: 230,280",
                        "MouseMove Position: 230,280",
                    };
                    CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
                }
                else
                {
                    var expected = new[]
                    {
                        "MouseEnter Position: 250,300",
                        "PreviewMouseMove Position: 250,300",
                        "MouseMove Position: 250,300",
                        "PreviewMouseMove Position: 230,300",
                        "MouseMove Position: 230,300",
                        "PreviewMouseMove Position: 230,280",
                        "MouseMove Position: 230,280",
                    };
                    CollectionAssert.IsSubsetOf(expected, events.Items.Select(x => x.Text).ToArray());
                }
            }
        }

        [TestCase(200)]
        [TestCase(2000)]
        [TestCase(double.PositiveInfinity)]
        public void MoveByWithSpeed(double speed)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");

                Mouse.Position = mouseArea.Bounds.Center();
                Mouse.MoveBy(new Vector(-20, 0), speed);
                Mouse.MoveBy(new Vector(0, -20), speed);
                Mouse.MoveBy(new Vector(20, 20), speed);

                var expected = new[]
                {
                    "MouseEnter Position: 250,300",
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                    "PreviewMouseMove Position: 230,300",
                    "MouseMove Position: 230,300",
                    "PreviewMouseMove Position: 230,280",
                    "MouseMove Position: 230,280",
                };
                CollectionAssert.IsSubsetOf(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void ClickMouseButtonLeft()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Click(MouseButton.Left, mouseArea.Bounds.Center());
                var expected = new[]
                {
                    "MouseEnter Position: 250,300",
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                    "PreviewMouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "MouseDown Position: 250,300",
                    "PreviewMouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "PreviewMouseUp Position: 250,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "MouseUp Position: 250,300",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void ClickMouseButtonRight()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Click(MouseButton.Right, mouseArea.Bounds.Center());
                var expected = new[]
                {
                    "MouseEnter Position: 250,300",
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                    "PreviewMouseRightButtonDown Position: 250,300 Button: Right Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Right Pressed",
                    "MouseRightButtonDown Position: 250,300 Button: Right Pressed",
                    "MouseDown Position: 250,300",
                    "PreviewMouseRightButtonUp Position: 250,300 Button: Right Released",
                    "PreviewMouseUp Position: 250,300 Button: Right Released",
                    "MouseRightButtonUp Position: 250,300 Button: Right Released",
                    "MouseUp Position: 250,300",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void LeftClick()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.LeftClick(mouseArea.Bounds.Center());
                var expected = new[]
                {
                    "MouseEnter Position: 250,300",
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                    "PreviewMouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "MouseDown Position: 250,300",
                    "PreviewMouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "PreviewMouseUp Position: 250,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "MouseUp Position: 250,300",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void RightClick()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.RightClick(mouseArea.Bounds.Center());
                var expected = new[]
                {
                    "MouseEnter Position: 250,300",
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                    "PreviewMouseRightButtonDown Position: 250,300 Button: Right Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Right Pressed",
                    "MouseRightButtonDown Position: 250,300 Button: Right Pressed",
                    "MouseDown Position: 250,300",
                    "PreviewMouseRightButtonUp Position: 250,300 Button: Right Released",
                    "PreviewMouseUp Position: 250,300 Button: Right Released",
                    "MouseRightButtonUp Position: 250,300 Button: Right Released",
                    "MouseUp Position: 250,300",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void LeftDoubleClick()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.DoubleClick(MouseButton.Left, mouseArea.Bounds.Center());
                var expected = new[]
                {
                    "MouseEnter Position: 250,300",
                    "PreviewMouseMove Position: 250,300",
                    "MouseMove Position: 250,300",
                    "PreviewMouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "MouseDown Position: 250,300",
                    "PreviewMouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "PreviewMouseUp Position: 250,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "MouseUp Position: 250,300",
                    "PreviewMouseDoubleClick Position: 250,300 Button: Left Pressed",
                    "MouseDoubleClick Position: 250,300",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void LeftDownThenUp()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                Mouse.Down(MouseButton.Left);
                var expected = new List<string>
                {
                    "PreviewMouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "MouseDown Position: 250,300",
                };
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());

                Mouse.Up(MouseButton.Left);
                expected.AddRange(new[]
                {
                    "PreviewMouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "PreviewMouseUp Position: 250,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "MouseUp Position: 250,300",
                });

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void RightDownThenUp()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                Mouse.Down(MouseButton.Right);
                var expected = new List<string>
                {
                    "PreviewMouseRightButtonDown Position: 250,300 Button: Right Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Right Pressed",
                    "MouseRightButtonDown Position: 250,300 Button: Right Pressed",
                    "MouseDown Position: 250,300",
                };
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());

                Mouse.Up(MouseButton.Right);
                expected.AddRange(new[]
                {
                    "PreviewMouseRightButtonUp Position: 250,300 Button: Right Released",
                    "PreviewMouseUp Position: 250,300 Button: Right Released",
                    "MouseRightButtonUp Position: 250,300 Button: Right Released",
                    "MouseUp Position: 250,300",
                });
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void HoldLeft()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                List<string> expected;
                using (Mouse.Hold(MouseButton.Left))
                {
                    expected = new List<string>
                    {
                        "PreviewMouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                        "PreviewMouseDown Position: 250,300 Button: Left Pressed",
                        "MouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                        "MouseDown Position: 250,300",
                    };
                    CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
                }

                expected.AddRange(new[]
                {
                    "PreviewMouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "PreviewMouseUp Position: 250,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 250,300 Button: Left Released",
                    "MouseUp Position: 250,300",
                });

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void HoldRight()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                List<string> expected;
                using (Mouse.Hold(MouseButton.Right))
                {
                    expected = new List<string>
                    {
                        "PreviewMouseRightButtonDown Position: 250,300 Button: Right Pressed",
                        "PreviewMouseDown Position: 250,300 Button: Right Pressed",
                        "MouseRightButtonDown Position: 250,300 Button: Right Pressed",
                        "MouseDown Position: 250,300",
                    };
                }

                expected.AddRange(new[]
                {
                    "PreviewMouseRightButtonUp Position: 250,300 Button: Right Released",
                    "PreviewMouseUp Position: 250,300 Button: Right Released",
                    "MouseRightButtonUp Position: 250,300 Button: Right Released",
                    "MouseUp Position: 250,300",
                });
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void Drag()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                Mouse.Drag(MouseButton.Left, mouseArea.Bounds.Center(), mouseArea.Bounds.Center() + new Vector(10, 20));

                var expected = new[]
                {
                    "PreviewMouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "MouseDown Position: 250,300",
                    "PreviewMouseMove Position: 260,320",
                    "MouseMove Position: 260,320",
                    "PreviewMouseLeftButtonUp Position: 260,320 Button: Left Released",
                    "PreviewMouseUp Position: 260,320 Button: Left Released",
                    "MouseLeftButtonUp Position: 260,320 Button: Left Released",
                    "MouseUp Position: 260,320",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [TestCase(0)]
        [TestCase(200)]
        public void DragWithDuration(int milliseconds)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                Mouse.Drag(MouseButton.Left, mouseArea.Bounds.Center(), mouseArea.Bounds.Center() + new Vector(10, 20), TimeSpan.FromMilliseconds(milliseconds));
                Wait.UntilInputIsProcessed();
                var expected = new[]
                {
                    "PreviewMouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "MouseDown Position: 250,300",
                    "PreviewMouseMove Position: 260,320",
                    "MouseMove Position: 260,320",
                    "PreviewMouseLeftButtonUp Position: 260,320 Button: Left Released",
                    "PreviewMouseUp Position: 260,320 Button: Left Released",
                    "MouseLeftButtonUp Position: 260,320 Button: Left Released",
                    "MouseUp Position: 260,320",
                };

                if (milliseconds == 0)
                {
                    CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
                }
                else
                {
                    CollectionAssert.IsSubsetOf(expected, events.Items.Select(x => x.Text).ToArray());
                }
            }
        }

        [Test]
        public void DragDragHorizontally()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                Mouse.DragHorizontally(MouseButton.Left, mouseArea.Bounds.Center(), 10);

                var expected = new[]
                {
                    "PreviewMouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "MouseDown Position: 250,300",
                    "PreviewMouseMove Position: 260,300",
                    "MouseMove Position: 260,300",
                    "PreviewMouseLeftButtonUp Position: 260,300 Button: Left Released",
                    "PreviewMouseUp Position: 260,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 260,300 Button: Left Released",
                    "MouseUp Position: 260,300",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void DragVertically()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                Mouse.DragVertically(MouseButton.Left, mouseArea.Bounds.Center(), 10);

                var expected = new[]
                {
                    "PreviewMouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 250,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 250,300 Button: Left Pressed",
                    "MouseDown Position: 250,300",
                    "PreviewMouseMove Position: 250,310",
                    "MouseMove Position: 250,310",
                    "PreviewMouseLeftButtonUp Position: 250,310 Button: Left Released",
                    "PreviewMouseUp Position: 250,310 Button: Left Released",
                    "MouseLeftButtonUp Position: 250,310 Button: Left Released",
                    "MouseUp Position: 250,310",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [TestCase(-2, "-240")]
        [TestCase(-1, "-120")]
        [TestCase(1, "120")]
        [TestCase(2, "240")]
        public void Scroll(int scroll, string expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                Mouse.Scroll(scroll);
                var expecteds = new[]
                {
                    $"PreviewMouseWheel Position: 250,300 {expected}",
                    $"MouseWheel Position: 250,300 {expected}",
                };

                CollectionAssert.AreEqual(expecteds, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [TestCase(-2, "-240")]
        [TestCase(-1, "-120")]
        [TestCase(1, "120")]
        [TestCase(2, "240")]
        public void HorizontalScroll(int scroll, string expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.Center();
                app.MainWindow.FindButton("Clear").Invoke();

                Mouse.HorizontalScroll(scroll);
                Assert.Inconclusive($"Not sure if we can detect any events here. Expected {expected}");
                var expecteds = new[]
                {
                    $"PreviewMouseWheel Position: 250,300 {expected}",
                    $"MouseWheel Position: 250,300 {expected}",
                };

                CollectionAssert.AreEqual(expecteds, events.Items.Select(x => x.Text).ToArray());
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static void Dump(ListBox events)
        {
            foreach (var item in events.Items)
            {
                Console.WriteLine($"\"{item.Text}\",");
            }
        }
    }
}
