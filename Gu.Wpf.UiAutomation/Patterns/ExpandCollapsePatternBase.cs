namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class ExpandCollapsePatternBase<TNativePattern> : PatternBase<TNativePattern>, IExpandCollapsePattern
    {
        private AutomationProperty<ExpandCollapseState> expandCollapseState;

        protected ExpandCollapsePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public IExpandCollapsePatternProperties Properties => this.Automation.PropertyLibrary.ExpandCollapse;

        public AutomationProperty<ExpandCollapseState> ExpandCollapseState => this.GetOrCreate(ref this.expandCollapseState, this.Properties.ExpandCollapseState);

        public abstract void Collapse();

        public abstract void Expand();
    }
}
