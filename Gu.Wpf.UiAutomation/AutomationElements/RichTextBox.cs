namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class RichTextBox : Control
    {
        public RichTextBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public ScrollViewer ScrollViewer => this.FindScrollViewer();

        public ScrollPattern ScrollPattern => this.AutomationElement.ScrollPattern();

        public TextPattern TextPattern => this.AutomationElement.TextPattern();

        public bool IsReadOnly
        {
            get
            {
                if (this.AutomationElement.TryGetValuePattern(out var valuePattern))
                {
                    return valuePattern.Current.IsReadOnly;
                }

                return true;
            }
        }
    }
}
