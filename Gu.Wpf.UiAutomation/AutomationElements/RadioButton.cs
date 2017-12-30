namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class RadioButton : Control
    {
        public RadioButton(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Flag to get/set the selection of this element.
        /// </summary>
        public bool IsChecked
        {
            get => this.SelectionItemPattern.Current.IsSelected;
            set
            {
                if (this.IsChecked == value)
                {
                    return;
                }

                if (value && !this.IsChecked)
                {
                    this.ExecuteInPattern(this.SelectionItemPattern, throwIfNotSupported: true, action: pattern => pattern.Select());
                }

                if (this.IsChecked != value)
                {
                    throw new UiAutomationException($"Setting {this}.IsChecked to {value} failed.");
                }
            }
        }

        protected SelectionItemPattern SelectionItemPattern => this.AutomationElement.SelectionItemPattern();
    }
}
