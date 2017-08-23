namespace Gu.Wpf.UiAutomation
{
    /// <summary>
    /// Cell element for grids and tables.
    /// </summary>
    public class GridCell : AutomationElement
    {
        public GridCell(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public Grid ContainingGrid => this.GridItemPattern.ContainingGrid.Value.AsGrid();

        public GridRow ContainingRow
        {
            get
            {
                // Get the parent of the cell (which should be the row)
                var rowElement = this.Automation.TreeWalkerFactory.GetControlViewWalker().GetParent(this);
                return rowElement?.AsGridRow();
            }
        }

        public string Value => this.Properties.Name.Value;

        protected IGridItemPattern GridItemPattern => this.Patterns.GridItem.Pattern;

        protected ITableItemPattern TableItemPattern => this.Patterns.TableItem.Pattern;
    }
}