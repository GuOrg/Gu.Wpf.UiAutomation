namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ISelectionItemPatternProperties
    {
        PropertyId IsSelected { get; }

        PropertyId SelectionContainer { get; }
    }
}