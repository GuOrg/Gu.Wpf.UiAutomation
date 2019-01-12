// ReSharper disable InconsistentNaming
#pragma warning disable
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;

    [DebuggerDisplay("({X}, {Y})")]
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT : IEquatable<POINT>
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool operator ==(POINT left, POINT right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(POINT left, POINT right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public bool Equals(POINT other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is POINT other &&
                                                   this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X * 397) ^ this.Y;
            }
        }

        internal static POINT From(Point p)
        {
            return new POINT
            {
                X = (int)p.X,
                Y = (int)p.Y,
            };
        }
    }
}
