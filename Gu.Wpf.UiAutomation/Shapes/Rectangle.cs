namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows;

    /// <summary>
    /// UI-independent implementation of a rectangle
    /// </summary>
    [Obsolete("Remove")]
    public class Rectangle : ShapeBase
    {
        public double Left { get; set; }

        public double Top { get; set; }

        public double Right { get; set; }

        public double Bottom { get; set; }

        public double X
        {
            get => this.Left;
            set => this.Left = value;
        }

        public double Y
        {
            get => this.Top;
            set => this.Top = value;
        }

        public double Width
        {
            get => this.Right - this.Left;
            set => this.Right = this.Left + value;
        }

        public double Height
        {
            get => this.Bottom - this.Top;
            set => this.Bottom = this.Top + value;
        }

        public bool IsEmpty => this.X.Equals(0) && this.Y.Equals(0) && this.Width.Equals(0) && this.Height.Equals(0);

        public bool IsValid => this.X.HasValue() && this.Y.HasValue() && this.Width.HasValue() && this.Height.HasValue();

        public Point Center => new Point((this.Width / 2) + this.Left, (this.Height / 2) + this.Top);

        public Point North => this.GetNorth();

        public Point East => this.GetEast();

        public Point South => this.GetSouth();

        public Point West => this.GetWest();

        public Point ImmediateExteriorNorth => this.GetNorth(-1);

        public Point ImmediateInteriorNorth => this.GetNorth(1);

        public Point ImmediateExteriorEast => this.GetEast(1);

        public Point ImmediateInteriorEast => this.GetEast(-1);

        public Point ImmediateExteriorSouth => this.GetSouth(1);

        public Point ImmediateInteriorSouth => this.GetSouth(-1);

        public Point ImmediateExteriorWest => this.GetWest(-1);

        public Point ImmediateInteriorWest => this.GetWest(1);

        public Rectangle(double x, double y, double width, double height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Implicit conversion to GDI rectangle
        /// </summary>
        public static implicit operator System.Drawing.Rectangle(Rectangle r)
        {
            return new System.Drawing.Rectangle(r.X.ToInt(), r.Y.ToInt(), r.Width.ToInt(), r.Height.ToInt());
        }

        /// <summary>
        /// Implicit conversion from GDI rectangle
        /// </summary>
        public static implicit operator Rectangle(System.Drawing.Rectangle r)
        {
            return new Rectangle(r.X, r.Y, r.Width, r.Height);
        }

        /// <summary>
        /// Implicit conversion to WPF rectangle
        /// </summary>
        public static implicit operator Rect(Rectangle r)
        {
            return new Rect(r.X, r.Y, r.Width, r.Height);
        }

        /// <summary>
        /// Implicit conversion from WPF rectangle
        /// </summary>
        public static implicit operator Rectangle(Rect r)
        {
            return new Rectangle(r.X, r.Y, r.Width, r.Height);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"X={this.X},Y={this.Y},Width={this.Width},Height={this.Height}";
        }

        private Point GetNorth(int by = 0)
        {
            return new Point(this.Center.X, this.Top + by);
        }

        private Point GetEast(int by = 0)
        {
            return new Point(this.Right + by, this.Center.Y);
        }

        private Point GetSouth(int by = 0)
        {
            return new Point(this.Center.X, this.Bottom + by);
        }

        private Point GetWest(int by = 0)
        {
            return new Point(this.Left + by, this.Center.Y);
        }

        public static Rectangle Empty => new Rectangle(0, 0, 0, 0);
    }
}
