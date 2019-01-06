namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;

    /// <summary>
    /// For simulating touch input.
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
            Down(point).Dispose();
            Wait.For(TimeSpan.FromMilliseconds(100));
        }

        /// <summary>
        /// Simulate touch down.
        /// </summary>
        /// <param name="point">The position.</param>
        /// <returns>A disposable that calls Up when disposed.</returns>
        public static IDisposable Down(Point point)
        {
            var location = TouchPoint.Create(point);
            var info = PointerTouchInfo.Create(point, 0);
            contacts = new[] { info };

            if (!InjectTouchInput(1, contacts))
            {
                throw new Win32Exception();
            }

            Wait.UntilInputIsProcessed();
            return new ActionDisposable(() => Up());
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
            Wait.UntilInputIsProcessed();
        }

        /// <summary>
        /// Simulate touch drag.
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

            Wait.UntilInputIsProcessed();
        }

        [DllImport("User32.dll", SetLastError = true)]
        private static extern bool InitializeTouchInjection(uint maxCount = 256, TouchFeedback feedbackMode = TouchFeedback.DEFAULT);

        [DllImport("User32.dll", SetLastError = true)]
        private static extern bool InjectTouchInput(int count, [MarshalAs(UnmanagedType.LPArray), In] PointerTouchInfo[] contacts);
    }
}
