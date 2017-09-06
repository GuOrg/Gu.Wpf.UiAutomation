namespace Gu.Wpf.UiAutomation
{
    public interface IExpandCollapsePattern : IPattern
    {
        IExpandCollapsePatternProperties Properties { get; }

        AutomationProperty<ExpandCollapseState> ExpandCollapseState { get; }

        void Collapse();

        void Expand();
    }
}