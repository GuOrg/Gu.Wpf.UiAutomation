// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable NotAccessedField.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
#pragma warning disable 649
namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows;

    public static partial class Touch
    {
        /// <summary>
        /// Values that can appear in the pointerFlags field of the POINTER_INFO structure.
        /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/inputmsg/pointer-flags-contants.
        /// </summary>
        [Flags]
        private enum PointerFlag
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

        private enum TouchFeedback
        {
            DEFAULT = 0x1,
            INDIRECT = 0x2,
            NONE = 0x3,
        }

        private enum TouchFlags
        {
            NONE = 0x00000000,
        }

        [Flags]
        private enum TouchMask
        {
            NONE = 0x00000000,
            CONTACTAREA = 0x00000001,
            ORIENTATION = 0x00000002,
            PRESSURE = 0x00000004,
        }

        /// <summary>
        /// Identifies the pointer input types.
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ne-winuser-tagpointer_input_type.
        /// </summary>
        private enum PointerInputType
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

        /// <summary>
        /// Identifies a change in the state of a button associated with a pointer.
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ne-winuser-tagpointer_button_change_type.
        /// </summary>
        private enum PointerButtonChangeType
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

        private struct ContactArea
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public static ContactArea Create(TouchPoint p, int r) => new ContactArea
            {
                Left = p.X - r,
                Right = p.X + r,
                Top = p.Y - r,
                Bottom = p.Y + r,
            };
        }

        private struct TouchPoint
        {
            public int X;
            public int Y;

            public static TouchPoint Create(Point p)
            {
                return new TouchPoint
                {
                    X = (int)p.X,
                    Y = (int)p.Y,
                };
            }
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagpointer_info.
        /// </summary>
        private struct POINTER_INFO
        {
            /// <summary>
            /// A value from the <see cref="PointerInputType"/> enumeration that specifies the pointer type.
            /// </summary>
            public PointerInputType PointerType;

            /// <summary>
            /// An identifier that uniquely identifies a pointer during its lifetime.
            /// A pointer comes into existence when it is first detected and ends its existence when it goes out of detection range.
            /// Note that if a physical entity (finger or pen) goes out of detection range and then returns to be detected again, it is treated as a new pointer and may be assigned a new pointer identifier.
            /// </summary>
            public uint PointerId;

            /// <summary>
            /// An identifier common to multiple pointers for which the source device reported an update in a single input frame.
            /// For example, a parallel-mode multi-touch digitizer may report the positions of multiple touch contacts in a single update to the system.
            /// <remarks>
            /// Note that frame identifier is assigned as input is reported to the system for all pointers across all devices.
            /// Therefore, this field may not contain strictly sequential values in a single series of messages that a window receives.
            /// However, this field will contain the same numerical value for all input updates that were reported in the same input frame by a single device.
            /// </remarks>
            /// </summary>
            public uint FrameId;

            /// <summary>
            /// May be any reasonable combination of flags from the <see cref="PointerFlag"/> constants.
            /// </summary>
            public PointerFlag PointerFlags;

            /// <summary>
            /// Handle to the source device that can be used in calls to the raw input device API and the digitizer device API.
            /// </summary>
            public IntPtr SourceDevice;

            /// <summary>
            /// Window to which this message was targeted.
            /// If the pointer is captured, either implicitly by virtue of having made contact over this window or explicitly using the pointer capture API, this is the capture window.
            /// If the pointer is uncaptured, this is the window over which the pointer was when this message was generated.
            /// </summary>
            public IntPtr HwndTarget;

            /// <summary>
            /// The predicted screen coordinates of the pointer, in pixels.
            /// The predicted value is based on the pointer position reported by the digitizer and the motion of the pointer.
            /// This correction can compensate for visual lag due to inherent delays in sensing and processing the pointer location on the digitizer.This is applicable to pointers of type PT_TOUCH.
            /// For other pointer types, the predicted value will be the same as the non-predicted value (see ptPixelLocationRaw).
            /// </summary>
            public TouchPoint PtPixelLocation;

            /// <summary>
            /// The predicted screen coordinates of the pointer, in HIMETRIC units.
            /// The predicted value is based on the pointer position reported by the digitizer and the motion of the pointer.
            /// This correction can compensate for visual lag due to inherent delays in sensing and processing the pointer location on the digitizer.
            /// This is applicable to pointers of type PT_TOUCH.For other pointer types, the predicted value will be the same as the non-predicted value (see ptHimetricLocationRaw).
            /// </summary>
            public TouchPoint PtPixelLocationRaw;

            /// <summary>
            /// The screen coordinates of the pointer, in pixels. For adjusted screen coordinates, see ptPixelLocation.
            /// </summary>
            public TouchPoint PtHimetricLocation;

            /// <summary>
            /// The screen coordinates of the pointer, in HIMETRIC units. For adjusted screen coordinates, see ptHimetricLocation.
            /// </summary>
            public TouchPoint PtHimetricLocationRaw;

            /// <summary>
            /// 0 or the time stamp of the message, based on the system tick count when the message was received.
            /// The application can specify the input time stamp in either dwTime or PerformanceCount.
            /// The value cannot be more recent than the current tick count or QueryPerformanceCount (QPC) value of the injection thread. Once a frame is injected with a time stamp, all subsequent frames must include a timestamp until all contacts in the frame go to an UP state.The custom timestamp value must also be provided for the first element in the contacts array.The time stamp values after the first element are ignored. The custom timestamp value must increment in every injection frame.
            /// When PerformanceCount is specified, the time stamp will be converted to the current time in .1 millisecond resolution upon actual injection.If a custom PerformanceCount resulted in the same .1 millisecond window from the previous injection, ERROR_NOT_READY is returned and injection will not occur. While injection will not be invalidated immediately by the error, the next successful injection must have a PerformanceCount value that is at least 0.1 millisecond from the previously successful injection. This is also true if dwTime is used.
            /// If both dwTime and PerformanceCount are specified in InjectTouchInput, ERROR_INVALID_PARAMETER is returned.
            /// InjectTouchInput cannot switch between dwTime and PerformanceCount once injection has started.
            /// If neither dwTime and PerformanceCount are specified, InjectTouchInput allocates the timestamp based on the timing of the call.
            /// If InjectTouchInput calls are repeatedly less than 0.1 millisecond apart, ERROR_NOT_READY might be returned.
            /// The error will not invalidate the input immediately, but the injection application needs to retry the same frame again for injection to succeed.
            /// </summary>
            public uint DwTime;

            /// <summary>
            /// Count of inputs that were coalesced into this message.
            /// This count matches the total count of entries that can be returned by a call to GetPointerInfoHistory.
            /// If no coalescing occurred, this count is 1 for the single input represented by the message.
            /// </summary>
            public uint HistoryCount;

            public uint InputData;

            /// <summary>
            /// Indicates which keyboard modifier keys were pressed at the time the input was generated. May be zero or a combination of the following values.
            /// POINTER_MOD_SHIFT – A SHIFT key was pressed.
            /// POINTER_MOD_CTRL – A CTRL key was pressed.
            /// </summary>
            public uint DwKeyStates;

            /// <summary>
            /// The value of the high-resolution performance counter when the pointer message was received (high-precision, 64 bit alternative to dwTime).
            /// The value can be calibrated when the touch digitizer hardware supports the scan timestamp information in its input report.
            /// </summary>
            public ulong PerformanceCount;

            /// <summary>
            /// A value from the POINTER_BUTTON_CHANGE_TYPE enumeration that specifies the change in button state between this input and the previous input.
            /// </summary>
            public PointerButtonChangeType ButtonChangeType;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagpointer_touch_info.
        /// </summary>
        private struct PointerTouchInfo
        {
            /// <summary>
            /// An embedded <see cref="PointerInfo"/> header structure.
            /// </summary>
            public POINTER_INFO PointerInfo;

            /// <summary>
            /// Currently none.
            /// </summary>
            public TouchFlags TouchFlags;

            /// <summary>
            /// Indicates which of the optional fields contain valid values. The member can be zero or any combination of the values from the <see cref="TouchMasks"/> constants.
            /// </summary>
            public TouchMask TouchMasks;

            /// <summary>
            /// The predicted screen coordinates of the contact area, in pixels. By default, if the device does not report a contact area, this field defaults to a 0-by-0 rectangle centered around the pointer location.
            /// The predicted value is based on the pointer position reported by the digitizer and the motion of the pointer.
            /// This correction can compensate for visual lag due to inherent delays in sensing and processing the pointer location on the digitizer.
            /// This is applicable to pointers of type <see cref="PointerInputType.PT_TOUCH"/>.
            /// </summary>
            public ContactArea ContactArea;

            /// <summary>
            /// The raw screen coordinates of the contact area, in pixels. For adjusted screen coordinates, see rcContact.
            /// </summary>
            public ContactArea ContactAreaRaw;

            /// <summary>
            /// A pointer orientation, with a value between 0 and 359, where 0 indicates a touch pointer aligned with the x-axis and pointing from left to right; increasing values indicate degrees of rotation in the clockwise direction.
            /// This field defaults to 0 if the device does not report orientation.
            /// </summary>
            public uint Orientation;

            /// <summary>
            /// A pen pressure normalized to a range between 0 and 1024. The default is 0 if the device does not report pressure.
            /// </summary>
            public uint Pressure;

            public static PointerTouchInfo Create(Point point, uint id)
            {
                var touchPoint = TouchPoint.Create(point);
                var contact = new PointerTouchInfo
                {
                    PointerInfo =
                    {
                        PointerType = PointerInputType.PT_TOUCH,
                        PointerFlags = PointerFlag.DOWN | PointerFlag.INRANGE | PointerFlag.INCONTACT,
                        PtPixelLocation = touchPoint,
                        PointerId = id,
                    },
                    TouchFlags = TouchFlags.NONE,
                    TouchMasks = TouchMask.CONTACTAREA,
                    ContactArea = ContactArea.Create(touchPoint, 1),
                };

                return contact;
            }

            public void Move(int deltaX, int deltaY)
            {
                this.PointerInfo.PtPixelLocation.X += deltaX;
                this.PointerInfo.PtPixelLocation.Y += deltaY;
                this.ContactArea.Left += deltaX;
                this.ContactArea.Right += deltaX;
                this.ContactArea.Top += deltaY;
                this.ContactArea.Bottom += deltaY;
            }
        }
    }
}
