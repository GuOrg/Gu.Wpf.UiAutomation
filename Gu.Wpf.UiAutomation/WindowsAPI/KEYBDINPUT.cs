// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1310 // Field names must not contain underscore
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT : IEquatable<KEYBDINPUT>
    {
        public ushort wVk;
        public ushort wScan;
        public KeyEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;

        public static bool operator ==(KEYBDINPUT left, KEYBDINPUT right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(KEYBDINPUT left, KEYBDINPUT right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public bool Equals(KEYBDINPUT other) => this.wVk == other.wVk &&
                                                this.wScan == other.wScan &&
                                                this.dwFlags == other.dwFlags &&
                                                this.time == other.time &&
                                                this.dwExtraInfo.Equals(other.dwExtraInfo);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is KEYBDINPUT other && this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.wVk.GetHashCode();
                hashCode = (hashCode * 397) ^ this.wScan.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)this.dwFlags;
                hashCode = (hashCode * 397) ^ (int)this.time;
                hashCode = (hashCode * 397) ^ this.dwExtraInfo.GetHashCode();
                return hashCode;
            }
        }
    }
}
