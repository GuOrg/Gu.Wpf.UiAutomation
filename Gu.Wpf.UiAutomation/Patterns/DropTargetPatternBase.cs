namespace Gu.Wpf.UiAutomation
{
    public abstract class DropTargetPatternBase<TNativePattern> : PatternBase<TNativePattern>, IDropTargetPattern
        where TNativePattern : class
    {
        private AutomationProperty<string> dropTargetEffect;
        private AutomationProperty<string[]> dropTargetEffects;

        protected DropTargetPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public IDropTargetPatternProperties Properties => this.Automation.PropertyLibrary.DropTarget;

        /// <inheritdoc/>
        public IDropTargetPatternEvents Events => this.Automation.EventLibrary.DropTarget;

        /// <inheritdoc/>
        public AutomationProperty<string> DropTargetEffect => this.GetOrCreate(ref this.dropTargetEffect, this.Properties.DropTargetEffect);

        /// <inheritdoc/>
        public AutomationProperty<string[]> DropTargetEffects => this.GetOrCreate(ref this.dropTargetEffects, this.Properties.DropTargetEffects);
    }
}
