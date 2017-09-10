namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public interface ITreeWalker
    {
        AutomationElement GetParent(AutomationElement element);

        IReadOnlyList<AutomationElement> GetChildren(AutomationElement element);

        AutomationElement GetFirstChild(AutomationElement element);

        AutomationElement GetLastChild(AutomationElement element);

        AutomationElement GetNextSibling(AutomationElement element);

        AutomationElement GetPreviousSibling(AutomationElement element);
    }
}
