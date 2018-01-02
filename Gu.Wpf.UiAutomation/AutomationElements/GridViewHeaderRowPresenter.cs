namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public class GridViewHeaderRowPresenter : Control
    {
        public GridViewHeaderRowPresenter(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public IReadOnlyList<GridViewColumnHeader> Headers => this.FindAllChildren(Condition.GridViewColumnHeader, x => new GridViewColumnHeader(x));
    }
}