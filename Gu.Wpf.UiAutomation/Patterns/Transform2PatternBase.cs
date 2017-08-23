namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;

    public abstract class Transform2PatternBase<TNativePattern> : TransformPatternBase<TNativePattern>, ITransform2Pattern
    {
        private AutomationProperty<bool> canZoom;
        private AutomationProperty<double> zoomLevel;
        private AutomationProperty<double> zoomMaximum;
        private AutomationProperty<double> zoomMinimum;

        protected Transform2PatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        ITransform2PatternProperties ITransform2Pattern.Properties => this.Automation.PropertyLibrary.Transform2;

        public AutomationProperty<bool> CanZoom => this.GetOrCreate(ref this.canZoom, ((ITransform2Pattern)this).Properties.CanZoom);

        public AutomationProperty<double> ZoomLevel => this.GetOrCreate(ref this.zoomLevel, ((ITransform2Pattern)this).Properties.ZoomLevel);

        public AutomationProperty<double> ZoomMaximum => this.GetOrCreate(ref this.zoomMaximum, ((ITransform2Pattern)this).Properties.ZoomMaximum);

        public AutomationProperty<double> ZoomMinimum => this.GetOrCreate(ref this.zoomMinimum, ((ITransform2Pattern)this).Properties.ZoomMinimum);

        public abstract void Zoom(double zoom);

        public abstract void ZoomByUnit(ZoomUnit zoomUnit);
    }
}
