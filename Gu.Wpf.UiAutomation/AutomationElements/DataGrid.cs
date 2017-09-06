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

        public override int RowCount
        {
            get
            {
                var gridPattern = this.GridPattern;
                if (!this.GridPattern.RowCount.TryGetValue(out var rowCount))
                {
                    return 0;
                }

                if (rowCount == 0)
                {
                    return 0;
                }

                var cell = new GridCell(gridPattern.GetItem(rowCount - 1, 0).BasicAutomationElement);
                if (cell.IsNewItemPlaceholder &&
                    cell.IsReadOnly)
                {
                    return rowCount - 1;
                }

                return rowCount;
            }
        }
    }
}