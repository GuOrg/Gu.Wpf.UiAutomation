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

        /// <summary>
        /// Create an instance of <see cref="TwoFingers"/>.
        /// </summary>
        /// <param name="around">The center point.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="angle">The angle to the x axis.</param>
        /// <returns>An instance of <see cref="TwoFingers"/>.</returns>
        public static TwoFingers Around(Point around, double radius, double angle)
        {
            var v = new Vector(radius * Math.Cos(angle * Math.PI / 180), radius * Math.Sin(angle * Math.PI / 180));
            return new TwoFingers(around + v, around - v);
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
