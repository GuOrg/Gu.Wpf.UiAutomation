namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public static class Desktop
    {
        public static Window Instance => new(AutomationElement);

        public static AutomationElement AutomationElement => AutomationElement.RootElement;
    }
}
