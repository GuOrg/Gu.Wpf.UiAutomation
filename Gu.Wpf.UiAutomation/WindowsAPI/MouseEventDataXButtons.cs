// ReSharper disable InconsistentNaming
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    [Flags]
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum MouseEventDataXButtons : uint
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        NOTHING = 0x00000000,
        XBUTTON1 = 0x00000001,
        XBUTTON2 = 0x00000002,
    }
}
