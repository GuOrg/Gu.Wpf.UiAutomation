﻿using Gu.Wpf.UiAutomation.Shapes;
using Gu.Wpf.UiAutomation.Tools;
using UIA = Interop.UIAutomationClient;

namespace Gu.Wpf.UiAutomation.UIA3.Extensions
{
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