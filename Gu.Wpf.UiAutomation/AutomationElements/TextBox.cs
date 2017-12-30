namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Windows.Automation;

    public class TextBox : Control
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

        /// <summary>
        /// Simulate typing in text. This is slower than setting Text but raises more events.
        /// </summary>
        public void Enter(string value)
        {
            if (this.IsOffscreen)
            {
                throw new InvalidOperationException("Cannot click when off screen.");
            }

            if (!this.IsKeyboardFocusable)
            {
                throw new InvalidOperationException("Cannot enter when not KeyboardFocusable.");
            }

            if (!this.HasKeyboardFocus)
            {
                this.Focus();
            }

            if (this.AutomationElement.TryGetValuePattern(out var valuePattern))
            {
                valuePattern?.SetValue(string.Empty);
            }

            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            var lines = value.Replace("\r\n", "\n").Split('\n');
            Keyboard.Type(lines[0]);
            foreach (var line in lines.Skip(1))
            {
                Keyboard.Type(Key.RETURN);
                Keyboard.Type(line);
            }

            // give some time to process input.
            var stopTime = DateTime.Now.AddSeconds(1);
            while (DateTime.Now < stopTime)
            {
                if (this.Text == value)
                {
                    return;
                }

                if (!Thread.Yield())
                {
                    Thread.Sleep(10);
                }
            }
        }
    }
}
