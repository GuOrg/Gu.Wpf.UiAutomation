namespace Gu.Wpf.UiAutomation
{
    using System.Threading;
    using System.Windows.Automation;

    /// <summary>
    /// Represents a menuitem which can also contain sub-menuitems
    /// </summary>
    public class MenuItem : Control
    {
        private readonly InvokeAutomationElement invokeAutomationElement;
        private readonly ExpandCollapseAutomationElement expandCollapseAutomationElement;

        public MenuItem(AutomationElement automationElement)
            : base(automationElement)
        {
            this.invokeAutomationElement = new InvokeAutomationElement(automationElement);
            this.expandCollapseAutomationElement = new ExpandCollapseAutomationElement(automationElement);
        }

        public string Text => this.AutomationElement.Name();

        public MenuItems Items
        {
            get
            {
                // Special handling for Win32 context menus
                if (this.IsWin32Menu)
                {
                    // Click the item to load the child items
                    this.Click();

                    // In Win32, the nested menu items are below a menu control which is below the application window
                    // So search the app window first
                    var appWindow = this.AutomationElement.GetDesktop()
                                        .FindFirstChild(
                                            new AndCondition(
                                                Condition.ByControlType(ControlType.Window),
                                                Condition.ByProcessId(this.ProcessId)));

                    // Then search the menu below the window
                    var menu = appWindow.FindFirstChild(
                                            new AndCondition(
                                                Condition.ByControlType(ControlType.Menu),
                                                Condition.ByName(this.Text)))
                                        .AsMenu();
                    menu.IsWin32Menu = true;

                    // Now return the menu items
                    return menu.Items;
                }

                // Expand if needed, WinForms does not have the expand pattern but all children are already visible so it works as well
                if (this.AutomationElement.TryGetExpandCollapsePattern(out _))
                {
                    ExpandCollapseState state;
                    do
                    {
                        state = this.expandCollapseAutomationElement.ExpandCollapseState;
                        if (state == ExpandCollapseState.Collapsed)
                        {
                            this.Expand();
                        }

                        Thread.Sleep(50);
                    }
                    while (state != ExpandCollapseState.Expanded);
                }

                var childItems = this.FindAllChildren(
                    Condition.ByControlType(ControlType.MenuItem),
                    x => new MenuItem(x) { IsWin32Menu = this.IsWin32Menu });
                return new MenuItems(childItems);
            }
        }

        /// <summary>
        /// Flag to indicate if the containing menu is a Win32 menu because that one needs special handling
        /// </summary>
        internal bool IsWin32Menu { get; set; }

        public MenuItem Invoke()
        {
            this.invokeAutomationElement.Invoke();
            return this;
        }

        public MenuItem Expand()
        {
            this.expandCollapseAutomationElement.Expand();
            return this;
        }

        public MenuItem Collapse()
        {
            this.expandCollapseAutomationElement.Collapse();
            return this;
        }
    }
}