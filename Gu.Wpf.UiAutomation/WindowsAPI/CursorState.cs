// ReSharper disable InconsistentNaming
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    /// <summary>
    /// Returned from GetCursorInfo
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagcursorinfo
    /// </summary>
    public enum CursorState
    {
        /// <summary>
        /// No mouse installed.
        /// </summary>
        NOMOUSE = -1,

        /// <summary>
        /// The cursor is hidden.
        /// </summary>
        HIDDEN = 0,

        /// <summary>
        /// The cursor is showing.
        /// </summary>
        CURSOR_SHOWING = 0x00000001,

        /// <summary>
        /// Windows 8: The cursor is suppressed. This flag indicates that the system is not drawing the cursor because the user is providing input through touch or pen instead of the mouse.
        /// </summary>
        CURSOR_SUPPRESSED = 0x00000002,
    }
}
