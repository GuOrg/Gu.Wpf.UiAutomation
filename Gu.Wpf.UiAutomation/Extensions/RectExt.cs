namespace Gu.Wpf.UiAutomation
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    public static class RectExt
    {
        public static bool IsValid(this Rect self)
        {
            return self.X.HasValue() && self.Y.HasValue() && self.Width.HasValue() && self.Height.HasValue();
        }

        public static Point Center(this Rect self) => new(self.Left + (self.Width / 2), self.Top + (self.Height / 2));

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator", Justification = "We want it here.")]
        public static bool IsZeroes(this Rect self)
        {
            return self.X == 0 &&
                   self.Y == 0 &&
                   self.Width == 0 &&
                   self.Height == 0;
        }
    }
}
