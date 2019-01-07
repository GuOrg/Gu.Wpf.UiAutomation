// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    /// <summary>
    /// Identifies a change in the state of a button associated with a pointer.
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ne-winuser-tagpointer_button_change_type.
    /// </summary>
    public enum POINTER_BUTTON_CHANGE_TYPE
    {
        /// <summary>
        /// No change in button state.
        /// </summary>
        NONE,

        /// <summary>
        /// The first button (see POINTER_FLAG_FIRSTBUTTON) transitioned to a pressed state.
        /// </summary>
        FIRSTBUTTON_DOWN,

        /// <summary>
        /// The first button (see POINTER_FLAG_FIRSTBUTTON) transitioned to a released state.
        /// </summary>
        FIRSTBUTTON_UP,

        /// <summary>
        /// The second button (see POINTER_FLAG_SECONDBUTTON) transitioned to a pressed state.
        /// </summary>
        SECONDBUTTON_DOWN,

        /// <summary>
        /// The second button (see POINTER_FLAG_SECONDBUTTON) transitioned to a released state.
        /// </summary>
        SECONDBUTTON_UP,

        /// <summary>
        /// The third button (see POINTER_FLAG_THIRDBUTTON) transitioned to a pressed state.
        /// </summary>
        THIRDBUTTON_DOWN,

        /// <summary>
        /// The third button (see POINTER_FLAG_THIRDBUTTON) transitioned to a released state.
        /// </summary>
        THIRDBUTTON_UP,

        /// <summary>
        /// The fourth button (see POINTER_FLAG_FOURTHBUTTON) transitioned to a pressed state.
        /// </summary>
        FOURTHBUTTON_DOWN,

        /// <summary>
        /// The fourth button (see POINTER_FLAG_FOURTHBUTTON) transitioned to a released state.
        /// </summary>
        FOURTHBUTTON_UP,

        /// <summary>
        /// The fifth button (see POINTER_FLAG_FIFTHBUTTON) transitioned to a pressed state.
        /// </summary>
        FIFTHBUTTON_DOWN,

        /// <summary>
        /// The fifth button (see POINTER_FLAG_FIFTHBUTTON) transitioned to a released state.
        /// </summary>
        FIFTHBUTTON_UP,
    }
}
