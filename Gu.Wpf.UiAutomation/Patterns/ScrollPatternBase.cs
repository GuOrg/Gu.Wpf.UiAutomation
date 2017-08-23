namespace Gu.Wpf.UiAutomation
{
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

        /// <inheritdoc/>
        public IScrollPatternProperties Properties => this.Automation.PropertyLibrary.Scroll;

        /// <inheritdoc/>
        public AutomationProperty<bool> HorizontallyScrollable => this.GetOrCreate(ref this.horizontallyScrollable, this.Properties.HorizontallyScrollable);

        /// <inheritdoc/>
        public AutomationProperty<double> HorizontalScrollPercent => this.GetOrCreate(ref this.horizontalScrollPercent, this.Properties.HorizontalScrollPercent);

        /// <inheritdoc/>
        public AutomationProperty<double> HorizontalViewSize => this.GetOrCreate(ref this.horizontalViewSize, this.Properties.HorizontalViewSize);

        /// <inheritdoc/>
        public AutomationProperty<bool> VerticallyScrollable => this.GetOrCreate(ref this.verticallyScrollable, this.Properties.VerticallyScrollable);

        /// <inheritdoc/>
        public AutomationProperty<double> VerticalScrollPercent => this.GetOrCreate(ref this.verticalScrollPercent, this.Properties.VerticalScrollPercent);

        /// <inheritdoc/>
        public AutomationProperty<double> VerticalViewSize => this.GetOrCreate(ref this.verticalViewSize, this.Properties.VerticalViewSize);

        /// <inheritdoc/>
        public abstract void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount);

        /// <inheritdoc/>
        public abstract void SetScrollPercent(double horizontalPercent, double verticalPercent);
    }
}