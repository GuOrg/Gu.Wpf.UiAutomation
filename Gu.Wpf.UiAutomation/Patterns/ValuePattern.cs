namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IValuePattern : IPattern
    {
        IValuePatternProperties Properties { get; }

        AutomationProperty<bool> IsReadOnly { get; }

        AutomationProperty<string> Value { get; }

        void SetValue(string value);
    }

    public interface IValuePatternProperties
    {
        PropertyId IsReadOnly { get; }

        PropertyId Value { get; }
    }

    public abstract class ValuePatternBase<TNativePattern> : PatternBase<TNativePattern>, IValuePattern
    {
        private AutomationProperty<bool> isReadOnly;
        private AutomationProperty<string> value;

        protected ValuePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public IValuePatternProperties Properties => this.Automation.PropertyLibrary.Value;

        public AutomationProperty<bool> IsReadOnly => this.GetOrCreate(ref this.isReadOnly, this.Properties.IsReadOnly);

        public AutomationProperty<string> Value => this.GetOrCreate(ref this.value, this.Properties.Value);

        public abstract void SetValue(string value);
    }
}
