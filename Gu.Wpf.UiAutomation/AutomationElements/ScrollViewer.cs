namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ScrollViewer : ContentControl
    {
        public ScrollViewer(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public HorizontalScrollBar HorizontalScrollBar => new HorizontalScrollBar(
            this.AutomationElement.FindFirst(TreeScope.Children, Conditions.ByAutomationId(nameof(this.HorizontalScrollBar))));

        public HorizontalScrollBar VerticalScrollBar => new HorizontalScrollBar(
            this.AutomationElement.FindFirst(TreeScope.Children, Conditions.ByAutomationId(nameof(this.VerticalScrollBar))));

        public ScrollPattern ScrollPattern => this.AutomationElement.ScrollPattern();
    }
}
