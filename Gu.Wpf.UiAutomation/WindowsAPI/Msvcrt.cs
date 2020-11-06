namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

#pragma warning disable CA1060 // Move pinvokes to native methods class
    public static class Msvcrt
#pragma warning restore CA1060 // Move pinvokes to native methods class
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
#pragma warning disable SA1300, IDE1006 // Element should begin with upper-case letter
        public static extern int memcmp(IntPtr b1, IntPtr b2, UIntPtr count);
#pragma warning restore SA1300, IDE1006 // Element should begin with upper-case letter
    }
}
