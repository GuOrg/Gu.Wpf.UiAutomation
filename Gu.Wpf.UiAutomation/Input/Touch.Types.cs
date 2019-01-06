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
        [Flags]
        private enum PointerFlag
        {
            NONE = 0x00000000,
            NEW = 0x00000001,
            INRANGE = 0x00000002,
            INCONTACT = 0x00000004,
            FIRSTBUTTON = 0x00000010,
            SECONDBUTTON = 0x00000020,
            THIRDBUTTON = 0x00000040,
            OTHERBUTTON = 0x00000080,
            PRIMARY = 0x00000100,
            CONFIDENCE = 0x00000200,
            CANCELLED = 0x00000400,
            DOWN = 0x00010000,
            UPDATE = 0x00020000,
            UP = 0x00040000,
            WHEEL = 0x00080000,
            HWHEEL = 0x00100000,
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

        private enum PointerButtonChangeType
        {
            NONE,
            FIRSTBUTTON_DOWN,
            FIRSTBUTTON_UP,
            SECONDBUTTON_DOWN,
            SECONDBUTTON_UP,
            THIRDBUTTON_DOWN,
            THIRDBUTTON_UP,
            FOURTHBUTTON_DOWN,
            FOURTHBUTTON_UP,
            FIFTHBUTTON_DOWN,
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
            public uint PointerId;
            public uint FrameId;
            public PointerFlag PointerFlags;
            public IntPtr SourceDevice;
            public IntPtr HwndTarget;
            public TouchPoint PtPixelLocation;
            public TouchPoint PtPixelLocationRaw;
            public TouchPoint PtHimetricLocation;
            public TouchPoint PtHimetricLocationRaw;
            public uint DwTime;
            public uint HistoryCount;
            public uint InputData;
            public uint DwKeyStates;
            public ulong PerformanceCount;
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
