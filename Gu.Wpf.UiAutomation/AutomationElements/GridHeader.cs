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
                if (this.AutomationElement.TryFindSingleChild(Conditions.TextBlock, out var child))
                {
                    return child.Name();
                }

                if (this.AutomationElement.TryFindSingleChild(Conditions.Label, out child))
                {
                    return child.Name();
                }

                return this.Name;
            }
        }
    }
}
