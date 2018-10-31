// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    [Flags]
    public enum VkKeyScanModifiers : byte
    {
        NONE = 0,
        SHIFT = 0x01,
        CONTROL = 0x02,
        ALT = 0x04,
        Hankaku = 0x08,
        Reserved1 = 0x10,
        Reserved2 = 0x20,
    }
}