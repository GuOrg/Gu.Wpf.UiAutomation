namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class ScrollPatternBase<TNativePattern> : PatternBase<TNativePattern>, IScrollPattern
    {
        private AutomationProperty<bool> horizontallyScrollable;
        private AutomationProperty<double> horizontalScrollPercent;
        private AutomationProperty<double> horizontalViewSize;
        private AutomationProperty<bool> verticallyScrollable;
        private AutomationProperty<double> verticalScrollPercent;
        private AutomationProperty<double> verticalViewSize;

        protected ScrollPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public IScrollPatternProperties Properties => this.Automation.PropertyLibrary.Scroll;

        public AutomationProperty<bool> HorizontallyScrollable => this.GetOrCreate(ref this.horizontallyScrollable, this.Properties.HorizontallyScrollable);

        public AutomationProperty<double> HorizontalScrollPercent => this.GetOrCreate(ref this.horizontalScrollPercent, this.Properties.HorizontalScrollPercent);

        public AutomationProperty<double> HorizontalViewSize => this.GetOrCreate(ref this.horizontalViewSize, this.Properties.HorizontalViewSize);

        public AutomationProperty<bool> VerticallyScrollable => this.GetOrCreate(ref this.verticallyScrollable, this.Properties.VerticallyScrollable);

        public AutomationProperty<double> VerticalScrollPercent => this.GetOrCreate(ref this.verticalScrollPercent, this.Properties.VerticalScrollPercent);

        public AutomationProperty<double> VerticalViewSize => this.GetOrCreate(ref this.verticalViewSize, this.Properties.VerticalViewSize);

        public abstract void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount);

        public abstract void SetScrollPercent(double horizontalPercent, double verticalPercent);
    }
}