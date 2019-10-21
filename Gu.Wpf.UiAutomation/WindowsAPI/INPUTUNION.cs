// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1310 // Field names must not contain underscore
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct INPUTUNION : IEquatable<INPUTUNION>
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;

        [FieldOffset(0)]
        public KEYBDINPUT ki;

        [FieldOffset(0)]
        public HARDWAREINPUT hi;

        public static bool operator ==(INPUTUNION left, INPUTUNION right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(INPUTUNION left, INPUTUNION right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public bool Equals(INPUTUNION other) => this.mi.Equals(other.mi) &&
                                                this.ki.Equals(other.ki) &&
                                                this.hi.Equals(other.hi);

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is INPUTUNION other &&
                                                   this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.mi.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ki.GetHashCode();
                hashCode = (hashCode * 397) ^ this.hi.GetHashCode();
                return hashCode;
            }
        }
    }
}
