namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public class ScrollPatternConstants
    {
        public const double NoScroll = -1.0;
    }

    public interface IScrollPattern : IPattern
    {
        IScrollPatternProperties Properties { get; }

        AutomationProperty<bool> HorizontallyScrollable { get; }
        AutomationProperty<double> HorizontalScrollPercent { get; }
        AutomationProperty<double> HorizontalViewSize { get; }
        AutomationProperty<bool> VerticallyScrollable { get; }
        AutomationProperty<double> VerticalScrollPercent { get; }
        AutomationProperty<double> VerticalViewSize { get; }

        void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount);
        void SetScrollPercent(double horizontalPercent, double verticalPercent);
    }

    public interface IScrollPatternProperties
    {
        PropertyId HorizontallyScrollable { get; }
        PropertyId HorizontalScrollPercent { get; }
        PropertyId HorizontalViewSize { get; }
        PropertyId VerticallyScrollable { get; }
        PropertyId VerticalScrollPercent { get; }
        PropertyId VerticalViewSize { get; }
    }

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
