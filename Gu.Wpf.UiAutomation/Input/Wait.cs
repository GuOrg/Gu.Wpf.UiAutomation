namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Threading;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <summary>
    /// Class with various helper tools used in various places.
    /// </summary>
    public static class Wait
    {
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(1);

        public static void For(TimeSpan time)
        {
            var stopTime = DateTime.Now + time;
            while (DateTime.Now < stopTime)
            {
                if (!Thread.Yield())
                {
                    Thread.Sleep(10);
                }
            }
        }

        /// <summary>
        /// Waits 100 ms for a generic time which was found to be sufficient to allow
        /// input (mouse, keyboard, ...) do be processed.
        /// </summary>
        public static void UntilInputIsProcessed()
        {
            For(TimeSpan.FromMilliseconds(WindowsVersion.IsAppVeyor() ? 200 : 50));
        }

        /// <summary>
        /// Waits for a generic time which was found to be sufficient to allow
        /// input (mouse, keyboard, ...) do be processed.
        /// </summary>
        public static void UntilInputIsProcessed(TimeSpan delay)
        {
            For(delay);
        }

        public static bool UntilResponsive(UiElement uiElement)
        {
            return UntilResponsive(uiElement, DefaultTimeout);
        }

        public static bool UntilResponsive(UiElement uiElement, TimeSpan timeout)
        {
            if (uiElement == null)
            {
                throw new ArgumentNullException(nameof(uiElement));
            }

            if (uiElement.TryGetWindow(out var window))
            {
                return UntilResponsive(window.NativeWindowHandle, timeout);
            }

            return false;
        }

        public static bool UntilResponsive(IntPtr hWnd)
        {
            return UntilResponsive(hWnd, DefaultTimeout);
        }

        /// <summary>
        /// Waits until a window is responsive by sending a WM_NULL message.
        /// See: https://blogs.msdn.microsoft.com/oldnewthing/20161118-00/?p=94745.
        /// </summary>
        public static bool UntilResponsive(IntPtr hWnd, TimeSpan timeout)
        {
            var ret = User32.SendMessageTimeout(
                hWnd,
                WindowsMessages.WM_NULL,
                UIntPtr.Zero,
                IntPtr.Zero,
                SendMessageTimeoutFlags.SMTO_NORMAL,
                (uint)timeout.TotalMilliseconds,
                out UIntPtr _);

            // There might be other things going on so do a small sleep anyway...
            // Other sources: http://blogs.msdn.com/b/oldnewthing/archive/2014/02/13/10499047.aspx
            For(TimeSpan.FromMilliseconds(20));
            return ret != new IntPtr(0);
        }
    }
}
