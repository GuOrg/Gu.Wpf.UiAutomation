// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    [Flags]
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum MouseEventFlags : uint
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        /// <summary>
        /// Movement occurred.
        /// </summary>
        MOUSEEVENTF_MOVE = 0x0001,

        /// <summary>
        /// The left button is down.
        /// </summary>
        MOUSEEVENTF_LEFTDOWN = 0x0002,

        /// <summary>
        /// The left button is up.
        /// </summary>
        MOUSEEVENTF_LEFTUP = 0x0004,

        /// <summary>
        /// The right button is down.
        /// </summary>
        MOUSEEVENTF_RIGHTDOWN = 0x0008,

        /// <summary>
        /// The right button is up.
        /// </summary>
        MOUSEEVENTF_RIGHTUP = 0x0010,

        /// <summary>
        /// The middle button is down.
        /// </summary>
        MOUSEEVENTF_MIDDLEDOWN = 0x0020,

        /// <summary>
        /// The middle button is up.
        /// </summary>
        MOUSEEVENTF_MIDDLEUP = 0x0040,

        /// <summary>
        /// An X button was pressed. Set which button in data.
        /// </summary>
        MOUSEEVENTF_XDOWN = 0x0080,

        /// <summary>
        /// An X button was released. Set which button in data.
        /// </summary>
        MOUSEEVENTF_XUP = 0x0100,

        /// <summary>
        /// The wheel has been moved, if the mouse has a wheel. The amount of movement is specified in dwData
        /// </summary>
        MOUSEEVENTF_WHEEL = 0x0800,

        /// <summary>
        /// The wheel button is tilted. >= Win Vista only
        /// </summary>
        MOUSEEVENTF_HWHEEL = 0x1000,
        MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000,
        MOUSEEVENTF_VIRTUALDESK = 0x4000,

        /// <summary>
        /// The dx and dy parameters contain normalized absolute coordinates.
        /// If not set, those parameters contain relative data: the change in position since the last reported position.
        /// This flag can be set, or not set, regardless of what kind of mouse or mouse-like device, if any, is connected to the system.
        /// For further information about relative mouse motion, see the following Remarks section.
        /// </summary>
        MOUSEEVENTF_ABSOLUTE = 0x8000,
    }
}
