namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    public struct RECT : IEquatable<RECT>
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public static bool operator ==(RECT left, RECT right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RECT left, RECT right)
        {
            return !left.Equals(right);
        }

        public static RECT Create(POINT p, int radius) => new RECT
        {
            Left = p.X - radius,
            Right = p.X + radius,
            Top = p.Y - radius,
            Bottom = p.Y + radius,
        };

        /// <inheritdoc />
        public bool Equals(RECT other)
        {
            return this.Left == other.Left &&
                   this.Top == other.Top &&
                   this.Right == other.Right &&
                   this.Bottom == other.Bottom;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is RECT other && this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Left;
                hashCode = (hashCode * 397) ^ this.Top;
                hashCode = (hashCode * 397) ^ this.Right;
                hashCode = (hashCode * 397) ^ this.Bottom;
                return hashCode;
            }
        }
    }
}
