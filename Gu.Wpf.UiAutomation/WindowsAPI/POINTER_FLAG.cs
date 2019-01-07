// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    /// <summary>
    /// Values that can appear in the pointerFlags field of the POINTER_INFO structure.
    /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/inputmsg/pointer-flags-contants.
    /// </summary>
    [Flags]
    public enum POINTER_FLAG
    {
        /// <summary>
        /// Default
        /// </summary>
        NONE = 0x00000000,

        /// <summary>
        /// Indicates the arrival of a new pointer.
        /// </summary>
        NEW = 0x00000001,

        /// <summary>
        /// Indicates that this pointer continues to exist. When this flag is not set, it indicates the pointer has left detection range.
        /// This flag is typically not set only when a hovering pointer leaves detection range (POINTER_FLAG_UPDATE is set) or when a pointer in contact with a window surface leaves detection range(POINTER_FLAG_UP is set).
        /// </summary>
        INRANGE = 0x00000002,

        /// <summary>
        /// Indicates that this pointer is in contact with the digitizer surface. When this flag is not set, it indicates a hovering pointer.
        /// </summary>
        INCONTACT = 0x00000004,

        /// <summary>
        /// Indicates a primary action, analogous to a left mouse button down.
        /// A touch pointer has this flag set when it is in contact with the digitizer surface.
        /// A pen pointer has this flag set when it is in contact with the digitizer surface with no buttons pressed.
        /// A mouse pointer has this flag set when the left mouse button is down.
        /// </summary>
        FIRSTBUTTON = 0x00000010,

        /// <summary>
        /// Indicates a secondary action, analogous to a right mouse button down.
        /// A touch pointer does not use this flag.
        /// A pen pointer has this flag set when it is in contact with the digitizer surface with the pen barrel button pressed.
        /// A mouse pointer has this flag set when the right mouse button is down.
        /// </summary>
        SECONDBUTTON = 0x00000020,

        /// <summary>
        /// Analogous to a mouse wheel button down.
        /// A touch pointer does not use this flag.
        /// A pen pointer does not use this flag.
        /// A mouse pointer has this flag set when the mouse wheel button is down.
        /// </summary>
        THIRDBUTTON = 0x00000040,

        /// <summary>
        /// Analogous to a first extended mouse (XButton1) button down.
        /// A touch pointer does not use this flag.
        /// A pen pointer does not use this flag.
        /// A mouse pointer has this flag set when the first extended mouse(XBUTTON1) button is down.
        /// </summary>
        FOURTHBUTTON = 0x00000080,

        /// <summary>
        /// Analogous to a first extended mouse (XButton2) button down.
        /// A touch pointer does not use this flag.
        /// A pen pointer does not use this flag.
        /// A mouse pointer has this flag set when the first extended mouse(XBUTTON2) button is down.
        /// </summary>
        FIFTHBUTTON = 0x00000100,

        /// <summary>
        /// Indicates that this pointer has been designated as the primary pointer. A primary pointer is a single pointer that can perform actions beyond those available to non-primary pointers.
        /// For example, when a primary pointer makes contact with a window s surface, it may provide the window an opportunity to activate by sending it a WM_POINTERACTIVATE message.
        /// The primary pointer is identified from all current user interactions on the system (mouse, touch, pen, and so on).
        /// As such, the primary pointer might not be associated with your app.The first contact in a multi-touch interaction is set as the primary pointer.Once a primary pointer is identified, all contacts must be lifted before a new contact can be identified as a primary pointer.For apps that don't process pointer input, only the primary pointer's events are promoted to mouse events.
        /// </summary>
        PRIMARY = 0x00002000,

        /// <summary>
        /// Confidence is a suggestion from the source device about whether the pointer represents an intended or accidental interaction, which is especially relevant for PT_TOUCH pointers where an accidental interaction (such as with the palm of the hand) can trigger input. The presence of this flag indicates that the source device has high confidence that this input is part of an intended interaction.
        /// </summary>
        CONFIDENCE = 0x000004000,

        /// <summary>
        /// Indicates that the pointer is departing in an abnormal manner, such as when the system receives invalid input for the pointer or when a device with active pointers departs abruptly.
        /// If the application receiving the input is in a position to do so, it should treat the interaction as not completed and reverse any effects of the concerned pointer.
        /// </summary>
        CANCELLED = 0x000008000,

        /// <summary>
        /// Indicates that this pointer transitioned to a down state; that is, it made contact with the digitizer surface.
        /// </summary>
        DOWN = 0x00010000,

        /// <summary>
        /// Indicates that this is a simple update that does not include pointer state changes.
        /// </summary>
        UPDATE = 0x00020000,

        /// <summary>
        /// Indicates that this pointer transitioned to an up state; that is, contact with the digitizer surface ended.
        /// </summary>
        UP = 0x00040000,

        /// <summary>
        /// Indicates input associated with a pointer wheel. For mouse pointers, this is equivalent to the action of the mouse scroll wheel (WM_MOUSEHWHEEL).
        /// </summary>
        WHEEL = 0x00080000,

        /// <summary>
        /// Indicates input associated with a pointer h-wheel. For mouse pointers, this is equivalent to the action of the mouse horizontal scroll wheel (WM_MOUSEHWHEEL).
        /// </summary>
        HWHEEL = 0x00100000,

        /// <summary>
        /// Indicates that this pointer was captured by (associated with) another element and the original element has lost capture (see WM_POINTERCAPTURECHANGED).
        /// </summary>
        CAPTURECHANGED = 0x00200000,

        /// <summary>
        /// Indicates that this pointer has an associated transform.
        /// </summary>
        HASTRANSFORM = 0x00400000,
    }
}
