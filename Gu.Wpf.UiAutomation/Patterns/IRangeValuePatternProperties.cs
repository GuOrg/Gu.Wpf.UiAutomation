namespace Gu.Wpf.UiAutomation
{
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