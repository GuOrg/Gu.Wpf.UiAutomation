namespace Gu.Wpf.UiAutomation
{
    using System.Linq;

    /// <summary>
    /// Header item for grids and tables.
    /// </summary>
    public abstract class HeaderBase : Control
    {
        public HeaderBase(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text
        {
            get
            {
                var children = this.FindAllChildren()
                                   .Where(c => c.ControlType != ControlType.Thumb)
                                   .ToArray();
                if (children.Length == 1 &&
                    children[0].ControlType == ControlType.Text)
                {
                    return children[0].Properties.Name.Value;
                }

                return this.Properties.Name.Value;
            }
        }
    }
}