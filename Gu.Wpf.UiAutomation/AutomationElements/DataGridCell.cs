namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class DataGridCell : GridCell
    {
        public DataGridCell(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public DataGrid ContainingDataGrid => (DataGrid)FromAutomationElement(this.GridItemPattern.Current.ContainingGrid);
    }
}
