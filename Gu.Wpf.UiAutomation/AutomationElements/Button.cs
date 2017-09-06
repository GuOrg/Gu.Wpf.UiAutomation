namespace Gu.Wpf.UiAutomation
{
    public class Button : InvokeAutomationElement
    {
        public Button(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

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

        public AutomationElement Content => this.FindFirstChild();
    }
}
