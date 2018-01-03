namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    /// <summary>
    /// Header item for grids and tables.
    /// </summary>
    public abstract class GridHeader : Control
    {
        protected GridHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Text => this.AutomationElement.Text();
    }
}
