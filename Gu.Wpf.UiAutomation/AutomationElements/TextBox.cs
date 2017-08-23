namespace Gu.Wpf.UiAutomation
{
    using System.Linq;
    using Gu.Wpf.UiAutomation.Exceptions;
    using Gu.Wpf.UiAutomation.Input;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class TextBox : AutomationElement
    {
        public TextBox(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text
        {
            get
            {
                if (this.Properties.IsPassword)
                {
                    throw new MethodNotSupportedException($"Text from element '{this.ToString()}' cannot be retrieved because it is set as password.");
                }

                var valuePattern = this.Patterns.Value.PatternOrDefault;
                if (valuePattern != null)
                {
                    return valuePattern.Value;
                }

                var textPattern = this.Patterns.Text.PatternOrDefault;
                if (textPattern != null)
                {
                    return textPattern.DocumentRange.GetText(int.MaxValue);
                }

                throw new MethodNotSupportedException($"AutomationElement '{this.ToString()}' supports neither ValuePattern or TextPattern");
            }

            set
            {
                var valuePattern = this.Patterns.Value.PatternOrDefault;
                if (valuePattern != null)
                {
                    valuePattern.SetValue(value);
                }
                else
                {
                    this.Enter(value);
                }
            }
        }

        public void Enter(string value)
        {
            this.Focus();
            var valuePattern = this.Patterns.Value.PatternOrDefault;
            valuePattern?.SetValue(string.Empty);
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            var lines = value.Replace("\r\n", "\n").Split('\n');
            Keyboard.Type(lines[0]);
            foreach (var line in lines.Skip(1))
            {
                Keyboard.Type(VirtualKeyShort.RETURN);
                Keyboard.Type(line);
            }

            Helpers.WaitUntilInputIsProcessed();
        }
    }
}
