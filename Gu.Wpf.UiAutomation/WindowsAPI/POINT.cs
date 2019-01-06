// ReSharper disable InconsistentNaming
#pragma warning disable
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;

    [DebuggerDisplay("({X}, {Y})")]
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public static POINT Create(Point p)
        {
            return new POINT
            {
                X = (int)p.X,
                Y = (int)p.Y,
            };
        }
    }
}
#pragma warning restore
