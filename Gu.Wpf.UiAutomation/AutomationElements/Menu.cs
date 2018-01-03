namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    /// <summary>
    /// Represents a menu or a menubar, which contains menuitems
    /// </summary>
    public class Menu : Control
    {
        public Menu(AutomationElement automationElement, bool isWin32Menu = false)
            : base(automationElement)
        {
            this.IsWin32Menu = isWin32Menu;
        }

        public MenuItems Items
        {
            get
            {
                var childItems = this.FindAllChildren(
                    Conditions.MenuItem,
                    x => new MenuItem(x) { IsWin32Menu = this.IsWin32Menu });
                return new MenuItems(childItems);
            }
        }

        /// <summary>
        /// Flag to indicate if the menu is a Win32 menu because that one needs special handling
        /// </summary>
        public bool IsWin32Menu { get; set; }
    }
}
