namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;
    using Gu.Wpf.UiAutomation.Logging;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <summary>
    /// Keyboard class to simulate key input.
    /// </summary>
    public static class Keyboard
    {
        /// <summary>
        /// Types the given text, one char after another.
        /// </summary>
        public static void Type(string text)
        {
            foreach (var c in text)
            {
                Type(c);
            }
        }

        /// <summary>
        /// Types the given character.
        /// </summary>
        public static void Type(char character)
        {
            var code = User32.VkKeyScan(character);

            // Check if the char is unicode or no virtual key could be found
            if (character > 0xFE || code == -1)
            {
                // It seems to be unicode
                SendInput(character, KeyEventFlags.KEYEVENTF_KEYDOWN | KeyEventFlags.KEYEVENTF_UNICODE);
                SendInput(character, KeyEventFlags.KEYEVENTF_KEYUP | KeyEventFlags.KEYEVENTF_UNICODE);
            }
            else
            {
                // Get the high-order and low-order byte from the code
                var high = (byte)(code >> 8);
                var low = (byte)(code & 0xff);

                // Check if there are any modifiers
                var modifiers = new List<Key>();
                if (HasScanModifier(high, VkKeyScanModifiers.SHIFT))
                {
                    modifiers.Add(Key.SHIFT);
                }

                if (HasScanModifier(high, VkKeyScanModifiers.CONTROL))
                {
                    modifiers.Add(Key.CONTROL);
                }

                if (HasScanModifier(high, VkKeyScanModifiers.ALT))
                {
                    modifiers.Add(Key.ALT);
                }

                // Press the modifiers
                foreach (var mod in modifiers)
                {
                    SendInput((ushort)mod, KeyEventFlags.KEYEVENTF_KEYDOWN);
                }

                // Type the effective key
                SendInput(low, KeyEventFlags.KEYEVENTF_KEYDOWN);
                SendInput(low, KeyEventFlags.KEYEVENTF_KEYUP);

                // Release the modifiers
                foreach (var mod in Enumerable.Reverse(modifiers))
                {
                    SendInput((ushort)mod, KeyEventFlags.KEYEVENTF_KEYUP);
                }
            }
        }

        /// <summary>
        /// Types the given keys, one by one.
        /// </summary>
        public static void Type(params Key[] keys)
        {
            if (keys == null)
            {
                return;
            }

            foreach (var key in keys)
            {
                SendInput((ushort)key, KeyEventFlags.KEYEVENTF_KEYDOWN);
                SendInput((ushort)key, KeyEventFlags.KEYEVENTF_KEYUP);
            }
        }

        /// <summary>
        /// Types the given keys simultaneously (starting with the first).
        /// </summary>
        public static void TypeSimultaneously(params Key[] keys)
        {
            if (keys == null)
            {
                return;
            }

            foreach (var key in keys)
            {
                SendInput((ushort)key, KeyEventFlags.KEYEVENTF_KEYDOWN);
            }

            foreach (var key in keys.Reverse())
            {
                SendInput((ushort)key, KeyEventFlags.KEYEVENTF_KEYUP);
            }
        }

        /// <summary>
        /// Types the given scan-code.
        /// </summary>
        public static void TypeScanCode(ushort scanCode, bool isExtendedKey)
        {
            SendInput(scanCode, KeyEventFlags.KEYEVENTF_KEYDOWN | KeyEventFlags.KEYEVENTF_SCANCODE, isExtended: isExtendedKey);
            SendInput(scanCode, KeyEventFlags.KEYEVENTF_KEYUP | KeyEventFlags.KEYEVENTF_SCANCODE, isExtended: isExtendedKey);
        }

        /// <summary>
        /// Types the given virtual key-code.
        /// </summary>
        public static void TypeVirtualKeyCode(ushort virtualKeyCode)
        {
            SendInput(virtualKeyCode, KeyEventFlags.KEYEVENTF_KEYDOWN);
            SendInput(virtualKeyCode, KeyEventFlags.KEYEVENTF_KEYUP);
        }

        /// <summary>
        /// Presses the given key and releases it when the returned object is disposed.
        /// </summary>
        public static IDisposable Hold(Key key)
        {
            return new PressedKey(key);
        }

        /// <summary>
        /// Presses the given key.
        /// </summary>
        [Obsolete("Prefer Hold")]
        public static void Press(Key key)
        {
            PressVirtualKeyCode((ushort)key);
        }

        /// <summary>
        /// Presses the given scan-code.
        /// </summary>
        [Obsolete("Prefer Hold")]
        public static void PressScanCode(ushort scanCode, bool isExtendedKey)
        {
            SendInput(scanCode, KeyEventFlags.KEYEVENTF_KEYDOWN | KeyEventFlags.KEYEVENTF_SCANCODE, isExtended: isExtendedKey);
        }

        /// <summary>
        /// Presses the given virtual key-code.
        /// </summary>
        [Obsolete("Prefer Hold")]
        public static void PressVirtualKeyCode(ushort virtualKeyCode)
        {
            SendInput(virtualKeyCode, KeyEventFlags.KEYEVENTF_KEYDOWN);
        }

        /// <summary>
        /// Releases the given key.
        /// </summary>
        public static void Release(Key key)
        {
            ReleaseVirtualKeyCode((ushort)key);
        }

        /// <summary>
        /// Releases the given scan-code.
        /// </summary>
        public static void ReleaseScanCode(ushort scanCode, bool isExtendedKey)
        {
            SendInput(scanCode, KeyEventFlags.KEYEVENTF_KEYUP | KeyEventFlags.KEYEVENTF_SCANCODE, isExtended: isExtendedKey);
        }

        /// <summary>
        /// Releases the given virtual key-code.
        /// </summary>
        public static void ReleaseVirtualKeyCode(ushort virtualKeyCode)
        {
            SendInput(virtualKeyCode, KeyEventFlags.KEYEVENTF_KEYUP);
        }

        /// <summary>
        /// Presses the given key and releases it when the returned object is disposed.
        /// </summary>
        [Obsolete("Renamed to Hold")]
        public static IDisposable Pressing(Key key)
        {
            return new PressedKey(key);
        }

        public static void ClearFocus()
        {
            _ = User32.SetFocus(IntPtr.Zero);
        }

        /// <summary>
        /// Checks if a given byte has a specific VkKeyScan-modifier set.
        /// </summary>
        private static bool HasScanModifier(byte b, VkKeyScanModifiers modifierToTest)
        {
            return (VkKeyScanModifiers)(b & (byte)modifierToTest) == modifierToTest;
        }

        /// <summary>
        /// Effectively sends the keyboard input command.
        /// </summary>
        /// <param name="keyCode">The key code to send. Can be the scan code or the virtual key code.</param>
        /// <param name="keyFlags">Flag if the key should be pressed or released.</param>
        /// <param name="isExtended">Flag if the key is an extended key.</param>
        private static void SendInput(ushort keyCode, KeyEventFlags keyFlags, bool isExtended = false)
        {
            // Prepare the basic object
            var keyboardInput = new KEYBDINPUT
            {
                dwExtraInfo = User32.GetMessageExtraInfo(),
                dwFlags = keyFlags,
            };

            if (keyFlags.HasFlag(KeyEventFlags.KEYEVENTF_SCANCODE))
            {
                keyboardInput.wScan = keyCode;

                // Add the extended flag if the flag is set or the keycode is prefixed with the byte 0xE0
                // See https://msdn.microsoft.com/en-us/library/windows/desktop/ms646267(v=vs.85).aspx
                if (isExtended ||
                    (keyCode & 0xFF00) == 0xE0)
                {
                    keyboardInput.dwFlags |= KeyEventFlags.KEYEVENTF_EXTENDEDKEY;
                }
            }
            else if (keyFlags.HasFlag(KeyEventFlags.KEYEVENTF_UNICODE))
            {
                keyboardInput.wScan = keyCode;
            }
            else
            {
                keyboardInput.wVk = keyCode;
            }

            SendInput(keyboardInput);
        }

        [PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
        private static void SendInput(KEYBDINPUT keyboardInput)
        {
            // Demand the correct permissions
            var permissions = new PermissionSet(PermissionState.Unrestricted);
            permissions.Demand();

            if (User32.SendInput(1, new[] { INPUT.KeyboardInput(keyboardInput) }, INPUT.Size) == 0)
            {
                throw new Win32Exception();
            }

            Wait.For(TimeSpan.FromMilliseconds(10));
        }

        /// <summary>Disposable class which presses the key on creation and releases it on dispose.</summary>
        private class PressedKey : IDisposable
        {
            private readonly Key key;

            public PressedKey(Key key)
            {
                this.key = key;
                SendInput((ushort)key, KeyEventFlags.KEYEVENTF_KEYDOWN);
            }

            public void Dispose()
            {
                SendInput((ushort)this.key, KeyEventFlags.KEYEVENTF_KEYUP);
            }
        }
    }
}
