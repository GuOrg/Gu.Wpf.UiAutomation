namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class SelectionItemPatternBase<TNativePattern> : PatternBase<TNativePattern>, ISelectionItemPattern
    {
        private AutomationProperty<bool> isSelected;
        private AutomationProperty<AutomationElement> selectionContainer;

        protected SelectionItemPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public ISelectionItemPatternProperties Properties => this.Automation.PropertyLibrary.SelectionItem;

        /// <inheritdoc/>
        public ISelectionItemPatternEvents Events => this.Automation.EventLibrary.SelectionItem;

        /// <inheritdoc/>
        public AutomationProperty<bool> IsSelected => this.GetOrCreate(ref this.isSelected, this.Properties.IsSelected);

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement> SelectionContainer => this.GetOrCreate(ref this.selectionContainer, this.Properties.SelectionContainer);

        /// <inheritdoc/>
        public abstract void AddToSelection();

        /// <inheritdoc/>
        public abstract void RemoveFromSelection();

        /// <inheritdoc/>
        public abstract void Select();
    }
}
