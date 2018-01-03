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

        public IReadOnlyList<GridViewColumnHeader> Headers => this.FindAllChildren(Conditions.GridViewColumnHeader, x => new GridViewColumnHeader(x));
    }
}