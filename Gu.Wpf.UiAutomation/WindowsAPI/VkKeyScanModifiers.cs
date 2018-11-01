// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    [Flags]
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum VkKeyScanModifiers : byte
#pragma warning restore CA1028 // Enum Storage should be Int32
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
