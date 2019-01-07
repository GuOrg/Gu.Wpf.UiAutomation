namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// https://stackoverflow.com/a/16050600/1069200.
    /// </summary>
    public class SafeCursorHandle : SafeHandleZeroOrMinusOneIsInvalid
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
