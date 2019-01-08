// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable CA2225 // Operator overloads have named alternates
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [Obsolete("This is not used anywhere, dunno if there is a reason to keep it.")]
    [StructLayout(LayoutKind.Sequential)]
    public struct COLORREF : IEquatable<COLORREF>
    {
        public byte R;
        public byte G;
        public byte B;

        public COLORREF(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public static implicit operator Color(COLORREF c)
        {
            return Color.FromArgb(c.R, c.G, c.B);
        }

        public static implicit operator COLORREF(Color c)
        {
            return new COLORREF(c.R, c.G, c.B);
        }

        public static implicit operator System.Windows.Media.Color(COLORREF c)
        {
            return System.Windows.Media.Color.FromRgb(c.R, c.G, c.B);
        }

        public static implicit operator COLORREF(System.Windows.Media.Color c)
        {
            return new COLORREF(c.R, c.G, c.B);
        }

        public static bool operator ==(COLORREF left, COLORREF right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(COLORREF left, COLORREF right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"R={this.R},G={this.G},B={this.B}";
        }

        /// <inheritdoc/>
        public bool Equals(COLORREF other) => this.R == other.R &&
                                              this.G == other.G &&
                                              this.B == other.B;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is COLORREF other && this.Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.R.GetHashCode();
                hashCode = (hashCode * 397) ^ this.G.GetHashCode();
                hashCode = (hashCode * 397) ^ this.B.GetHashCode();
                return hashCode;
            }
        }
    }
}
