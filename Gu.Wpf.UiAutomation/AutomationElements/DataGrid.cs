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
                var firstRow = this.Rows.FirstOrDefault();
                if (firstRow == null)
                {
                    return true;
                }

                return firstRow.Cells.All(x => x.IsReadOnly);
            }
        }

        public GridCell this[int row, int col]
        {
            get
            {
                var gridRow = this.Rows[row];
                if (gridRow.IsOffscreen)
                {
                    gridRow.Patterns.ScrollItem.Pattern.ScrollIntoView();
                }

                return gridRow.Cells[col];
            }
        }
    }
}