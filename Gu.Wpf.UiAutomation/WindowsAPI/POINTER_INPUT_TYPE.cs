// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    /// <summary>
    /// Identifies the pointer input types.
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ne-winuser-tagpointer_input_type.
    /// </summary>
    public enum POINTER_INPUT_TYPE
    {
        /// <summary>
        /// Generic pointer type. This type never appears in pointer messages or pointer data. Some data query functions allow the caller to restrict the query to specific pointer type.
        /// The PT_POINTER type can be used in these functions to specify that the query is to include pointers of all types
        /// </summary>
        PT_POINTER = 0x00000001,

        /// <summary>
        /// Touch pointer type.
        /// </summary>
        PT_TOUCH = 0x00000002,

        /// <summary>
        /// Pen pointer type.
        /// </summary>
        PT_PEN = 0x00000003,

        /// <summary>
        /// Mouse pointer type.
        /// </summary>
        PT_MOUSE = 0x00000004,

        /// <summary>
        /// Touchpad pointer type (Windows 8.1 and later).
        /// </summary>
        PT_TOUCHPAD = 0x00000005,
    }
}
