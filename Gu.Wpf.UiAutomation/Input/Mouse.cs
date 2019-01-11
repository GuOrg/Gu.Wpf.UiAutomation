namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.ComponentModel;
    using System.Security;
    using System.Security.Permissions;
    using System.Windows;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <summary>
    /// Mouse class to simulate mouse input.
    /// </summary>
    public static class Mouse
    {
        /// <summary>
        /// Time to add to the double click time to prevent false double clicks.
        /// </summary>
        private const int ExtraMillisecondsBecauseOfBugInWindows = 13;

        /// <summary>
        /// Number which defines one wheel "click" of the mouse wheel.
        /// </summary>
        private const int WheelDelta = 120;

        private static ButtonClick? lastClick;

        /// <summary>
        /// The speed the mouse is moved when for example dragging.
        /// Pixels per second.
        /// Default value is 2000.
        /// </summary>
        public static double MoveSpeed { get; set; } = 2000;

        /// <summary>
        /// Current position of the mouse cursor.
        /// </summary>
        public static Point Position
        {
            get
            {
                if (User32.GetCursorPos(out var p))
                {
                    return new Point(p.X, p.Y);
                }

#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                throw new Win32Exception();
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
            }

            set
            {
                if (!User32.SetCursorPos((int)value.X, (int)value.Y))
                {
                    throw new Win32Exception();
                }

                Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(10));
            }
        }

        /// <summary>
        /// Flag to indicate if the buttons are swapped (left-handed).
        /// </summary>
        public static bool AreButtonsSwapped => User32.GetSystemMetrics(SystemMetric.SM_SWAPBUTTON) != 0;

        /// <summary>
        /// Moves the mouse by a given delta from the current position.
        /// </summary>
        /// <param name="deltaX">The delta for the x-axis.</param>
        /// <param name="deltaY">The delta for the y-axis.</param>
        public static void MoveBy(int deltaX, int deltaY)
        {
            MoveTo(Position + new Vector(deltaX, deltaY));
        }

        /// <summary>
        /// Moves the mouse by a given delta from the current position.
        /// </summary>
        /// <param name="delta">The delta for the x-axis.</param>
        public static void MoveBy(Vector delta)
        {
            MoveTo(Position + delta);
        }

        /// <summary>
        /// Moves the mouse by a given delta from the current position.
        /// </summary>
        /// <param name="deltaX">The delta for the x-axis.</param>
        /// <param name="deltaY">The delta for the y-axis.</param>
        /// <param name="duration">The time to interpolate the move.</param>
        public static void MoveBy(int deltaX, int deltaY, TimeSpan duration)
        {
            MoveTo(Position + new Vector(deltaX, deltaY), duration);
        }

        /// <summary>
        /// Moves the mouse by a given delta from the current position.
        /// </summary>
        /// <param name="delta">The delta for the x-axis.</param>
        /// <param name="duration">The time to interpolate the move.</param>
        public static void MoveBy(Vector delta, TimeSpan duration)
        {
            MoveTo(Position + delta, duration);
        }

        /// <summary>
        /// Moves the mouse to a new position.
        /// </summary>
        /// <param name="newX">The new position on the x-axis.</param>
        /// <param name="newY">The new position on the y-axis.</param>
        public static void MoveTo(int newX, int newY)
        {
            var p = new Point(newX, newY);
            MoveTo(p, Interpolation.Duration(Position, p, MoveSpeed));
        }

        /// <summary>
        /// Moves the mouse to a new position.
        /// </summary>
        /// <param name="newPosition">The new position for the mouse.</param>
        public static void MoveTo(Point newPosition)
        {
            MoveTo((int)newPosition.X, (int)newPosition.Y);
        }

        /// <summary>
        /// Moves the mouse by a given delta from the current position.
        /// </summary>
        /// <param name="newPosition">The delta for the x-axis.</param>
        /// <param name="duration">The time to interpolate the move.</param>
        public static void MoveTo(Point newPosition, TimeSpan duration)
        {
            var interpolation = Interpolation.Start(Position, newPosition, duration);
            while (interpolation.TryGetPosition(out var pos))
            {
                if (!User32.SetCursorPos(pos.X, pos.Y))
                {
                    throw new Win32Exception();
                }

                Wait.For(TimeSpan.FromMilliseconds(10));
            }

            Wait.UntilInputIsProcessed();
        }

        /// <summary>
        /// Clicks the specified mouse button at the current location.
        /// </summary>
        /// <param name="mouseButton">The mouse button to click.</param>
        public static void Click(MouseButton mouseButton)
        {
            var position = POINT.Create(Position);
            if (lastClick is ButtonClick buttonClick &&
                buttonClick.Button == mouseButton &&
                Math.Abs(buttonClick.Point.X - position.X) < User32.GetSystemMetrics(SystemMetric.SM_CXDOUBLECLK) / 2 &&
                Math.Abs(buttonClick.Point.Y - position.Y) < User32.GetSystemMetrics(SystemMetric.SM_CYDOUBLECLK) / 2)
            {
                // Get the timeout needed to not fire a double click
                var timeout = User32.GetDoubleClickTime() - DateTime.UtcNow.Subtract(buttonClick.Time).Milliseconds;

                // Wait the needed time to prevent the double click
                if (timeout > 0)
                {
                    Wait.For(TimeSpan.FromMilliseconds(timeout + ExtraMillisecondsBecauseOfBugInWindows));
                }
            }

            Down(mouseButton);
            Up(mouseButton);
            lastClick = new ButtonClick(mouseButton, position);
            Wait.UntilInputIsProcessed();
        }

        /// <summary>
        /// Moves to a specific position and clicks the specified mouse button.
        /// </summary>
        /// <param name="mouseButton">The mouse button to click.</param>
        /// <param name="point">The position to move to before clicking.</param>
        public static void Click(MouseButton mouseButton, Point point)
        {
            Position = point;
            Click(mouseButton);
        }

        /// <summary>
        /// Double-clicks the specified mouse button at the current location.
        /// </summary>
        /// <param name="mouseButton">The mouse button to double-click.</param>
        public static void DoubleClick(MouseButton mouseButton)
        {
            Down(mouseButton);
            Up(mouseButton);
            Down(mouseButton);
            Up(mouseButton);
        }

        /// <summary>
        /// Moves to a specific position and double-clicks the specified mouse button.
        /// </summary>
        /// <param name="mouseButton">The mouse button to double-click.</param>
        /// <param name="point">The position to move to before double-clicking.</param>
        public static void DoubleClick(MouseButton mouseButton, Point point)
        {
            Position = point;
            DoubleClick(mouseButton);
        }

        /// <summary>
        /// Sends a mouse down command for the specified mouse button.
        /// Avoid calling this method as things get weird if Up is not called.
        /// Prefer using(Hold())  for drag operations.
        /// </summary>
        /// <param name="mouseButton">The mouse button to press.</param>
        public static void Down(MouseButton mouseButton)
        {
            if (GetCursorState() == CursorState.CURSOR_SUPPRESSED)
            {
                Restore();
            }

            switch (SwapButtonIfNeeded(mouseButton))
            {
                case MouseButton.Left:
                    SendInput(MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
                    break;
                case MouseButton.Middle:
                    SendInput(MouseEventFlags.MOUSEEVENTF_MIDDLEDOWN);
                    break;
                case MouseButton.Right:
                    SendInput(MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);
                    break;
                case MouseButton.XButton1:
                    SendInput(MouseEventFlags.MOUSEEVENTF_XDOWN, (int)MouseEventDataXButtons.XBUTTON1);
                    break;
                case MouseButton.XButton2:
                    SendInput(MouseEventFlags.MOUSEEVENTF_XDOWN, (int)MouseEventDataXButtons.XBUTTON2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mouseButton));
            }
        }

        /// <summary>
        /// Sends a mouse up command for the specified mouse button.
        /// </summary>
        /// <param name="mouseButton">The mouse button to release.</param>
        public static void Up(MouseButton mouseButton)
        {
            switch (SwapButtonIfNeeded(mouseButton))
            {
                case MouseButton.Left:
                    SendInput(MouseEventFlags.MOUSEEVENTF_LEFTUP);
                    break;
                case MouseButton.Middle:
                    SendInput(MouseEventFlags.MOUSEEVENTF_MIDDLEUP);
                    break;
                case MouseButton.Right:
                    SendInput(MouseEventFlags.MOUSEEVENTF_RIGHTUP);
                    break;
                case MouseButton.XButton1:
                    SendInput(MouseEventFlags.MOUSEEVENTF_XUP, (int)MouseEventDataXButtons.XBUTTON1);
                    break;
                case MouseButton.XButton2:
                    SendInput(MouseEventFlags.MOUSEEVENTF_XUP, (int)MouseEventDataXButtons.XBUTTON2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mouseButton));
            }
        }

        /// <summary>
        /// Hold the mouse button pressed for example during a drag operation.
        /// </summary>
        /// <param name="mouseButton">The <see cref="MouseButton"/>.</param>
        /// <returns>A <see cref="IDisposable"/> that releases the kay on dispose.</returns>
        public static IDisposable Hold(MouseButton mouseButton)
        {
            return new PressedButton(mouseButton);
        }

        /// <summary>
        /// Simulates scrolling of the mouse wheel up or down.
        /// </summary>
        public static void Scroll(double lines)
        {
            SendInput(MouseEventFlags.MOUSEEVENTF_WHEEL, (int)(WheelDelta * lines));
        }

        /// <summary>
        /// Simulates scrolling of the horizontal mouse wheel left or right.
        /// </summary>
        public static void HorizontalScroll(double lines)
        {
            SendInput(MouseEventFlags.MOUSEEVENTF_HWHEEL, (int)(WheelDelta * lines));
        }

        /// <summary>
        /// Drags the mouse horizontally in one step.
        /// </summary>
        /// <param name="mouseButton">The mouse button to use for dragging.</param>
        /// <param name="startingPoint">Starting point of the drag.</param>
        /// <param name="distance">The distance to drag, + for right, - for left.</param>
        public static void DragHorizontally(MouseButton mouseButton, Point startingPoint, double distance)
        {
            Drag(mouseButton, startingPoint, startingPoint + new Vector(distance, 0));
        }

        /// <summary>
        /// Drags the mouse vertically in one step.
        /// </summary>
        /// <param name="mouseButton">The mouse button to use for dragging.</param>
        /// <param name="startingPoint">Starting point of the drag.</param>
        /// <param name="distance">The distance to drag, + for down, - for up.</param>
        public static void DragVertically(MouseButton mouseButton, Point startingPoint, double distance)
        {
            Drag(mouseButton, startingPoint, startingPoint + new Vector(0, distance));
        }

        /// <summary>
        /// Drags the mouse in one step.
        /// </summary>
        /// <param name="mouseButton">The mouse button to use for dragging.</param>
        /// <param name="from">Start point of the drag.</param>
        /// <param name="to">End point for the drga.</param>
        public static void Drag(MouseButton mouseButton, Point from, Point to)
        {
            Drag(mouseButton, from, to, Interpolation.Duration(from, to, MoveSpeed));
        }

        /// <summary>
        /// Drags the mouse in one step.
        /// </summary>
        /// <param name="mouseButton">The mouse button to use for dragging.</param>
        /// <param name="from">Start point of the drag.</param>
        /// <param name="to">End point for the drga.</param>
        /// <param name="speed">The speed in pixels per second.</param>
        public static void Drag(MouseButton mouseButton, Point from, Point to, double speed)
        {
            Drag(mouseButton, from, to, Interpolation.Duration(from, to, speed));
        }

        /// <summary>
        /// Drags the mouse in one step.
        /// </summary>
        /// <param name="mouseButton">The mouse button to use for dragging.</param>
        /// <param name="from">Start point of the drag.</param>
        /// <param name="to">End point for the drga.</param>
        /// <param name="duration">The time to perform the drag.</param>
        public static void Drag(MouseButton mouseButton, Point from, Point to, TimeSpan duration)
        {
            Position = from;
            using (Hold(mouseButton))
            {
                var interpolation = Interpolation.Start(from, to, duration);
                while (interpolation.TryGetPosition(out var pos))
                {
                    if (!User32.SetCursorPos(pos.X, pos.Y))
                    {
                        throw new Win32Exception();
                    }

                    Wait.For(TimeSpan.FromMilliseconds(10));
                }
            }

            Wait.UntilInputIsProcessed();
        }

        public static void LeftClick()
        {
            Click(MouseButton.Left);
        }

        public static void LeftClick(Point point)
        {
            Click(MouseButton.Left, point);
        }

        public static void LeftDoubleClick()
        {
            DoubleClick(MouseButton.Left);
        }

        public static void LeftDoubleClick(Point point)
        {
            DoubleClick(MouseButton.Left, point);
        }

        public static void RightClick()
        {
            Click(MouseButton.Right);
        }

        public static void RightClick(Point point)
        {
            Click(MouseButton.Right, point);
        }

        public static void RightDoubleClick()
        {
            DoubleClick(MouseButton.Right);
        }

        public static void RightDoubleClick(Point point)
        {
            DoubleClick(MouseButton.Right, point);
        }

        /// <summary>
        /// Restore the mouse cursor.
        /// </summary>
        public static void Restore()
        {
            SendInput(new MOUSEINPUT
            {
                dx = (int)(Position.X * 65536) / User32.GetSystemMetrics(SystemMetric.SM_CXSCREEN),
                dy = (int)(Position.Y * 65536) / User32.GetSystemMetrics(SystemMetric.SM_CYSCREEN),
                dwExtraInfo = User32.GetMessageExtraInfo(),
                dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE,
            });

            Wait.UntilInputIsProcessed();
            SendInput(new MOUSEINPUT
            {
                dwExtraInfo = User32.GetMessageExtraInfo(),
                dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP,
            });

            Wait.UntilInputIsProcessed();
        }

        /// <summary>
        /// Get <see cref="CursorState"/>.
        /// </summary>
        /// <returns>The <see cref="CursorState"/>.</returns>
        public static CursorState GetCursorState()
        {
            var cursorInfo = CURSORINFO.Create();
            if (!User32.GetCursorInfo(ref cursorInfo))
            {
                throw new Win32Exception();
            }

            return cursorInfo.Flags;
        }

        /// <summary>
        /// Swaps the left/right button if <see cref="AreButtonsSwapped" /> is set.
        /// </summary>
        private static MouseButton SwapButtonIfNeeded(MouseButton mouseButton)
        {
            if (!AreButtonsSwapped)
            {
                return mouseButton;
            }

            switch (mouseButton)
            {
                case MouseButton.Left:
                    return MouseButton.Right;
                case MouseButton.Right:
                    return MouseButton.Left;
                default:
                    return mouseButton;
            }
        }

        private static void SendInput(MouseEventFlags flags, int data = 0)
        {
            SendInput(new MOUSEINPUT
            {
                dwFlags = flags,
                mouseData = data,
                dwExtraInfo = User32.GetMessageExtraInfo(),
            });
        }

        [PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
        private static void SendInput(MOUSEINPUT mouseInput)
        {
            // Demand the correct permissions
            var permissions = new PermissionSet(PermissionState.Unrestricted);
            permissions.Demand();

            var input = new INPUT
            {
                type = InputType.INPUT_MOUSE,
                u = new INPUTUNION
                {
                    mi = mouseInput,
                },
            };

            // Send the command
            if (User32.SendInput(1, new[] { input }, INPUT.Size) == 0)
            {
                throw new Win32Exception();
            }
        }

        private static void MoveAbsolute(Point p)
        {
            var input = new INPUT
            {
                type = InputType.INPUT_MOUSE,
                u = new INPUTUNION
                {
                    mi = new MOUSEINPUT
                    {
                        dx = (int)(p.X * 65536) / User32.GetSystemMetrics(SystemMetric.SM_CXSCREEN),
                        dy = (int)(p.Y * 65536) / User32.GetSystemMetrics(SystemMetric.SM_CYSCREEN),
                        dwExtraInfo = User32.GetMessageExtraInfo(),
                        time = 0,
                        dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE,
                    },
                },
            };

            if (User32.SendInput(1, new[] { input }, INPUT.Size) == 0)
            {
                throw new Win32Exception();
            }
        }

        private struct ButtonClick
        {
            internal readonly MouseButton Button;
            internal readonly POINT Point;
            internal readonly DateTime Time;

            public ButtonClick(MouseButton button, POINT point)
            {
                this.Button = button;
                this.Time = DateTime.UtcNow;
                this.Point = point;
            }
        }

        /// <summary>Disposable class which presses the button on creation and releases it on dispose.</summary>
        private class PressedButton : IDisposable
        {
            private readonly MouseButton button;

            public PressedButton(MouseButton button)
            {
                this.button = button;
                Down(button);
            }

            public void Dispose()
            {
                Up(this.button);
            }
        }
    }
}
