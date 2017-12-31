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

        public bool IsModal => this.AutomationElement.WindowPattern().Current.IsModal;

        public TitleBar TitleBar => this.FindFirstChild(Condition.TitleBar)?.AsTitleBar();

        public IReadOnlyList<Window> ModalWindows
        {
            get
            {
                return this.FindAllChildren(
                               new AndCondition(
                                   Condition.Window,
                                   new PropertyCondition(WindowPatternIdentifiers.IsModalProperty, true)))
                           .Select(e => new Window(e.AutomationElement, isMainWindow: false))
                           .ToArray();
            }
        }

        /// <summary>
        /// Gets the current WPF popup window
        /// </summary>
        public Popup Popup
        {
            get
            {
                var mainWindow = this.GetMainWindow();
                var popup = mainWindow.FindFirstChild(
                    new AndCondition(
                        Condition.Window,
                        Condition.ByName(string.Empty),
                        Condition.ByClassName("Popup")));
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
        public ContextMenu ContextMenu => this.GetContextMenuByFrameworkType(this.FrameworkType);

        public IntPtr NativeWindowHandle => new IntPtr(this.AutomationElement.NativeWindowHandle());

        public MessageBox FindMessageBox() =>
            this.FindFirstDescendant(Condition.ByClassName(MessageBox.ClassNameString))?.AsMessageBox() ??
            throw new InvalidOperationException("Did not find a message box");

        public Window FindDialog() => this.ModalWindows.FirstOrDefault() ?? throw new InvalidOperationException("Did not find a dialog");

        public ContextMenu GetContextMenuByFrameworkType(FrameworkType frameworkType)
        {
            if (frameworkType == FrameworkType.Win32)
            {
                this.WaitUntilResponsive();

                // The main menu is directly under the desktop with the name "Context" or in a few cases "System"
                if (Desktop.AutomationElement.TryFindFirst(
                    TreeScope.Children,
                    new AndCondition(
                        Condition.Menu,
                        new OrCondition(
                            Condition.ByName("Context"),
                            Condition.ByName("System"))),
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
                var ctxMenu = mainWindow.FindFirstChild(
                    new AndCondition(
                        Condition.Menu,
                        Condition.ByName("DropDown")));
                return ctxMenu?.AsContextMenu() ?? throw new InvalidOperationException("Could not find ControlType.Menu with name DropDown");
            }

            if (frameworkType == FrameworkType.Wpf)
            {
                // In WPF, there is a window (Popup) where the menu is inside
                var popup = this.Popup;
                var ctxMenu = popup.FindFirstChild(Condition.Menu);
                return ctxMenu.AsContextMenu();
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

        public void Move(int x, int y)
        {
            this.AutomationElement.TransformPattern().Move(x, y);
        }

        /// <summary>
        /// Brings the element to the foreground
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

        public UiElement FocusedElement()
        {
            return new UiElement(AutomationElement.FocusedElement);
        }

        /// <summary>
        /// Gets the main window (first window on desktop with the same process as this window)
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
                                               Condition.ByProcessId(this.ProcessId));
            var mainWindow = new Window(element, isMainWindow: true);
            return mainWindow;
        }
    }
}
