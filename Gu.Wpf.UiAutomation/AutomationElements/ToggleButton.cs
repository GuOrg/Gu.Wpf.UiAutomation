namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Globalization;
    using System.Windows.Automation;

    public class ToggleButton : Control
    {
        public ToggleButton(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public TogglePattern TogglePattern => this.AutomationElement.TogglePattern();

        public string Text => this.AutomationElement.Text();

        public bool? IsChecked
        {
            get
            {
                switch (this.State)
                {
                    case ToggleState.Off:
                        return false;
                    case ToggleState.On:
                        return true;
                    case ToggleState.Indeterminate:
                        return null;
                    default:
                        throw new InvalidOperationException($"Not handling state {this.State}");
                }
            }

            set
            {
                if (this.IsChecked == value)
                {
                    return;
                }

                this.TogglePattern.Toggle();
                if (this.IsChecked == value)
                {
                    return;
                }

                this.TogglePattern.Toggle();
                if (this.IsChecked == value)
                {
                    return;
                }

                throw new UiAutomationException($"Setting ToggleButton {this.Name}.IsChecked to {value?.ToString(CultureInfo.InvariantCulture) ?? "null"} failed.");
            }
        }

        private ToggleState State => this.TogglePattern.Current.ToggleState;
    }
}
