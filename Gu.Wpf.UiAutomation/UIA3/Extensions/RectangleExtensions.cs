namespace Gu.Wpf.UiAutomation.UIA3.Extensions
{
    using System.Windows;

    public static class RectangleExtensions
    {
        public static Interop.UIAutomationClient.tagRECT ToTagRect(this Rect r)
        {
            return new Interop.UIAutomationClient.tagRECT
            {
                left = r.Left.ToInt(),
                top = r.Top.ToInt(),
                right = r.Right.ToInt(),
                bottom = r.Bottom.ToInt()
            };
        }

        public static Rect ToRectangle(this Interop.UIAutomationClient.tagRECT r)
        {
            return new Rect(r.left, r.top, r.right - r.left, r.bottom - r.top);
        }
    }
}
