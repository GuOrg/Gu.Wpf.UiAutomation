namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IDragPattern : IPattern
    {
        IDragPatternProperties Properties { get; }
        IDragPatternEvents Events { get; }

        AutomationProperty<string> DropEffect { get; }
        AutomationProperty<string[]> DropEffects { get; }
        AutomationProperty<bool> IsGrabbed { get; }
        AutomationProperty<AutomationElement[]> GrabbedItems { get; }
    }

    public interface IDragPatternProperties
    {
        PropertyId DropEffect { get; }
        PropertyId DropEffects { get; }
        PropertyId IsGrabbed { get; }
        PropertyId GrabbedItems { get; }
    }

    public interface IDragPatternEvents
    {
        EventId DragCancelEvent { get; }
        EventId DragCompleteEvent { get; }
        EventId DragStartEvent { get; }
    }

    public abstract class DragPatternBase<TNativePattern> : PatternBase<TNativePattern>, IDragPattern
    {
        private AutomationProperty<string> dropEffect;
        private AutomationProperty<string[]> dropEffects;
        private AutomationProperty<bool> isGrabbed;
        private AutomationProperty<AutomationElement[]> grabbedItems;

        protected DragPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public IDragPatternProperties Properties => this.Automation.PropertyLibrary.Drag;
        public IDragPatternEvents Events => this.Automation.EventLibrary.Drag;

        public AutomationProperty<string> DropEffect => this.GetOrCreate(ref this.dropEffect, this.Properties.DropEffect);
        public AutomationProperty<string[]> DropEffects => this.GetOrCreate(ref this.dropEffects, this.Properties.DropEffects);
        public AutomationProperty<bool> IsGrabbed => this.GetOrCreate(ref this.isGrabbed, this.Properties.IsGrabbed);
        public AutomationProperty<AutomationElement[]> GrabbedItems => this.GetOrCreate(ref this.grabbedItems, this.Properties.GrabbedItems);
    }
}
