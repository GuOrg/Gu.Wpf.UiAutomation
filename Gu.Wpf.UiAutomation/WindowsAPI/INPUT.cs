// ReSharper disable InconsistentNaming
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable CA1724 // Do not declare visible instance fields
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT : IEquatable<INPUT>
    {
        public InputType type;
        public INPUTUNION u;

        public static int Size => Marshal.SizeOf(typeof(INPUT));

        public static bool operator ==(INPUT left, INPUT right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(INPUT left, INPUT right)
        {
            return !left.Equals(right);
        }

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

        /// <inheritdoc />
        public bool Equals(INPUT other) => this.type == other.type &&
                                           this.u.Equals(other.u);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is INPUT other && this.Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)this.type * 397) ^ this.u.GetHashCode();
            }
        }
    }
}
