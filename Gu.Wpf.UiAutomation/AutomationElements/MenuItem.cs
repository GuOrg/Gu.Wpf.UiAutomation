namespace Gu.Wpf.UiAutomation
{
    using System.Threading;
    using System.Windows.Automation;

    /// <summary>
    /// Represents a menuitem which can also contain sub-menuitems.
    /// </summary>
    public class MenuItem : Control
    {
        private readonly InvokeControl invokeControl;
        private readonly ExpandCollapseControl expandCollapseControl;

        public MenuItem(AutomationElement automationElement)
            : base(automationElement)
        {
            this.invokeControl = new InvokeControl(automationElement);
            this.expandCollapseControl = new ExpandCollapseControl(automationElement);
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
                    // Then search the menu below the window
                    var menu = new Menu(
                        this.Window.AutomationElement.FindFirstChild(
                            new AndCondition(
                                Conditions.Menu,
                                Conditions.ByName(this.Text))),
                        isWin32Menu: true);

                    // Now return the menu items
                    return menu.Items;
                }

                // Expand if needed, WinForms does not have the expand pattern but all children are already visible so it works as well
                if (this.AutomationElement.TryGetExpandCollapsePattern(out _))
                {
                    ExpandCollapseState state;
                    do
                    {
                        state = this.expandCollapseControl.ExpandCollapseState;
                        if (state == ExpandCollapseState.Collapsed)
                        {
                            this.Expand();
                        }

                        Thread.Sleep(50);
                    }
                    while (state != ExpandCollapseState.Expanded);
                }

                var childItems = this.FindAllChildren(
                    Conditions.MenuItem,
                    x => new MenuItem(x) { IsWin32Menu = this.IsWin32Menu });
                return new MenuItems(childItems);
            }
        }

        /// <summary>
        /// Flag to indicate if the containing menu is a Win32 menu because that one needs special handling.
        /// </summary>
        internal bool IsWin32Menu { get; set; }

        public MenuItem Invoke()
        {
            this.invokeControl.Invoke();
            return this;
        }

        public MenuItem Expand()
        {
            this.expandCollapseControl.Expand();
            return this;
        }

        public MenuItem Collapse()
        {
            this.expandCollapseControl.Collapse();
            return this;
        }
    }
}
