namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

    public static class Msvcrt
    {
        [DllImport("msvcrt.dll")]
#pragma warning disable SA1300, IDE1006 // Element should begin with upper-case letter
        public static extern int memcmp(IntPtr b1, IntPtr b2, long count);
#pragma warning restore SA1300, IDE1006 // Element should begin with upper-case letter
    }
}
