namespace Gu.Wpf.UiAutomation
{
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

        /// <inheritdoc/>
        public IRangeValuePatternProperties Properties => this.Automation.PropertyLibrary.RangeValue;

        /// <inheritdoc/>
        public AutomationProperty<bool> IsReadOnly => this.GetOrCreate(ref this.isReadOnly, this.Properties.IsReadOnly);

        /// <inheritdoc/>
        public AutomationProperty<double> LargeChange => this.GetOrCreate(ref this.largeChange, this.Properties.LargeChange);

        /// <inheritdoc/>
        public AutomationProperty<double> Maximum => this.GetOrCreate(ref this.maximum, this.Properties.Maximum);

        /// <inheritdoc/>
        public AutomationProperty<double> Minimum => this.GetOrCreate(ref this.minimum, this.Properties.Minimum);

        /// <inheritdoc/>
        public AutomationProperty<double> SmallChange => this.GetOrCreate(ref this.smallChange, this.Properties.SmallChange);

        /// <inheritdoc/>
        public AutomationProperty<double> Value => this.GetOrCreate(ref this.value, this.Properties.Value);

        /// <inheritdoc/>
        public abstract void SetValue(double val);
    }
}
