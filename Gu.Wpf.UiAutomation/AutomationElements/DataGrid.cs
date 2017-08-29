namespace Gu.Wpf.UiAutomation
{
    public class DataGrid : GridView
    {
        public DataGrid(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public GridCell this[int row, int col] => this.Rows[row].Cells[col];
    }
}