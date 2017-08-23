namespace Gu.Wpf.UiAutomation.AutomationElements
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a list of <see cref="MenuItem"/>s.
    /// </summary>
    public class MenuItems : List<MenuItem>
    {
        public MenuItems(IEnumerable<MenuItem> collection)
            : base(collection)
        {
        }

        public int Length => this.Count;

        public MenuItem this[string text]
        {
            get { return Enumerable.FirstOrDefault(this, x => x.Text.Equals(text)); }
        }
    }
}