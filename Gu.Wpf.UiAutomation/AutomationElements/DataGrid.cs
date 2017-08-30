namespace Gu.Wpf.UiAutomation
{
    using System.Linq;

    public class DataGrid : GridView
    {
        public DataGrid(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public bool IsReadOnly => this.FindAllChildren(cf => cf.ByControlType(ControlType.DataItem).Or(cf.ByControlType(ControlType.ListItem)))
                                      .LastOrDefault()
                                      ?.Properties.IsOffscreen ??
                                  true;

        public GridCell this[int row, int col] => this.Rows[row].Cells[col];
    }
}