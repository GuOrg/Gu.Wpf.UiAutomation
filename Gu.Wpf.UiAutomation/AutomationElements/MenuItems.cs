namespace Gu.Wpf.UiAutomation
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a list of <see cref="MenuItem"/>s.
    /// </summary>
    public class MenuItems : IReadOnlyList<MenuItem>
    {
        private readonly IReadOnlyList<MenuItem> items;

        public MenuItems(IReadOnlyList<MenuItem> items)
        {
            this.items = items;
        }

        public int Count => this.items.Count;

        public MenuItem this[int index] => this.items[index];

        public MenuItem this[string text] => this.FirstOrDefault(x => x.Text.Equals(text));

        public IEnumerator<MenuItem> GetEnumerator() => this.items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.items).GetEnumerator();
    }
}
