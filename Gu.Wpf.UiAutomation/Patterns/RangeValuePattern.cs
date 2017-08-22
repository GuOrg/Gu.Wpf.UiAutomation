namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IRangeValuePattern : IPattern
    {
        IRangeValuePatternProperties Properties { get; }

        AutomationProperty<bool> IsReadOnly { get; }

        AutomationProperty<double> LargeChange { get; }

        AutomationProperty<double> Maximum { get; }

        AutomationProperty<double> Minimum { get; }

        AutomationProperty<double> SmallChange { get; }

        AutomationProperty<double> Value { get; }

        void SetValue(double val);
    }

    public interface IRangeValuePatternProperties
    {
        PropertyId IsReadOnly { get; }

        PropertyId LargeChange { get; }

        PropertyId Maximum { get; }

        PropertyId Minimum { get; }

        PropertyId SmallChange { get; }

        PropertyId Value { get; }
    }

    public abstract class RangeValuePatternBase<TNativePattern> : PatternBase<TNativePattern>, IRangeValuePattern
    {
        private AutomationProperty<bool> isReadOnly;
        private AutomationProperty<double> largeChange;
        private AutomationProperty<double> maximum;
        private AutomationProperty<double> minimum;
        private AutomationProperty<double> smallChange;
        private AutomationProperty<double> value;

        protected RangeValuePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public IRangeValuePatternProperties Properties => this.Automation.PropertyLibrary.RangeValue;

        public AutomationProperty<bool> IsReadOnly => this.GetOrCreate(ref this.isReadOnly, this.Properties.IsReadOnly);

        public AutomationProperty<double> LargeChange => this.GetOrCreate(ref this.largeChange, this.Properties.LargeChange);

        public AutomationProperty<double> Maximum => this.GetOrCreate(ref this.maximum, this.Properties.Maximum);

        public AutomationProperty<double> Minimum => this.GetOrCreate(ref this.minimum, this.Properties.Minimum);

        public AutomationProperty<double> SmallChange => this.GetOrCreate(ref this.smallChange, this.Properties.SmallChange);

        public AutomationProperty<double> Value => this.GetOrCreate(ref this.value, this.Properties.Value);

        public abstract void SetValue(double val);
    }
}
