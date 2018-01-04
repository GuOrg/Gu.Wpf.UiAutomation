namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public class TextBox : TextBoxBase
    {
        public TextBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Text
        {
            get
            {
                if (this.AutomationElement.IsPassword())
                {
                    throw new NotSupportedException($"Text from element '{this}' cannot be retrieved because it is set as password.");
                }

                if (this.AutomationElement.TryGetValuePattern(out var valuePattern))
                {
                    return valuePattern.Current.Value;
                }

                if (this.AutomationElement.TryGetTextPattern(out var textPattern))
                {
                    return textPattern.DocumentRange.GetText(int.MaxValue);
                }

                throw new NotSupportedException($"AutomationElement '{this}' supports neither ValuePattern or TextPattern");
            }

            set
            {
                if (this.AutomationElement.TryGetValuePattern(out var valuePattern))
                {
                    valuePattern.SetValue(value ?? string.Empty);
                }
                else
                {
                    this.Enter(value);
                }
            }
        }
    }
}
