namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public static RECT Create(POINT p, int radius) => new RECT
        {
            Left = p.X - radius,
            Right = p.X + radius,
            Top = p.Y - radius,
            Bottom = p.Y + radius,
        };
    }
}
