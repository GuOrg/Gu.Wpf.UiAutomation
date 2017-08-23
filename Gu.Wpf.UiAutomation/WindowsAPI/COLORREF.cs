namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct COLORREF
    {
        public byte R;
        public byte G;
        public byte B;

        public COLORREF(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static implicit operator Color(COLORREF c)
        {
            return Color.FromArgb(c.R, c.G, c.B);
        }

        public static implicit operator COLORREF(Color c)
        {
            return new COLORREF(c.R, c.G, c.B);
        }

        public static implicit operator System.Windows.Media.Color(COLORREF c)
        {
            return System.Windows.Media.Color.FromRgb(c.R, c.G, c.B);
        }

        public static implicit operator COLORREF(System.Windows.Media.Color c)
        {
            return new COLORREF(c.R, c.G, c.B);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"R={R},G={G},B={B}";
        }
    }
}