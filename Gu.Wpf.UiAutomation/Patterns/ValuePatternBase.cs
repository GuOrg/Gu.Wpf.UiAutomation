namespace Gu.Wpf.UiAutomation
{
    public abstract class ValuePatternBase<TNativePattern> : PatternBase<TNativePattern>, IValuePattern
    {
        private AutomationProperty<bool> isReadOnly;
        private AutomationProperty<string> value;

        protected ValuePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public IValuePatternProperties Properties => this.Automation.PropertyLibrary.Value;

        /// <inheritdoc/>
        public AutomationProperty<bool> IsReadOnly => this.GetOrCreate(ref this.isReadOnly, this.Properties.IsReadOnly);

        /// <inheritdoc/>
        public AutomationProperty<string> Value => this.GetOrCreate(ref this.value, this.Properties.Value);

        /// <inheritdoc/>
        public abstract void SetValue(string value);
    }
}
