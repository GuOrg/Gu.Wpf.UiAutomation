namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    public class TouchBox
    {
        [Test]
        public void TapThenMoveOutside()
        {
            using (var app = Application.Launch("WpfApplication.exe", "EmptyWindow"))
            {
                var window = app.MainWindow;
                Console.WriteLine(Mouse.Position);
                Touch.Tap(window.Bounds.Center());
                Console.WriteLine(Mouse.Position);
                SendInput(POINT.Create(window.Bounds.TopLeft + new Vector(-1, -1)), MouseEventFlags.MOUSEEVENTF_MOVE);
                Console.WriteLine(Mouse.Position);
                //SendInput(new POINT(0, 0), MouseEventFlags.MOUSEEVENTF_MOVE);

                //var p = POINT.Create(window.Bounds.Center());
                //User32.SetCursorPos(p.X, p.Y);
            }
        }

        private static void SendInput(POINT p, MouseEventFlags flags)
        {
            var input = new INPUT
            {
                type = InputType.INPUT_MOUSE,
                u = new INPUTUNION
                {
                    mi = new MOUSEINPUT
                    {
                        dx = p.X,
                        dy = p.Y,
                        dwExtraInfo = User32.GetMessageExtraInfo(),
                        time = 0,
                        dwFlags = flags,
                    },
                },
            };

            if (User32.SendInput(1, new[] { input }, INPUT.Size) == 0)
            {
                throw new Win32Exception();
            }

            Wait.UntilInputIsProcessed();
        }
    }
}
