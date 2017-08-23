namespace Gu.Wpf.UiAutomation.AutomationElements
{
    using System.Linq;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;

    /// <summary>
    /// Header element for grids and tables.
    /// </summary>
    public class GridHeader : AutomationElement
    {
        public GridHeader(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public GridHeaderItem[] Columns
        {
            get
            {
                var headerItems = this.FindAllChildren(cf => cf.ByControlType(ControlType.HeaderItem));
                return Enumerable.Select(headerItems, x => x.AsGridHeaderItem()).ToArray();
            }
        }
    }
}