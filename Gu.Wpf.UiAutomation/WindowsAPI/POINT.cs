// ReSharper disable InconsistentNaming
#pragma warning disable
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    [DebuggerDisplay("({X}, {Y})")]
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
}
#pragma warning restore
