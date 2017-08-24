namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
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

        public IReadOnlyList<GridHeaderItem> Columns
        {
            get
            {
                return this.FindAllChildren(cf => cf.ByControlType(ControlType.HeaderItem))
                           .Select(x => x.AsGridHeaderItem())
                           .ToArray();
            }
        }
    }
}