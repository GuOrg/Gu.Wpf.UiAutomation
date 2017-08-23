namespace Gu.Wpf.UiAutomation
{
    using System.Linq;

    /// <summary>
    /// Represents a menu or a menubar, which contains menuitems
    /// </summary>
    public class Menu : AutomationElement
    {
        public Menu(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public MenuItems MenuItems
        {
            get
            {
                var childItems = this.FindAllChildren(cf => cf.ByControlType(ControlType.MenuItem))
                    .Select(e =>
                    {
                        var mi = e.AsMenuItem();
                        mi.IsWin32Menu = this.IsWin32Menu;
                        return mi;
                    });
                return new MenuItems(childItems);
            }
        }

        /// <summary>
        /// Flag to indicate if the menu is a Win32 menu because that one needs special handling
        /// </summary>
        public bool IsWin32Menu { get; set; }
    }
}
