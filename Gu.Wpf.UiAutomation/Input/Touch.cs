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
    /// https://social.technet.microsoft.com/wiki/contents/articles/6460.simulating-touch-input-in-windows-8-using-touch-injection-api.aspx
    /// </summary>
    public static partial class Touch
    {
        static Touch()
        {
            if (!User32.InitializeTouchInjection())
            {
                throw new Win32Exception();
            }
        }

        private static POINTER_TOUCH_INFO[] contacts = new POINTER_TOUCH_INFO[1];

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
            }
        }

        /// <summary>
        /// Simulate touch down.
        /// </summary>
        /// <param name="point">The position.</param>
        /// <returns>A disposable that calls Up when disposed.</returns>
        public static IDisposable Down(Point point)
        {
            contacts = new[] { POINTER_TOUCH_INFO.Create(point, 0) };

            if (!User32.InjectTouchInput(1, contacts))
            {
                throw new Win32Exception();
            }

            return new ActionDisposable(() =>
            {
                contacts[0].PointerInfo.PointerFlags = POINTER_FLAG.UP;
                if (!User32.InjectTouchInput(1, contacts))
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
        /// Simulate touch drag in one step.
        /// </summary>
        /// <param name="from">The start position.</param>
        /// <param name="to">The end position.</param>
        public static void Drag(Point from, Point to)
        {
            using (Down(from))
            {
                contacts[0].PointerInfo.PointerFlags = POINTER_FLAG.UPDATE | POINTER_FLAG.INRANGE | POINTER_FLAG.INCONTACT;
                contacts[0].PointerInfo.PtPixelLocation = POINT.Create(to);

                if (!User32.InjectTouchInput(1, contacts))
                {
                    throw new Win32Exception();
                }
            }
        }

        /// <summary>
        /// Simulate touch drag.
        /// Call <see cref="Down"/> before calling this method.
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

            contacts[0].PointerInfo.PointerFlags = POINTER_FLAG.UPDATE | POINTER_FLAG.INRANGE | POINTER_FLAG.INCONTACT;
            contacts[0].PointerInfo.PtPixelLocation = POINT.Create(position);

            if (!User32.InjectTouchInput(1, contacts))
            {
                throw new Win32Exception();
            }
        }
    }
}
