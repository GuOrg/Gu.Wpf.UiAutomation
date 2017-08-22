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
        private AutomationProperty<string> _dropTargetEffect;
        private AutomationProperty<string[]> _dropTargetEffects;

        protected DropTargetPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public IDropTargetPatternProperties Properties => Automation.PropertyLibrary.DropTarget;
        public IDropTargetPatternEvents Events => Automation.EventLibrary.DropTarget;

        public AutomationProperty<string> DropTargetEffect => GetOrCreate(ref _dropTargetEffect, Properties.DropTargetEffect);
        public AutomationProperty<string[]> DropTargetEffects => GetOrCreate(ref _dropTargetEffects, Properties.DropTargetEffects);
    }
}
