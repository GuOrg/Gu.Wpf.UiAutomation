namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public class ToggleButton : Control
    {
        public ToggleButton(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public TogglePattern TogglePattern => this.AutomationElement.TogglePattern();

        public string Text
        {
            get
            {
                var children = this.FindAllChildren();
                if (children.Count == 1 &&
                    children[0].ControlType.Id == ControlType.Text.Id)
                {
                    return children[0].Name;
                }

                return this.Name;
            }
        }

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
                        throw new ArgumentOutOfRangeException();
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

                throw new UiAutomationException($"Setting ToggleButton {this.Name}.IsChecked to {value?.ToString() ?? "null"} failed.");
            }
        }

        private ToggleState State => this.TogglePattern.Current.ToggleState;
    }
}
