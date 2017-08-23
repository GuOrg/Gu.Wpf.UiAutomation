namespace Gu.Wpf.UiAutomation
{
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Represents a menuitem which can also contain sub-menuitems
    /// </summary>
    public class MenuItem : AutomationElement
    {
        private readonly InvokeAutomationElement invokeAutomationElement;
        private readonly ExpandCollapseAutomationElement expandCollapseAutomationElement;

        public MenuItem(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
            this.invokeAutomationElement = new InvokeAutomationElement(basicAutomationElement);
            this.expandCollapseAutomationElement = new ExpandCollapseAutomationElement(basicAutomationElement);
        }

        /// <summary>
        /// Flag to indicate if the containing menu is a Win32 menu because that one needs special handling
        /// </summary>
        internal bool IsWin32Menu { get; set; }

        public string Text => this.Properties.Name.Value;

        public MenuItems SubMenuItems
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
                    var appWindow = this.BasicAutomationElement.Automation.GetDesktop().FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(this.Properties.ProcessId)));

                    // Then search the menu below the window
                    var menu = appWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName(this.Text))).AsMenu();
                    menu.IsWin32Menu = true;

                    // Now return the menu items
                    return menu.MenuItems;
                }

                // Expand if needed, WinForms does not have the expand pattern but all children are already visible so it works as well
                if (this.Patterns.ExpandCollapse.IsSupported)
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

                var childItems = Enumerable.Select(this.FindAllChildren(cf => cf.ByControlType(ControlType.MenuItem)), e => e.AsMenuItem());
                return new MenuItems(childItems);
            }
        }

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