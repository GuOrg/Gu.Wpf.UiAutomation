namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class Selector : Control
    {
        public Selector(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public SelectionPattern SelectionPattern => this.AutomationElement.SelectionPattern();

        public bool CanSelectMultiple => (bool)this.AutomationElement.GetCurrentPropertyValue(SelectionPattern.CanSelectMultipleProperty);
    }
}
