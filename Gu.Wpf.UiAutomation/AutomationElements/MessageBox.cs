namespace Gu.Wpf.UiAutomation
{
    public class MessageBox : AutomationElement
    {
        public MessageBox(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
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