namespace Gu.Wpf.UiAutomation.UIA3.Extensions
{
    using System.Windows;

    public static class PointExtensions
    {
        public static Interop.UIAutomationClient.tagPOINT ToTagPoint(this Point p)
        {
            return new Interop.UIAutomationClient.tagPOINT { x = p.X.ToInt(), y = p.Y.ToInt() };
        }

        public static Point ToPoint(this Interop.UIAutomationClient.tagPOINT p)
        {
            return new Point(p.x, p.y);
        }
    }
}
