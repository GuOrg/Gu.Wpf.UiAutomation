namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class TextBlock : UiElement
    {
        public TextBlock(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Text => this.Name;
    }
}