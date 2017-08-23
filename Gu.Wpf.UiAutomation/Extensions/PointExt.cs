namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows;

    public static class PointExt
    {
        public static double DistanceTo(this Point self, Point p)
        {
            return Math.Sqrt(Math.Pow(self.X - p.X, 2) + Math.Pow(self.Y - p.Y, 2));
        }
    }
}
