// ReSharper disable UnassignedField.Global
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagpointer_info.
    /// </summary>
    public struct POINTER_INFO : IEquatable<POINTER_INFO>
    {
        /// <summary>
        /// A value from the <see cref="POINTER_INPUT_TYPE"/> enumeration that specifies the pointer type.
        /// </summary>
        public POINTER_INPUT_TYPE PointerType;

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
        /// May be any reasonable combination of flags from the <see cref="POINTER_FLAG"/> constants.
        /// </summary>
        public POINTER_FLAG PointerFlags;

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
        public POINT PtPixelLocation;

        /// <summary>
        /// The predicted screen coordinates of the pointer, in HIMETRIC units.
        /// The predicted value is based on the pointer position reported by the digitizer and the motion of the pointer.
        /// This correction can compensate for visual lag due to inherent delays in sensing and processing the pointer location on the digitizer.
        /// This is applicable to pointers of type PT_TOUCH.For other pointer types, the predicted value will be the same as the non-predicted value (see ptHimetricLocationRaw).
        /// </summary>
        public POINT PtPixelLocationRaw;

        /// <summary>
        /// The screen coordinates of the pointer, in pixels. For adjusted screen coordinates, see ptPixelLocation.
        /// </summary>
        public POINT PtHimetricLocation;

        /// <summary>
        /// The screen coordinates of the pointer, in HIMETRIC units. For adjusted screen coordinates, see ptHimetricLocation.
        /// </summary>
        public POINT PtHimetricLocationRaw;

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
        public POINTER_BUTTON_CHANGE_TYPE ButtonChangeType;

        public static bool operator ==(POINTER_INFO left, POINTER_INFO right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(POINTER_INFO left, POINTER_INFO right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public bool Equals(POINTER_INFO other)
        {
            return this.PointerType == other.PointerType &&
                   this.PointerId == other.PointerId &&
                   this.FrameId == other.FrameId &&
                   this.PointerFlags == other.PointerFlags &&
                   this.SourceDevice.Equals(other.SourceDevice) &&
                   this.HwndTarget.Equals(other.HwndTarget) &&
                   this.PtPixelLocation.Equals(other.PtPixelLocation) &&
                   this.PtPixelLocationRaw.Equals(other.PtPixelLocationRaw) &&
                   this.PtHimetricLocation.Equals(other.PtHimetricLocation) &&
                   this.PtHimetricLocationRaw.Equals(other.PtHimetricLocationRaw) &&
                   this.DwTime == other.DwTime &&
                   this.HistoryCount == other.HistoryCount &&
                   this.InputData == other.InputData &&
                   this.DwKeyStates == other.DwKeyStates &&
                   this.PerformanceCount == other.PerformanceCount &&
                   this.ButtonChangeType == other.ButtonChangeType;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is POINTER_INFO other &&
                                                   this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)this.PointerType;
                hashCode = (hashCode * 397) ^ (int)this.PointerId;
                hashCode = (hashCode * 397) ^ (int)this.FrameId;
                hashCode = (hashCode * 397) ^ (int)this.PointerFlags;
                hashCode = (hashCode * 397) ^ this.SourceDevice.GetHashCode();
                hashCode = (hashCode * 397) ^ this.HwndTarget.GetHashCode();
                hashCode = (hashCode * 397) ^ this.PtPixelLocation.GetHashCode();
                hashCode = (hashCode * 397) ^ this.PtPixelLocationRaw.GetHashCode();
                hashCode = (hashCode * 397) ^ this.PtHimetricLocation.GetHashCode();
                hashCode = (hashCode * 397) ^ this.PtHimetricLocationRaw.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)this.DwTime;
                hashCode = (hashCode * 397) ^ (int)this.HistoryCount;
                hashCode = (hashCode * 397) ^ (int)this.InputData;
                hashCode = (hashCode * 397) ^ (int)this.DwKeyStates;
                hashCode = (hashCode * 397) ^ this.PerformanceCount.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)this.ButtonChangeType;
                return hashCode;
            }
        }
    }
}
