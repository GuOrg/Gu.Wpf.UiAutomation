#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Windows;

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagpointer_touch_info.
    /// </summary>
    public struct POINTER_TOUCH_INFO : IEquatable<POINTER_TOUCH_INFO>
    {
        /// <summary>
        /// An embedded <see cref="PointerInfo"/> header structure.
        /// </summary>
        public POINTER_INFO PointerInfo;

        /// <summary>
        /// Currently none.
        /// </summary>
        public TOUCH_FLAGS TouchFlags;

        /// <summary>
        /// Indicates which of the optional fields contain valid values. The member can be zero or any combination of the values from the <see cref="TouchMasks"/> constants.
        /// </summary>
        public TOUCH_MASK TouchMasks;

        /// <summary>
        /// The predicted screen coordinates of the contact area, in pixels. By default, if the device does not report a contact area, this field defaults to a 0-by-0 rectangle centered around the pointer location.
        /// The predicted value is based on the pointer position reported by the digitizer and the motion of the pointer.
        /// This correction can compensate for visual lag due to inherent delays in sensing and processing the pointer location on the digitizer.
        /// This is applicable to pointers of type <see cref="POINTER_INPUT_TYPE.PT_TOUCH"/>.
        /// </summary>
        public RECT Contact;

        /// <summary>
        /// The raw screen coordinates of the contact area, in pixels. For adjusted screen coordinates, see rcContact.
        /// </summary>
        public RECT ContactAreaRaw;

        /// <summary>
        /// A pointer orientation, with a value between 0 and 359, where 0 indicates a touch pointer aligned with the x-axis and pointing from left to right; increasing values indicate degrees of rotation in the clockwise direction.
        /// This field defaults to 0 if the device does not report orientation.
        /// </summary>
        public uint Orientation;

        /// <summary>
        /// A pen pressure normalized to a range between 0 and 1024. The default is 0 if the device does not report pressure.
        /// </summary>
        public uint Pressure;

        public static bool operator ==(POINTER_TOUCH_INFO left, POINTER_TOUCH_INFO right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(POINTER_TOUCH_INFO left, POINTER_TOUCH_INFO right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Create a <see cref="POINTER_TOUCH_INFO"/> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point"/>.</param>
        /// <param name="flags">The <see cref="POINTER_FLAG"/>.</param>
        /// <param name="id">The id of the point, only needed when more than one.</param>
        /// <returns>A <see cref="POINTER_TOUCH_INFO"/> structure.</returns>
        public static POINTER_TOUCH_INFO Create(Point point, POINTER_FLAG flags, uint id = 0)
        {
            var touchPoint = POINT.Create(point);
            var contact = new POINTER_TOUCH_INFO
            {
                PointerInfo =
                {
                    PointerType = POINTER_INPUT_TYPE.PT_TOUCH,
                    PointerFlags = flags,
                    PtPixelLocation = touchPoint,
                    PointerId = id,
                },
                TouchFlags = TOUCH_FLAGS.NONE,
                TouchMasks = TOUCH_MASK.NONE,
                Contact = RECT.Create(touchPoint, 0),
            };

            return contact;
        }

        public void Move(int deltaX, int deltaY)
        {
            var oldLocation = this.PointerInfo.PtPixelLocation;
            this.PointerInfo.PtPixelLocation = new POINT
            {
                X = oldLocation.X + deltaX,
                Y = oldLocation.Y + deltaY,
            };

            var oldArea = this.Contact;
            this.Contact = new RECT
            {
                Left = oldArea.Left + deltaX,
                Top = oldArea.Top + deltaY,
                Right = oldArea.Right + deltaX,
                Bottom = oldArea.Bottom + deltaY,
            };
        }

        /// <inheritdoc />
        public bool Equals(POINTER_TOUCH_INFO other)
        {
            return this.PointerInfo.Equals(other.PointerInfo) &&
                   this.TouchFlags == other.TouchFlags &&
                   this.TouchMasks == other.TouchMasks &&
                   this.Contact.Equals(other.Contact) &&
                   this.ContactAreaRaw.Equals(other.ContactAreaRaw) &&
                   this.Orientation == other.Orientation &&
                   this.Pressure == other.Pressure;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is POINTER_TOUCH_INFO other &&
                                                   this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.PointerInfo.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)this.TouchFlags;
                hashCode = (hashCode * 397) ^ (int)this.TouchMasks;
                hashCode = (hashCode * 397) ^ this.Contact.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ContactAreaRaw.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)this.Orientation;
                hashCode = (hashCode * 397) ^ (int)this.Pressure;
                return hashCode;
            }
        }
    }
}
