namespace Gu.Wpf.UiAutomation.Shapes
{
    using System;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <summary>
    /// UI-independent implementation of a point
    /// </summary>
    public class Point : ShapeBase
    {
        /// <summary>
        /// Exact x-coordinate
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Exact y-coordinate
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets a value indicating whether this point is empty (all coordinates are 0)
        /// </summary>
        public bool IsEmpty => this.Equals(EmptyPoint);

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Calculates the distance to the other given point
        /// </summary>
        public double Distance(Point otherPoint)
        {
            return this.Distance(otherPoint.X, otherPoint.Y);
        }

        public double Distance(double otherX, double otherY)
        {
            return Math.Sqrt(Math.Pow(this.X - otherX, 2) + Math.Pow(this.Y - otherY, 2));
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable NonReadonlyMemberInGetHashCode
                return this.X.GetHashCode() * 397 ^ this.Y.GetHashCode();

                // ReSharper restore NonReadonlyMemberInGetHashCode
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Point)other);
        }

        public bool Equals(Point other)
        {
            return this.X.Equals(other.X) && this.Y.Equals(other.Y);
        }

        /// <summary>
        /// Implicit conversion to GDI point
        /// </summary>
        public static implicit operator System.Drawing.Point(Point p)
        {
            return new System.Drawing.Point(p.X.ToInt(), p.Y.ToInt());
        }

        /// <summary>
        /// Implicit conversion from GDI point
        /// </summary>
        public static implicit operator Point(System.Drawing.Point p)
        {
            return new Point(p.X, p.Y);
        }

        /// <summary>
        /// Implicit conversion to WPF point
        /// </summary>
        public static implicit operator System.Windows.Point(Point p)
        {
            return new System.Windows.Point(p.X, p.Y);
        }

        /// <summary>
        /// Implicit conversion from WPF point
        /// </summary>
        public static implicit operator Point(System.Windows.Point p)
        {
            return new Point(p.X, p.Y);
        }

        /// <summary>
        /// Implicit conversion to native point
        /// </summary>
        public static implicit operator POINT(Point p)
        {
            return new POINT { X = p.X.ToInt(), Y = p.Y.ToInt() };
        }

        /// <summary>
        /// Implicit conversion from native point
        /// </summary>
        public static implicit operator Point(POINT p)
        {
            return new Point(p.X, p.Y);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"X={this.X},Y={this.Y}";
        }

        /// <summary>
        /// Instance of an empty point
        /// </summary>
        public static readonly Point EmptyPoint = new Point(0, 0);
    }
}
