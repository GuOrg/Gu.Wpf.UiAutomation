namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Automation;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class Window : UiElement
    {
        public Window(AutomationElement automationElement)
        : this(automationElement, Equals(automationElement.Parent(), Desktop.AutomationElement))
        {
        }

        public Window(AutomationElement automationElement, bool isMainWindow)
            : base(automationElement)
        {
            this.IsMainWindow = isMainWindow;
        }

        /// <summary>
        /// Flag to indicate, if the window is the application's main window.
        /// Is used so that it does not need to be looked up again in some cases (e.g. Context Menu).
        /// </summary>
        public bool IsMainWindow { get; }

        public string Title => this.Name;

        /// <summary>
        /// Get TransformPattern.CanResize.
        /// </summary>
        public bool CanResize => this.AutomationElement.TryGetTransformPattern(out var valuePattern) &&
                                 valuePattern.Current.CanResize;

        /// <summary>
        /// Get TransformPattern.CanMove.
        /// </summary>
        public bool CanMove => this.AutomationElement.TryGetTransformPattern(out var valuePattern) &&
                                 valuePattern.Current.CanMove;

        public bool IsModal => this.AutomationElement.WindowPattern().Current.IsModal;

        public TitleBar TitleBar => new TitleBar(this.AutomationElement.FindFirstChild(Conditions.TitleBar));

        public IReadOnlyList<Window> ModalWindows => this.FindAllChildren(Conditions.ModalWindow)
                                                         .Select(e => new Window(e.AutomationElement, isMainWindow: false))
                                                         .ToArray();

        /// <summary>
        /// Gets the current WPF popup window.
        /// </summary>
        public Popup Popup
        {
            get
            {
                var mainWindow = this.GetMainWindow();
                var popup = mainWindow.FindFirstChild(
                    new AndCondition(
                        Conditions.Window,
                        Conditions.ByName(string.Empty),
                        Conditions.ByClassName(nameof(this.Popup))));
                if (popup == null)
                {
                    throw new InvalidOperationException("Did not find a popup");
                }

                return new Popup(popup.AutomationElement);
            }
        }

        /// <summary>
        /// Gets the contest menu for the window.
        /// Note: It uses the FrameworkType of the window as lookup logic. Use <see cref="GetContextMenuByFrameworkType" /> if you want to control this.
        /// </summary>
        public ContextMenu? ContextMenu => this.GetContextMenuByFrameworkType(this.FrameworkType);

        public IntPtr NativeWindowHandle => new IntPtr(this.AutomationElement.NativeWindowHandle());

        public WindowPattern WindowPattern => this.AutomationElement.WindowPattern();

        public MessageBox FindMessageBox() => new MessageBox(this.AutomationElement.FindFirstChild(Conditions.MessageBox));

        public Window FindDialog() => this.FindFirstChild(Conditions.ModalWindow, e => new Window(e, isMainWindow: false));

        public ContextMenu? GetContextMenuByFrameworkType(FrameworkType frameworkType)
        {
            if (frameworkType == FrameworkType.Win32)
            {
                this.WaitUntilResponsive();

                // The main menu is directly under the desktop with the name "Context" or in a few cases "System"
                if (Desktop.AutomationElement.TryFindFirst(
                    TreeScope.Children,
                    new AndCondition(
                        Conditions.Menu,
                        new OrCondition(
                            Conditions.ByName("Context"),
                            Conditions.ByName("System"))),
                    out var element))
                {
                    return new ContextMenu(element, isWin32Menu: true);
                }
            }

            var mainWindow = this.GetMainWindow();
            if (mainWindow == null)
            {
                throw new InvalidOperationException("Could not find MainWindow");
            }

            if (frameworkType == FrameworkType.WinForms)
            {
                var ctxMenu = mainWindow.AutomationElement.FindFirstChild(
                    new AndCondition(
                        Conditions.Menu,
                        Conditions.ByName("DropDown")));
                return new ContextMenu(ctxMenu);
            }

            if (frameworkType == FrameworkType.Wpf)
            {
                // In WPF, there is a window (Popup) where the menu is inside
                var popup = this.Popup;
                var ctxMenu = popup.AutomationElement.FindFirstChild(Conditions.Menu);
                return new ContextMenu(ctxMenu);
            }

            // No menu found
            return null;
        }

        public void WaitUntilResponsive() => Wait.UntilResponsive(this);

        public void Close()
        {
            this.WaitUntilResponsive();
            if (!WindowsVersion.IsWindows7())
            {
                var closeButton = this.TitleBar?.CloseButton;
                if (closeButton != null)
                {
                    closeButton.Invoke();
                    return;
                }
            }

            this.AutomationElement.WindowPattern().Close();
        }

        /// <summary>Moves the control.</summary>
        /// <param name="x">Absolute screen coordinates of the left side of the control.</param>
        /// <param name="y">Absolute screen coordinates of the top of the control.</param>
        /// <exception cref="System.InvalidOperationException">The <see cref="System.Windows.Automation.TransformPattern.TransformPatternInformation.CanMove" /> property is false.</exception>
        public void MoveTo(int x, int y)
        {
            this.AutomationElement.TransformPattern().Move(x, y);
        }

        /// <summary>Resizes the control.</summary>
        /// <param name="width">The new width of the window, in pixels.</param>
        /// <param name="height">The new height of the window, in pixels.</param>
        /// <exception cref="System.InvalidOperationException">The <see cref="System.Windows.Automation.TransformPattern.TransformPatternInformation.CanResize" /> property is false.</exception>
        public void Resize(int width, int height)
        {
            this.AutomationElement.TransformPattern().Resize(width, height);
        }

        /// <summary>
        /// Brings the element to the foreground.
        /// </summary>
        public void SetTransparency(byte alpha)
        {
            if (User32.SetWindowLong(this.NativeWindowHandle, WindowLongParam.GWL_EXSTYLE, WindowStyles.WS_EX_LAYERED) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!User32.SetLayeredWindowAttributes(this.NativeWindowHandle, 0, alpha, LayeredWindowAttributes.LWA_ALPHA))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

#pragma warning disable CA1822
        public UiElement FocusedElement() => FromAutomationElement(AutomationElement.FocusedElement);
#pragma warning restore CA1822

        /// <summary>
        /// Gets the main window (first window on desktop with the same process as this window).
        /// </summary>
        private Window GetMainWindow()
        {
            if (this.IsMainWindow)
            {
                return this;
            }

            var element = AutomationElement.RootElement
                                           .FindFirst(
                                               TreeScope.Children,
                                               Conditions.ByProcessId(this.ProcessId));
            var mainWindow = new Window(element, isMainWindow: true);
            return mainWindow;
        }
    }
}
