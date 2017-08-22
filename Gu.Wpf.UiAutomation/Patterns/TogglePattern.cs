namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ITogglePattern : IPattern
    {
        ITogglePatternProperties Properties { get; }

        AutomationProperty<ToggleState> ToggleState { get; }

        void Toggle();
    }

    public interface ITogglePatternProperties
    {
        PropertyId ToggleState { get; }
    }

    public abstract class TogglePatternBase<TNativePattern> : PatternBase<TNativePattern>, ITogglePattern
    {
        private AutomationProperty<ToggleState> toggleState;

        protected TogglePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public ITogglePatternProperties Properties => this.Automation.PropertyLibrary.Toggle;

        public AutomationProperty<ToggleState> ToggleState => this.GetOrCreate(ref this.toggleState, this.Properties.ToggleState);

        public abstract void Toggle();
    }
}
