using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;

namespace Gu.Wpf.UiAutomation
{
    public interface ITreeWalker
    {
        AutomationElement GetParent(AutomationElement element);

        AutomationElement GetFirstChild(AutomationElement element);

        AutomationElement GetLastChild(AutomationElement element);

        AutomationElement GetNextSibling(AutomationElement element);

        AutomationElement GetPreviousSibling(AutomationElement element);
    }
}
