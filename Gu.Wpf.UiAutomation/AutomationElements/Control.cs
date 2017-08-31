namespace Gu.Wpf.UiAutomation
{
    using System;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class Control : AutomationElement
    {
        public Control(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public Control(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Get a value indicating if the element is enabled or not.
        /// </summary>
        public bool IsEnabled => this.Properties.IsEnabled.Value;

        /// <summary>
        /// Get a value indicating if the element can recieve keyboard focus.
        /// </summary>
        public bool IsKeyboardFocusable => this.Properties.IsKeyboardFocusable.Value;

        /// <summary>
        /// Get a value indicating if the element has keyboard focus.
        /// </summary>
        public bool HasKeyboardFocus => this.Properties.HasKeyboardFocus.Value;

        /// <summary>
        /// Gets a string containing the accelerator key combinations for the element.
        /// </summary>
        public string AcceleratorKey => this.Properties.AcceleratorKey.Value;

        /// <summary>
        /// Performs a left click on the element.
        /// </summary>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void Click(bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.LeftClick);
        }

        /// <summary>
        /// Performs a left click on the element.
        /// </summary>
        /// <param name="delay">The time to wait after the click. Useful if there is an animation for example.</param>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void Click(TimeSpan delay, bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.LeftClick);
            Wait.For(delay);
        }

        /// <summary>
        /// Performs a double left click on the element.
        /// </summary>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void DoubleClick(bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.LeftDoubleClick);
        }

        /// <summary>
        /// Performs a right click on the element.
        /// </summary>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void RightClick(bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.RightClick);
        }

        /// <summary>
        /// Performs a right click on the element.
        /// </summary>
        /// <param name="delay">The time to wait after the click. Useful if there is an animation for example.</param>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void RightClick(TimeSpan delay, bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.RightClick);
            Wait.For(delay);
        }

        /// <summary>
        /// Performs a double right click on the element.
        /// </summary>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void RightDoubleClick(bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.RightDoubleClick);
        }

        /// <summary>
        /// Sets the focus to this element.
        /// Warning: This can be unreliable! <see cref="SetForeground()" /> should be more reliable.
        /// </summary>
        public virtual void Focus()
        {
            this.BasicAutomationElement.SetFocus();
        }

        /// <summary>
        /// Sets the focus by using the Win32 SetFocus() method.
        /// </summary>
        public void FocusNative()
        {
            var windowHandle = this.Properties.NativeWindowHandle;
            if (windowHandle != new IntPtr(0))
            {
                User32.SetFocus(windowHandle);
                Wait.UntilResponsive(this);
            }
            else
            {
                // Fallback to the UIA Version
                this.Focus();
            }
        }

        /// <summary>
        /// Brings the element to the foreground.
        /// </summary>
        public void SetForeground()
        {
            var windowHandle = this.Properties.NativeWindowHandle;
            if (windowHandle != new IntPtr(0))
            {
                User32.SetForegroundWindow(windowHandle);
                Wait.UntilResponsive(this);
            }
            else
            {
                // Fallback to the UIA Version
                this.Focus();
            }
        }
    }
}