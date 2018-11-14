// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1310 // Field names must not contain underscore
#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable CA1815 // Override equals and operator equals on value types
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public KeyEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}
