namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

    public static class Msvcrt
    {
        [DllImport("msvcrt.dll")]
        public static extern int memcmp(IntPtr b1, IntPtr b2, long count);
    }
}
