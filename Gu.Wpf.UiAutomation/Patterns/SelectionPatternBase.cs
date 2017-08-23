namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class SelectionPatternBase<TNativePattern> : PatternBase<TNativePattern>, ISelectionPattern
    {
        private AutomationProperty<bool> canSelectMultiple;
        private AutomationProperty<bool> isSelectionRequired;
        private AutomationProperty<AutomationElement[]> selection;

        protected SelectionPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public ISelectionPatternProperties Properties => this.Automation.PropertyLibrary.Selection;

        /// <inheritdoc/>
        public ISelectionPatternEvents Events => this.Automation.EventLibrary.Selection;

        /// <inheritdoc/>
        public AutomationProperty<bool> CanSelectMultiple => this.GetOrCreate(ref this.canSelectMultiple, this.Properties.CanSelectMultiple);

        /// <inheritdoc/>
        public AutomationProperty<bool> IsSelectionRequired => this.GetOrCreate(ref this.isSelectionRequired, this.Properties.IsSelectionRequired);

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement[]> Selection => this.GetOrCreate(ref this.selection, this.Properties.Selection);
    }
}
