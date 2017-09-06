namespace Gu.Wpf.UiAutomation
{
    using System;

    public class ToggleButton : Control
    {
        public ToggleButton(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public ITogglePattern TogglePattern => this.Patterns.Toggle.Pattern;

        public string Text => this.Properties.Name.Value;

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

                throw new UiAutomationException($"Setting {this} .IsChecked to {value?.ToString() ?? "null failed."}");
            }
        }

        private ToggleState State => this.TogglePattern.ToggleState.Value;
    }
}
