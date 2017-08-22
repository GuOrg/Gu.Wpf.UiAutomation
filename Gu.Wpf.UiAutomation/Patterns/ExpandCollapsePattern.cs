namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IExpandCollapsePattern : IPattern
    {
        IExpandCollapsePatternProperties Properties { get; }

        AutomationProperty<ExpandCollapseState> ExpandCollapseState { get; }

        void Collapse();
        void Expand();
    }

    public interface IExpandCollapsePatternProperties
    {
        PropertyId ExpandCollapseState { get; }
    }

    public abstract class ExpandCollapsePatternBase<TNativePattern> : PatternBase<TNativePattern>, IExpandCollapsePattern
    {
        private AutomationProperty<ExpandCollapseState> _expandCollapseState;

        protected ExpandCollapsePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public IExpandCollapsePatternProperties Properties => Automation.PropertyLibrary.ExpandCollapse;

        public AutomationProperty<ExpandCollapseState> ExpandCollapseState => GetOrCreate(ref _expandCollapseState, Properties.ExpandCollapseState);

        public abstract void Collapse();
        public abstract void Expand();
    }
}
