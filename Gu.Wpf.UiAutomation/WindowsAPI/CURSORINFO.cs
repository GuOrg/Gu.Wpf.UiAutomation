// ReSharper disable InconsistentNaming
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CURSORINFO : IEquatable<CURSORINFO>
    {
        /// <summary>
        /// Specifies the size, in bytes, of the structure.
        /// The caller must set this to Marshal.SizeOf(typeof(CURSORINFO)).
        /// </summary>
        public int Size;

        /// <summary>
        /// Specifies the cursor state.
        /// </summary>
        public CursorState Flags;

        /// <summary>
        /// A handle to the cursor.
        /// </summary>
        public IntPtr Cursor;

        /// <summary>
        /// A structure that receives the screen coordinates of the cursor.
        /// </summary>
        public POINT ScreenPos;

        public static bool operator ==(CURSORINFO left, CURSORINFO right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CURSORINFO left, CURSORINFO right)
        {
            return !left.Equals(right);
        }

        public static CURSORINFO Create() =>
            new CURSORINFO
            {
                Size = Marshal.SizeOf(typeof(CURSORINFO)),
            };

        /// <inheritdoc />
        public bool Equals(CURSORINFO other)
        {
            return this.Size == other.Size &&
                   this.Flags == other.Flags &&
                   this.Cursor.Equals(other.Cursor) &&
                   this.ScreenPos.Equals(other.ScreenPos);
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is CURSORINFO other &&
                                                   this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Size;
                hashCode = (hashCode * 397) ^ (int)this.Flags;
                hashCode = (hashCode * 397) ^ this.Cursor.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ScreenPos.GetHashCode();
                return hashCode;
            }
        }
    }
}
