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

        private enum PointerInputType
        {
            PT_POINTER = 0x00000001,
            PT_TOUCH = 0x00000002,
            PT_PEN = 0x00000003,
            PT_MOUSE = 0x00000004,
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

        private struct POINTER_INFO
        {
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

        private struct PointerTouchInfo
        {
            public POINTER_INFO PointerInfo;
            public TouchFlags TouchFlags;
            public TouchMask TouchMasks;
            public ContactArea ContactArea;
            public ContactArea ContactAreaRaw;
            public uint Orientation;
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
                    Orientation = 90,
                    Pressure = 32000,
                    TouchMasks = TouchMask.CONTACTAREA | TouchMask.ORIENTATION | TouchMask.PRESSURE,
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
