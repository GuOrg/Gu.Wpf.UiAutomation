namespace Gu.Wpf.UiAutomation
{
    using System.Linq;
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
                if (this.FindAllChildren()
                        .Where(c => c.ControlType != ControlType.Thumb)
                        .TryGetSingle(out var child) &&
                    child.ControlType == ControlType.Text)
                {
                    return child.AsTextBlock().Text;
                }

                return this.Name;
            }
        }
    }
}