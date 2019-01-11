namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <summary>
    /// For simulating touch input.
    /// https://docs.microsoft.com/en-us/windows/desktop/api/_input_touchinjection/
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagpointer_touch_info
    /// https://social.technet.microsoft.com/wiki/contents/articles/6460.simulating-touch-input-in-windows-8-using-touch-injection-api.aspx.
    /// </summary>
    public static class Touch
    {
        private static POINTER_TOUCH_INFO[] contacts = new POINTER_TOUCH_INFO[1];

        static Touch()
        {
            if (!User32.InitializeTouchInjection())
            {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                throw new Win32Exception();
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
            }
        }

        /// <summary>
        /// The speed the mouse is moved when for example dragging.
        /// Pixels per second.
        /// Default value is 2000.
        /// </summary>
        public static double MoveSpeed { get; set; } = 2000;

        /// <summary>
        /// Initialize touch injection. Can be called many times.
        /// </summary>
        public static void Initialize()
        {
            // nop just for side effect of running static ctor.
        }

        /// <summary>
        /// Simulate tap.
        /// </summary>
        /// <param name="point">The position.</param>
        public static void Tap(Point point)
        {
            using (Down(point))
            {
                Wait.UntilInputIsProcessed();
            }
        }

        /// <summary>
        /// Simulate touch down.
        /// </summary>
        /// <param name="point">The position.</param>
        /// <returns>A disposable that calls Up when disposed.</returns>
        public static IDisposable Down(Point point)
        {
            contacts = new[] { POINTER_TOUCH_INFO.Create(point, POINTER_FLAG.DOWN | POINTER_FLAG.INRANGE | POINTER_FLAG.INCONTACT, 0) };

            if (!User32.InjectTouchInput(contacts.Length, contacts))
            {
                throw new Win32Exception();
            }

            return new ActionDisposable(() =>
            {
                contacts[0].PointerInfo.PointerFlags = POINTER_FLAG.UP;
                if (!User32.InjectTouchInput(contacts.Length, contacts))
                {
                    throw new Win32Exception();
                }

                contacts = null;
            });
        }

        /// <summary>
        /// Simulate touch down.
        /// </summary>
        /// <param name="fingers">The position.</param>
        /// <returns>A disposable that calls Up when disposed.</returns>
        public static IDisposable Down(TwoFingers fingers)
        {
            contacts = new[]
            {
                POINTER_TOUCH_INFO.Create(fingers.First, POINTER_FLAG.DOWN | POINTER_FLAG.INRANGE | POINTER_FLAG.INCONTACT, 0),
                POINTER_TOUCH_INFO.Create(fingers.Second, POINTER_FLAG.DOWN | POINTER_FLAG.INRANGE | POINTER_FLAG.INCONTACT, 1),
            };

            if (!User32.InjectTouchInput(contacts.Length, contacts))
            {
                throw new Win32Exception();
            }

            return new ActionDisposable(() =>
            {
                contacts[0].PointerInfo.PointerFlags = POINTER_FLAG.UP;
                contacts[1].PointerInfo.PointerFlags = POINTER_FLAG.UP;
                if (!User32.InjectTouchInput(contacts.Length, contacts))
                {
                    throw new Win32Exception();
                }

                contacts = null;
            });
        }

        /// <summary>
        /// Simulate touch up.
        /// </summary>
        public static void Up()
        {
            if (contacts == null)
            {
                throw new UiAutomationException("Call Touch.Down first.");
            }

            for (var i = 0; i < contacts.Length; i++)
            {
                contacts[i].PointerInfo.PointerFlags = POINTER_FLAG.UP;
            }

            if (!User32.InjectTouchInput(contacts.Length, contacts))
            {
                throw new Win32Exception();
            }

            contacts = null;
        }

        /// <summary>
        /// Simulate touch drag.
        /// Call <see cref="Down(Point)"/> before calling this method.
        /// This method is useful when dragging to multiple positions.
        /// </summary>
        /// <param name="position">The position.</param>
        public static void DragTo(Point position)
        {
            if (contacts == null ||
                contacts.Length != 1)
            {
                throw new UiAutomationException("Call Touch.Down first.");
            }

            var interpolation = Interpolation.Start(contacts[0].PointerInfo.PtPixelLocation, POINT.Create(position), MoveSpeed);
            while (interpolation.TryGetPosition(out var pos))
            {
                contacts[0].PointerInfo.PointerFlags = POINTER_FLAG.UPDATE | POINTER_FLAG.INRANGE | POINTER_FLAG.INCONTACT;
                contacts[0].PointerInfo.PtPixelLocation = pos;

                if (!User32.InjectTouchInput(1, contacts))
                {
                    throw new Win32Exception();
                }

                Wait.For(TimeSpan.FromMilliseconds(10));
            }
        }

        /// <summary>
        /// Simulate touch drag.
        /// </summary>
        /// <param name="from">The start position.</param>
        /// <param name="to">The end position.</param>
        public static void Drag(Point from, Point to)
        {
            Drag(from, to, Interpolation.Duration(from, to, MoveSpeed));
        }

        /// <summary>
        /// Simulate touch drag.
        /// </summary>
        /// <param name="from">The start position.</param>
        /// <param name="to">The end position.</param>
        /// <param name="speed">The speed for the drag pixels per second.</param>
        public static void Drag(Point from, Point to, double speed)
        {
            Drag(from, to, Interpolation.Duration(from, to, MoveSpeed));
        }

        /// <summary>
        /// Simulate touch drag.
        /// </summary>
        /// <param name="from">The start position.</param>
        /// <param name="to">The end position.</param>
        /// <param name="duration">The time to drag.</param>
        public static void Drag(Point from, Point to, TimeSpan duration)
        {
            using (Down(from))
            {
                var interpolation = Interpolation.Start(from, to, duration);
                while (interpolation.TryGetPosition(out var pos))
                {
                    contacts[0].PointerInfo.PointerFlags = POINTER_FLAG.UPDATE | POINTER_FLAG.INRANGE | POINTER_FLAG.INCONTACT;
                    contacts[0].PointerInfo.PtPixelLocation = pos;

                    if (!User32.InjectTouchInput(1, contacts))
                    {
                        throw new Win32Exception();
                    }

                    Wait.For(TimeSpan.FromMilliseconds(10));
                }
            }

            Wait.UntilInputIsProcessed();
        }

        /// <summary>
        /// Simulate touch pinch etc.
        /// </summary>
        /// <param name="from">The start position.</param>
        /// <param name="to">The end position.</param>
        /// <param name="duration">The time to drag.</param>
        public static void Multi(TwoFingers from, TwoFingers to, TimeSpan duration)
        {
            using (Down(from))
            {
                var interpolation1 = Interpolation.Start(from.First, to.First, duration);
                var interpolation2 = Interpolation.Start(from.Second, to.Second, duration);
                while (interpolation1.TryGetPosition(out var pos1) &&
                       interpolation2.TryGetPosition(out var pos2))
                {
                    contacts[0].PointerInfo.PointerFlags = POINTER_FLAG.UPDATE | POINTER_FLAG.INRANGE | POINTER_FLAG.INCONTACT;
                    contacts[0].PointerInfo.PtPixelLocation = pos1;
                    contacts[1].PointerInfo.PointerFlags = POINTER_FLAG.UPDATE | POINTER_FLAG.INRANGE | POINTER_FLAG.INCONTACT;
                    contacts[1].PointerInfo.PtPixelLocation = pos2;

                    if (!User32.InjectTouchInput(2, contacts))
                    {
                        throw new Win32Exception();
                    }

                    Wait.For(TimeSpan.FromMilliseconds(10));
                }
            }

            Wait.UntilInputIsProcessed();
        }

        /// <summary>
        /// Pinch around <paramref name="around"/>.
        /// </summary>
        /// <param name="around">The center point of the pinch.</param>
        /// <param name="startRadius">The start radius.</param>
        /// <param name="endRadius">The end radius.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="angle">The angle to the x axis.</param>
        public static void Pinch(Point around, double startRadius, double endRadius, TimeSpan duration, double angle = 45)
        {
            Multi(
                TwoFingers.Around(around, startRadius, angle),
                TwoFingers.Around(around, endRadius, angle),
                duration);
        }
    }
}
