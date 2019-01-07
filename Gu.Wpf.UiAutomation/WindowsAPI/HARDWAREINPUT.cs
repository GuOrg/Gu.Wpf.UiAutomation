// ReSharper disable InconsistentNaming
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT : IEquatable<HARDWAREINPUT>
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;

        public static bool operator ==(HARDWAREINPUT left, HARDWAREINPUT right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HARDWAREINPUT left, HARDWAREINPUT right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public bool Equals(HARDWAREINPUT other)
        {
            return this.uMsg == other.uMsg &&
                   this.wParamL == other.wParamL &&
                   this.wParamH == other.wParamH;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is HARDWAREINPUT other &&
                                                   this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)this.uMsg;
                hashCode = (hashCode * 397) ^ this.wParamL.GetHashCode();
                hashCode = (hashCode * 397) ^ this.wParamH.GetHashCode();
                return hashCode;
            }
        }
    }
}
