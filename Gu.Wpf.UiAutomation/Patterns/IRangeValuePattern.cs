namespace Gu.Wpf.UiAutomation.Patterns
{
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
}