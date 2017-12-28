namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public static class TreeWalkerExt
    {
        public static IEnumerable<AutomationElement> GetChildren(this TreeWalker walker, AutomationElement element)
        {
            var child = walker.GetFirstChild(element);
            while (child != null)
            {
                yield return child;
                child = walker.GetNextSibling(child);
            }
        }
    }
}