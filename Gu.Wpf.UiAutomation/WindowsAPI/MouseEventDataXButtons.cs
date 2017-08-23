namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    [Flags]
    public enum MouseEventDataXButtons : uint
    {
        NOTHING = 0x00000000,
        XBUTTON1 = 0x00000001,
        XBUTTON2 = 0x00000002
    }
}