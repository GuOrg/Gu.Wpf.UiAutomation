namespace Gu.Wpf.UiAutomation
{
    using System.Linq;

    public class DataGrid : GridView
    {
        public DataGrid(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public bool IsReadOnly
        {
            get
            {
                var firstRow = new GridRow(this.GridPattern.GetItem(0, 0).Parent.BasicAutomationElement);
                return firstRow.Cells.Where(x => x.IsKeyboardFocusable)
                                     .All(x => x.IsReadOnly);
            }
        }

        public GridCell this[int row, int col] => new GridCell(this.GridPattern.GetItem(row, col).BasicAutomationElement);

        public GridRow this[int row] => new GridRow(this.GridPattern.GetItem(row, 0).Parent.BasicAutomationElement);
    }
}