namespace Gu.Wpf.UiAutomation
{
    public abstract class ExpandCollapsePatternBase<TNativePattern> : PatternBase<TNativePattern>, IExpandCollapsePattern
        where TNativePattern : class
    {
        private AutomationProperty<ExpandCollapseState> expandCollapseState;

        protected ExpandCollapsePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public IExpandCollapsePatternProperties Properties => this.Automation.PropertyLibrary.ExpandCollapse;

        /// <inheritdoc/>
        public AutomationProperty<ExpandCollapseState> ExpandCollapseState => this.GetOrCreate(ref this.expandCollapseState, this.Properties.ExpandCollapseState);

        /// <inheritdoc/>
        public abstract void Collapse();

        /// <inheritdoc/>
        public abstract void Expand();
    }
}
