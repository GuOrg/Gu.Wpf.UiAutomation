namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class MessageBox : UiElement
    {
        public MessageBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public static string ClassNameString { get; } = "#32770";

        public void Close()
        {
            var windowPattern = this.Patterns.Window.PatternOrDefault;
            if (windowPattern != null)
            {
                windowPattern.Close();
                return;
            }

            throw new MethodNotSupportedException("Close is not supported");
        }
    }
}