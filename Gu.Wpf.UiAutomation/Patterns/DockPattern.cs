namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IDockPattern : IPattern
    {
        IDockPatternProperties Properties { get; }

        AutomationProperty<DockPosition> DockPosition { get; }

        void SetDockPosition(DockPosition dockPos);
    }

    public interface IDockPatternProperties
    {
        PropertyId DockPosition { get; }
    }

    public abstract class DockPatternBase<TNativePattern> : PatternBase<TNativePattern>, IDockPattern
    {
        private AutomationProperty<DockPosition> dockPosition;

        protected DockPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public IDockPatternProperties Properties => this.Automation.PropertyLibrary.Dock;

        public AutomationProperty<DockPosition> DockPosition => this.GetOrCreate(ref this.dockPosition, this.Properties.DockPosition);

        public abstract void SetDockPosition(DockPosition dockPos);
    }
}
