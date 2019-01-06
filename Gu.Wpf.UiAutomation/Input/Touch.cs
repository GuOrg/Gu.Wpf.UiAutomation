namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
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
            if (!InitializeTouchInjection())
            {
                throw new Win32Exception();
            }
        }

        private static PointerTouchInfo[] contacts = new PointerTouchInfo[1];

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
            contacts = new[] { PointerTouchInfo.Create(point, 0) };

            if (!InjectTouchInput(1, contacts))
            {
                throw new Win32Exception();
            }

            return new ActionDisposable(() =>
            {
                contacts[0].PointerInfo.PointerFlags = PointerFlag.UP;
                if (!InjectTouchInput(1, contacts))
                {
                    throw new Win32Exception();
                }

                //contacts[0].PointerInfo.PointerFlags = PointerFlag.CAPTURECHANGED;
                //contacts[0].TouchMasks = TouchMask.NONE;
                //contacts[0].PointerInfo.PointerType = PointerInputType.PT_MOUSE;
                //if (!InjectTouchInput(1, contacts))
                //{
                //    throw new Win32Exception();
                //}

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
                contacts[i].PointerInfo.PointerFlags = PointerFlag.UP;
            }

            if (!InjectTouchInput(contacts.Length, contacts))
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
                contacts[0].PointerInfo.PointerFlags = PointerFlag.UPDATE | PointerFlag.INRANGE | PointerFlag.INCONTACT;
                contacts[0].PointerInfo.PtPixelLocation = TouchPoint.Create(to);

                if (!InjectTouchInput(1, contacts))
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

            contacts[0].PointerInfo.PointerFlags = PointerFlag.UPDATE | PointerFlag.INRANGE | PointerFlag.INCONTACT;
            contacts[0].PointerInfo.PtPixelLocation = TouchPoint.Create(position);

            if (!InjectTouchInput(1, contacts))
            {
                throw new Win32Exception();
            }
        }

        [DllImport("User32.dll", SetLastError = true)]
        private static extern bool InitializeTouchInjection(uint maxCount = 256, TouchFeedback feedbackMode = TouchFeedback.NONE);

        [DllImport("User32.dll", SetLastError = true)]
        private static extern bool InjectTouchInput(int count, [MarshalAs(UnmanagedType.LPArray), In] PointerTouchInfo[] contacts);

        private static CursorState GetCursorState()
        {
            var cursorInfo = CURSORINFO.Create();
            if (!User32.GetCursorInfo(ref cursorInfo))
            {
                throw new Win32Exception();
            }

            return cursorInfo.Flags;
        }
    }
}
