namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// https://stackoverflow.com/a/16050600/1069200.
    /// </summary>
#pragma warning disable CA1060 // Move pinvokes to native methods class
    public class SafeCursorHandle : SafeHandleZeroOrMinusOneIsInvalid
#pragma warning restore CA1060 // Move pinvokes to native methods class
    {
        public SafeCursorHandle(IntPtr handle)
            : base(ownsHandle: true)
        {
            this.SetHandle(handle);
        }

        /// <inheritdoc />
        protected override bool ReleaseHandle()
        {
            if (!this.IsInvalid)
            {
                if (!DestroyCursor(this.handle))
                {
                    throw new System.ComponentModel.Win32Exception();
                }

                this.handle = IntPtr.Zero;
            }

            return true;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyCursor(IntPtr handle);
    }
}
