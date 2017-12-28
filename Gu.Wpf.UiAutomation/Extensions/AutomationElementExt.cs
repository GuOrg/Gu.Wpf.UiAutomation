namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        [Obsolete("This can be removed.")]
        public static UiElement GetDesktop(this AutomationElement element)
        {
            return new UiElement(AutomationElement.RootElement);
        }
    }
}
