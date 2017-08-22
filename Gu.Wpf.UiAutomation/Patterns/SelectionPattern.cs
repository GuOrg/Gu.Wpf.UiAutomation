namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ISelectionPattern : IPattern
    {
        ISelectionPatternProperties Properties { get; }
        ISelectionPatternEvents Events { get; }

        AutomationProperty<bool> CanSelectMultiple { get; }
        AutomationProperty<bool> IsSelectionRequired { get; }
        AutomationProperty<AutomationElement[]> Selection { get; }
    }

    public interface ISelectionPatternProperties
    {
        PropertyId CanSelectMultiple { get; }
        PropertyId IsSelectionRequired { get; }
        PropertyId Selection { get; }
    }

    public interface ISelectionPatternEvents
    {
        EventId InvalidatedEvent { get; }
    }

    public abstract class SelectionPatternBase<TNativePattern> : PatternBase<TNativePattern>, ISelectionPattern
    {
        private AutomationProperty<bool> canSelectMultiple;
        private AutomationProperty<bool> isSelectionRequired;
        private AutomationProperty<AutomationElement[]> selection;

        protected SelectionPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public ISelectionPatternProperties Properties => this.Automation.PropertyLibrary.Selection;
        public ISelectionPatternEvents Events => this.Automation.EventLibrary.Selection;

        public AutomationProperty<bool> CanSelectMultiple => this.GetOrCreate(ref this.canSelectMultiple, this.Properties.CanSelectMultiple);
        public AutomationProperty<bool> IsSelectionRequired => this.GetOrCreate(ref this.isSelectionRequired, this.Properties.IsSelectionRequired);
        public AutomationProperty<AutomationElement[]> Selection => this.GetOrCreate(ref this.selection, this.Properties.Selection);
    }
}
