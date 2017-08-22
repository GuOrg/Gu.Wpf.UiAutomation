namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IDropTargetPattern : IPattern
    {
        IDropTargetPatternProperties Properties { get; }

        IDropTargetPatternEvents Events { get; }

        AutomationProperty<string> DropTargetEffect { get; }

        AutomationProperty<string[]> DropTargetEffects { get; }
    }

    public interface IDropTargetPatternProperties
    {
        PropertyId DropTargetEffect { get; }

        PropertyId DropTargetEffects { get; }
    }

    public interface IDropTargetPatternEvents
    {
        EventId DragEnterEvent { get; }

        EventId DragLeaveEvent { get; }

        EventId DragCompleteEvent { get; }
    }

    public abstract class DropTargetPatternBase<TNativePattern> : PatternBase<TNativePattern>, IDropTargetPattern
    {
        private AutomationProperty<string> dropTargetEffect;
        private AutomationProperty<string[]> dropTargetEffects;

        protected DropTargetPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public IDropTargetPatternProperties Properties => this.Automation.PropertyLibrary.DropTarget;

        public IDropTargetPatternEvents Events => this.Automation.EventLibrary.DropTarget;

        public AutomationProperty<string> DropTargetEffect => this.GetOrCreate(ref this.dropTargetEffect, this.Properties.DropTargetEffect);

        public AutomationProperty<string[]> DropTargetEffects => this.GetOrCreate(ref this.dropTargetEffects, this.Properties.DropTargetEffects);
    }
}
