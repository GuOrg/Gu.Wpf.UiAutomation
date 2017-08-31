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

                return firstRow.Cells.Where(x => x.IsKeyboardFocusable)
                                     .All(x => x.IsReadOnly);
            }
        }

        public GridCell this[int row, int col] => new GridCell(this.GridPattern.GetItem(row, col).BasicAutomationElement);
    }
}