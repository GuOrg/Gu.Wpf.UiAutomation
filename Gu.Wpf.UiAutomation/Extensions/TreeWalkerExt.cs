namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public static class TreeWalkerExt
    {
        public static IEnumerable<AutomationElement> Ancestors(this TreeWalker walker, AutomationElement element)
        {
            var parent = walker.GetParent(element);
            while (parent != null)
            {
                yield return parent;
                parent = walker.GetParent(parent);
            }
        }

        public static IEnumerable<AutomationElement> Children(this TreeWalker walker, AutomationElement element)
        {
            var child = walker.GetFirstChild(element);
            while (child != null)
            {
                yield return child;
                child = walker.GetNextSibling(child);
            }
        }

        public static IEnumerable<AutomationElement> Decendants(this TreeWalker walker, AutomationElement element)
        {
            foreach (var child in Children(walker, element))
            {
                yield return child;
                foreach (var decendant in Decendants(walker, child))
                {
                    yield return decendant;
                }
            }
        }
    }
}