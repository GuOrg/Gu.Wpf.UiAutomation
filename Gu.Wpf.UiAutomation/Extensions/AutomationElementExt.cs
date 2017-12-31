namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static IEnumerable<AutomationElement> Children(this AutomationElement element)
        {
            return TreeWalker.RawViewWalker.Children(element);
        }
    }
}
