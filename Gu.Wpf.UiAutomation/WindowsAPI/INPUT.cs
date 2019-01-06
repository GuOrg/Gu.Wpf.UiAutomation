// ReSharper disable InconsistentNaming
#pragma warning disable CA1815 // Override equals and operator equals on value types
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable CA1051 // Do not declare visible instance fields
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public InputType type;
        public INPUTUNION u;

        public static int Size => Marshal.SizeOf(typeof(INPUT));

        public static INPUT MouseInput(MOUSEINPUT mouseInput)
        {
            return new INPUT
            {
                type = InputType.INPUT_MOUSE,
                u = new INPUTUNION { mi = mouseInput },
            };
        }

        public static INPUT KeyboardInput(KEYBDINPUT keyboardInput)
        {
            return new INPUT
            {
                type = InputType.INPUT_KEYBOARD,
                u = new INPUTUNION { ki = keyboardInput },
            };
        }

        public static INPUT HardwareInput(HARDWAREINPUT hardwareInput)
        {
            return new INPUT
            {
                type = InputType.INPUT_HARDWARE,
                u = new INPUTUNION { hi = hardwareInput },
            };
        }
    }
}
