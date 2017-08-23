namespace Gu.Wpf.UiAutomation
{
    /// <summary>
    /// Header item for grids and tables.
    /// </summary>
    public class GridHeaderItem : AutomationElement
    {
        public GridHeaderItem(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}