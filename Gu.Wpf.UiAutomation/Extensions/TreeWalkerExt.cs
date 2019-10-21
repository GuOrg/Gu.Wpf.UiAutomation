namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public static class TreeWalkerExt
    {
        public static IEnumerable<AutomationElement> Ancestors(this TreeWalker walker, AutomationElement element)
        {
            if (walker is null)
            {
                throw new System.ArgumentNullException(nameof(walker));
            }

            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            var parent = walker.GetParent(element);
            while (parent != null)
            {
                yield return parent;
                parent = walker.GetParent(parent);
            }
        }

        public static IEnumerable<AutomationElement> Children(this TreeWalker walker, AutomationElement element)
        {
            if (walker is null)
            {
                throw new System.ArgumentNullException(nameof(walker));
            }

            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            var child = walker.GetFirstChild(element);
            while (child != null)
            {
                yield return child;
                child = walker.GetNextSibling(child);
            }
        }

        public static IEnumerable<AutomationElement> Descendants(this TreeWalker walker, AutomationElement element)
        {
            if (walker is null)
            {
                throw new System.ArgumentNullException(nameof(walker));
            }

            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            foreach (var child in Children(walker, element))
            {
                yield return child;
                foreach (var descendants in Descendants(walker, child))
                {
                    yield return descendants;
                }
            }
        }
    }
}
