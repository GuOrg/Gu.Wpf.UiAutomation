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

        public string Text
        {
            get
            {
                var children = this.FindAllChildren();
                if (children.Count == 1 &&
                    children[0].ControlType == ControlType.Text)
                {
                    return children[0].Properties.Name.Value;
                }

                return this.Properties.Name.Value;
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

                throw new UiAutomationException($"Setting {this} .IsChecked to {value?.ToString() ?? "null failed."}");
            }
        }

        private ToggleState State => this.TogglePattern.ToggleState.Value;
    }
}
