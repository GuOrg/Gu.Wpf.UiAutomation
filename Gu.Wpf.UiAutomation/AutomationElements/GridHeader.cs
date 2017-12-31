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

        public string Text
        {
            get
            {
                if (this.AutomationElement.TryFindSingleChild(Condition.TextBlock, out var child))
                {
                    return child.Name();
                }

                return this.Name;
            }
        }
    }
}