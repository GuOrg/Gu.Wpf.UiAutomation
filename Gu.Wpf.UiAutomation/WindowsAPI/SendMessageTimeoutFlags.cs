// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    [Flags]
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum SendMessageTimeoutFlags : uint
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        SMTO_NORMAL = 0x0,
        SMTO_BLOCK = 0x1,
        SMTO_ABORTIFHUNG = 0x2,
        SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
        SMTO_ERRORONEXIT = 0x20,
    }
}
