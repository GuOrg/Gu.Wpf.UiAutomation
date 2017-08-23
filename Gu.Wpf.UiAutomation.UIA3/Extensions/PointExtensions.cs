namespace Gu.Wpf.UiAutomation.UIA3.Extensions
{
    using UIA = Interop.UIAutomationClient;

    public static class PointExtensions
    {
        public static UIA.tagPOINT ToTagPoint(this Point p)
        {
            return new UIA.tagPOINT { x = p.X.ToInt(), y = p.Y.ToInt() };
        }

        public static Point ToPoint(this UIA.tagPOINT p)
        {
            return new Point(p.x, p.y);
        }
    }
}
