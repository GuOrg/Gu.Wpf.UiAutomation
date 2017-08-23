namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IValuePatternProperties
    {
        PropertyId IsReadOnly { get; }

        PropertyId Value { get; }
    }
}