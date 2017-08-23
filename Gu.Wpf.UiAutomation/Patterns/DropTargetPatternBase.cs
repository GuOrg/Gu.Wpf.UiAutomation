namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

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
