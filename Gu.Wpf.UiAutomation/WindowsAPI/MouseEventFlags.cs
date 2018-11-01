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
        MOUSEEVENTF_MOVE = 0x0001,
        MOUSEEVENTF_LEFTDOWN = 0x0002,
        MOUSEEVENTF_LEFTUP = 0x0004,
        MOUSEEVENTF_RIGHTDOWN = 0x0008,
        MOUSEEVENTF_RIGHTUP = 0x0010,
        MOUSEEVENTF_MIDDLEDOWN = 0x0020,
        MOUSEEVENTF_MIDDLEUP = 0x0040,
        MOUSEEVENTF_XDOWN = 0x0080,
        MOUSEEVENTF_XUP = 0x0100,
        MOUSEEVENTF_WHEEL = 0x0800,
        MOUSEEVENTF_HWHEEL = 0x1000, // >= Win Vista only
        MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000,
        MOUSEEVENTF_VIRTUALDESK = 0x4000,
        MOUSEEVENTF_ABSOLUTE = 0x8000,
    }
}
