namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CURSORINFO
    {
        /// <summary>
        /// Specifies the size, in bytes, of the structure.
        /// The caller must set this to Marshal.SizeOf(typeof(CURSORINFO)).
        /// </summary>
        public Int32 Size;

        /// <summary>
        /// Specifies the cursor state. This parameter can be one of the following values:
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

        public static CURSORINFO Create() =>
            new CURSORINFO
            {
                Size = Marshal.SizeOf(typeof(CURSORINFO)),
            };
    }
}
