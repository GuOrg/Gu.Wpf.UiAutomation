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
                if (this.FindAllChildren()
                        .Where(c => c.ControlType != ControlType.Thumb)
                        .TryGetSingle(out var child) &&
                    child.ControlType == ControlType.Text)
                {
                    return child.AsTextBlock().Text;
                }

                return this.Properties.Name.Value;
            }
        }
    }
}