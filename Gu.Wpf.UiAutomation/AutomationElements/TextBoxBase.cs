namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Windows.Automation;

    public class TextBoxBase : Control
    {
        public TextBoxBase(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public ScrollViewer ScrollViewer => this.FindScrollViewer();

        public ScrollPattern ScrollPattern => this.AutomationElement.ScrollPattern();

        public TextPattern TextPattern => this.AutomationElement.TextPattern();

        public ValuePattern ValuePattern => this.AutomationElement.ValuePattern();

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

        public static TextBoxBase Create(AutomationElement automationElement)
        {
            if (automationElement is null)
            {
                throw new ArgumentNullException(nameof(automationElement));
            }

            if (Conditions.IsMatch(automationElement, Conditions.TextBox))
            {
                return new TextBox(automationElement);
            }

            if (Conditions.IsMatch(automationElement, Conditions.PasswordBox))
            {
                return new PasswordBox(automationElement);
            }

            return new TextBoxBase(automationElement);
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
            if (valuePattern != null)
            {
                var stopTime = DateTime.Now.AddSeconds(1);

                while (DateTime.Now < stopTime)
                {
                    if (valuePattern.Current.Value == value)
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
}
