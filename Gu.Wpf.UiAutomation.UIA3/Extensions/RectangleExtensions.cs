namespace Gu.Wpf.UiAutomation.UIA3.Extensions
{
    using System.Windows;
    using UIA = Interop.UIAutomationClient;

    public static class RectangleExtensions
    {
        public static UIA.tagRECT ToTagRect(this Rect r)
        {
            return new UIA.tagRECT
            {
                left = r.Left.ToInt(),
                top = r.Top.ToInt(),
                right = r.Right.ToInt(),
                bottom = r.Bottom.ToInt()
            };
        }

        public static Rect ToRectangle(this UIA.tagRECT r)
        {
            return new Rect(r.left, r.top, r.right - r.left, r.bottom - r.top);
        }
    }
}
