namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows;

    /// <summary>
    /// The position of two fingers when used in touch.
    /// </summary>
    public struct TwoFingers : IEquatable<TwoFingers>
    {
        /// <summary>
        /// Creates an instance of the <see cref="TwoFingers"/> struct.
        /// </summary>
        /// <param name="first">The position of the first finger.</param>
        /// <param name="second">The second of the first finger.</param>
        public TwoFingers(Point first, Point second)
        {
            this.First = first;
            this.Second = second;
        }

        /// <summary>
        /// The position of the first finger.
        /// </summary>
        public Point First { get; }

        /// <summary>
        /// The second of the first finger.
        /// </summary>
        public Point Second { get; }

        public static bool operator ==(TwoFingers left, TwoFingers right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TwoFingers left, TwoFingers right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public bool Equals(TwoFingers other) => this.First.Equals(other.First) &&
                                                this.Second.Equals(other.Second);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is TwoFingers other && this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.First.GetHashCode() * 397) ^ this.Second.GetHashCode();
            }
        }
    }
}
