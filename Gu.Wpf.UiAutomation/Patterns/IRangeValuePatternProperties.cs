namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IRangeValuePatternProperties
    {
        PropertyId IsReadOnly { get; }

        PropertyId LargeChange { get; }

        PropertyId Maximum { get; }

        PropertyId Minimum { get; }

        PropertyId SmallChange { get; }

        PropertyId Value { get; }
    }
}