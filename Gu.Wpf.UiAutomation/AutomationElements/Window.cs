namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class Window : AutomationElement
    {
        public Window(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Title => this.Properties.Name.Value;

        public bool IsModal => this.Patterns.Window.Pattern.IsModal.Value;

        public TitleBar TitleBar => this.FindFirstChild(cf => cf.ByControlType(ControlType.TitleBar))?.AsTitleBar();

        public IReadOnlyList<Window> ModalWindows
        {
            get
            {
                return this.FindAllChildren(cf =>
                    cf.ByControlType(ControlType.Window).
                    And(new PropertyCondition(this.Automation.PropertyLibrary.Window.IsModal, true)))
                    .Select(e => e.AsWindow())
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
                var popup = mainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Window)
                                                              .And(cf.ByText(string.Empty)
                                                              .And(cf.ByClassName("Popup"))));
                if (popup == null)
                {
                    throw new InvalidOperationException("Did not find a popup");
                }

                return new Popup(popup.BasicAutomationElement);
            }
        }

        /// <summary>
        /// Gets the contest menu for the window.
        /// Note: It uses the FrameworkType of the window as lookup logic. Use <see cref="GetContextMenuByFrameworkType" /> if you want to control this.
        /// </summary>
        public ContextMenu ContextMenu => this.GetContextMenuByFrameworkType(this.FrameworkType);

        /// <summary>
        /// Flag to indicate, if the window is the application's main window.
        /// Is used so that it does not need to be looked up again in some cases (e.g. Context Menu).
        /// </summary>
        internal bool IsMainWindow { get; set; }

        public MessageBox FindMessageBox() => this.FindFirstDescendant(cf => cf.ByClassName(MessageBox.ClassNameString))?.AsMessageBox() ?? throw new InvalidOperationException("Did not find a message box");

        public Window FindDialog() => this.ModalWindows.FirstOrDefault() ?? throw new InvalidOperationException("Did not find a dialog");

        public ContextMenu GetContextMenuByFrameworkType(FrameworkType frameworkType)
        {
            if (frameworkType == FrameworkType.Win32)
            {
                // The main menu is directly under the desktop with the name "Context" or in a few cases "System"
                var desktop = this.BasicAutomationElement.Automation.GetDesktop();
                var nameCondition = this.ConditionFactory.ByName("Context").Or(this.ConditionFactory.ByName("System"));
                var ctxMenu = desktop.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(nameCondition)).AsContextMenu();
                if (ctxMenu != null)
                {
                    ctxMenu.IsWin32Menu = true;
                    return ctxMenu;
                }
            }

            var mainWindow = this.GetMainWindow();
            if (frameworkType == FrameworkType.WinForms)
            {
                var ctxMenu = mainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName("DropDown")));
                return ctxMenu.AsContextMenu();
            }

            if (frameworkType == FrameworkType.Wpf)
            {
                // In WPF, there is a window (Popup) where the menu is inside
                var popup = this.Popup;
                var ctxMenu = popup.FindFirstChild(cf => cf.ByControlType(ControlType.Menu));
                return ctxMenu.AsContextMenu();
            }

            // No menu found
            return null;
        }

        public void WaitUntilResponsive() => Wait.UntilResponsive(this);

        public void Close()
        {
            if (!OperatingSystem.IsWindows7())
            {
                var closeButton = this.TitleBar?.CloseButton;
                if (closeButton != null)
                {
                    closeButton.Invoke();
                    return;
                }
            }

            var windowPattern = this.Patterns.Window.PatternOrDefault;
            if (windowPattern != null)
            {
                windowPattern.Close();
                return;
            }

            throw new MethodNotSupportedException("Close is not supported");
        }

        public void Move(int x, int y)
        {
            this.Patterns.Transform.PatternOrDefault?.Move(x, y);
        }

        /// <summary>
        /// Brings the element to the foreground
        /// </summary>
        public void SetTransparency(byte alpha)
        {
            if (User32.SetWindowLong(this.Properties.NativeWindowHandle, WindowLongParam.GWL_EXSTYLE, WindowStyles.WS_EX_LAYERED) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!User32.SetLayeredWindowAttributes(this.Properties.NativeWindowHandle, 0, alpha, LayeredWindowAttributes.LWA_ALPHA))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
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

            var mainWindow = this.Automation.GetDesktop()
                                 .FindFirstChild(cf => cf.ByProcessId(this.Properties.ProcessId.Value))
                                 .AsWindow();
            return mainWindow ?? this;
        }
    }
}
