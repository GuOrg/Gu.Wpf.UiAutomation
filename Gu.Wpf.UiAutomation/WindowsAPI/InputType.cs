// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum InputType : uint
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        INPUT_MOUSE = 0,
        INPUT_KEYBOARD = 1,
        INPUT_HARDWARE = 2,
    }
}
