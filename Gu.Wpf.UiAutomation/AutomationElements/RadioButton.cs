namespace Gu.Wpf.UiAutomation
{
    public class RadioButton : SelectionItemAutomationElement
    {
        public RadioButton(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public void Toggle()
        {
            this.IsChecked = !this.IsChecked;
        }
    }
}
