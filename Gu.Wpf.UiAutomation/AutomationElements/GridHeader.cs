namespace Gu.Wpf.UiAutomation
{
    using System.Linq;

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