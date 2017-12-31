namespace Gu.Wpf.UiAutomation
{
    using System.Linq;
    using System.Windows.Automation;

    public class DataGrid : GridView
    {
        public DataGrid(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public bool IsReadOnly
        {
            get
            {
                var firstRow = new GridRow(this.GridPattern.GetItem(0, 0).Parent());
                return firstRow.Cells.Where(x => x.IsKeyboardFocusable)
                                     .All(x => x.IsReadOnly);
            }
        }

        public override int RowCount
        {
            get
            {
                var rowCount = this.GridPattern.Current.RowCount;
                if (rowCount == 0)
                {
                    return 0;
                }

                var cell = new GridCell(this.GridPattern.GetItem(rowCount - 1, 0));
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