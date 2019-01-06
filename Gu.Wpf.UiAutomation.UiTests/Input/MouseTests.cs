namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using NUnit.Framework;

    [TestFixture]
    public class MouseTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string WindowName = "MouseWindow";

        [SetUp]
        public void SetUp()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
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
                var expected = new List<string>
                {
                    "MouseEnter Position: 0,0",
                    "PreviewMouseMove Position: 0,0",
                    "MouseMove Position: 0,0",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());

                Mouse.Position = mouseArea.Bounds.Center();
                expected.AddRange(new[]
                {
                    "PreviewMouseMove Position: 100,300",
                    "MouseMove Position: 100,300",
                });

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void MoveTest()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var mouseArea = window.FindGroupBox("Mouse area");
                var events = window.FindListBox("Events");
                Mouse.Position = mouseArea.Bounds.TopLeft;

                Mouse.MoveBy(800, 0);
                Mouse.MoveBy(0, 400);
                Mouse.MoveBy(-400, -200);
                var expected = new[]
                {
                    "MouseEnter Position: 0,0",
                    "PreviewMouseMove Position: 0,0",
                    "MouseMove Position: 0,0",
                    "PreviewMouseMove Position: 80,0",
                    "MouseMove Position: 80,0",
                    "PreviewMouseMove Position: 160,0",
                    "MouseMove Position: 160,0",
                    "MouseLeave Position: 240,0",
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
                Mouse.Click(MouseButton.Left, mouseArea.Bounds.Center());
                var expected = new[]
                {
                    "MouseEnter Position: 100,300",
                    "PreviewMouseMove Position: 100,300",
                    "MouseMove Position: 100,300",
                    "PreviewMouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 100,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "MouseDown Position: 100,300",
                    "PreviewMouseLeftButtonUp Position: 100,300 Button: Left Released",
                    "PreviewMouseUp Position: 100,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 100,300 Button: Left Released",
                    "MouseUp Position: 100,300",
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
                Mouse.Click(MouseButton.Right, mouseArea.Bounds.Center());
                var expected = new[]
                {
                    "MouseEnter Position: 100,300",
                    "PreviewMouseMove Position: 100,300",
                    "MouseMove Position: 100,300",
                    "PreviewMouseRightButtonDown Position: 100,300 Button: Right Pressed",
                    "PreviewMouseDown Position: 100,300 Button: Right Pressed",
                    "MouseRightButtonDown Position: 100,300 Button: Right Pressed",
                    "MouseDown Position: 100,300",
                    "PreviewMouseRightButtonUp Position: 100,300 Button: Right Released",
                    "PreviewMouseUp Position: 100,300 Button: Right Released",
                    "MouseRightButtonUp Position: 100,300 Button: Right Released",
                    "MouseUp Position: 100,300",
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
                    "MouseEnter Position: 100,300",
                    "PreviewMouseMove Position: 100,300",
                    "MouseMove Position: 100,300",
                    "PreviewMouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 100,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "MouseDown Position: 100,300",
                    "PreviewMouseLeftButtonUp Position: 100,300 Button: Left Released",
                    "PreviewMouseUp Position: 100,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 100,300 Button: Left Released",
                    "MouseUp Position: 100,300",
                    "PreviewMouseDoubleClick Position: 100,300 Button: Left Pressed",
                    "MouseDoubleClick Position: 100,300",
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
                    "PreviewMouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 100,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "MouseDown Position: 100,300",
                };
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());

                Mouse.Up(MouseButton.Left);
                expected.AddRange(new[]
                {
                    "PreviewMouseLeftButtonUp Position: 100,300 Button: Left Released",
                    "PreviewMouseUp Position: 100,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 100,300 Button: Left Released",
                    "MouseUp Position: 100,300",
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
                    "PreviewMouseRightButtonDown Position: 100,300 Button: Right Pressed",
                    "PreviewMouseDown Position: 100,300 Button: Right Pressed",
                    "MouseRightButtonDown Position: 100,300 Button: Right Pressed",
                    "MouseDown Position: 100,300",
                };
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());

                Mouse.Up(MouseButton.Right);
                expected.AddRange(new[]
                {
                    "PreviewMouseRightButtonUp Position: 100,300 Button: Right Released",
                    "PreviewMouseUp Position: 100,300 Button: Right Released",
                    "MouseRightButtonUp Position: 100,300 Button: Right Released",
                    "MouseUp Position: 100,300",
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
                        "PreviewMouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                        "PreviewMouseDown Position: 100,300 Button: Left Pressed",
                        "MouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                        "MouseDown Position: 100,300",
                    };
                    CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
                }

                expected.AddRange(new[]
                {
                    "PreviewMouseLeftButtonUp Position: 100,300 Button: Left Released",
                    "PreviewMouseUp Position: 100,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 100,300 Button: Left Released",
                    "MouseUp Position: 100,300",
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
                        "PreviewMouseRightButtonDown Position: 100,300 Button: Right Pressed",
                        "PreviewMouseDown Position: 100,300 Button: Right Pressed",
                        "MouseRightButtonDown Position: 100,300 Button: Right Pressed",
                        "MouseDown Position: 100,300",
                    };
                }

                expected.AddRange(new[]
                {
                    "PreviewMouseRightButtonUp Position: 100,300 Button: Right Released",
                    "PreviewMouseUp Position: 100,300 Button: Right Released",
                    "MouseRightButtonUp Position: 100,300 Button: Right Released",
                    "MouseUp Position: 100,300",
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
                    "PreviewMouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 100,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "MouseDown Position: 100,300",
                    "PreviewMouseMove Position: 110,320",
                    "MouseMove Position: 110,320",
                    "PreviewMouseLeftButtonUp Position: 110,320 Button: Left Released",
                    "PreviewMouseUp Position: 110,320 Button: Left Released",
                    "MouseLeftButtonUp Position: 110,320 Button: Left Released",
                    "MouseUp Position: 110,320",
                };
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
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

                Mouse.DragHorizontally(MouseButton.Left, mouseArea.Bounds.Center(), 1);

                var expected = new[]
                {
                    "PreviewMouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 100,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "MouseDown Position: 100,300",
                    "PreviewMouseMove Position: 101,300",
                    "MouseMove Position: 101,300",
                    "PreviewMouseLeftButtonUp Position: 101,300 Button: Left Released",
                    "PreviewMouseUp Position: 101,300 Button: Left Released",
                    "MouseLeftButtonUp Position: 101,300 Button: Left Released",
                    "MouseUp Position: 101,300",
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

                Mouse.DragVertically(MouseButton.Left, mouseArea.Bounds.Center(), 1);

                var expected = new[]
                {
                    "PreviewMouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "PreviewMouseDown Position: 100,300 Button: Left Pressed",
                    "MouseLeftButtonDown Position: 100,300 Button: Left Pressed",
                    "MouseDown Position: 100,300",
                    "PreviewMouseMove Position: 100,301",
                    "MouseMove Position: 100,301",
                    "PreviewMouseLeftButtonUp Position: 100,301 Button: Left Released",
                    "PreviewMouseUp Position: 100,301 Button: Left Released",
                    "MouseLeftButtonUp Position: 100,301 Button: Left Released",
                    "MouseUp Position: 100,301",
                };
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
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
