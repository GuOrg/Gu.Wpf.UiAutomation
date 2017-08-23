namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ISelectionPatternProperties
    {
        PropertyId CanSelectMultiple { get; }

        PropertyId IsSelectionRequired { get; }

        PropertyId Selection { get; }
    }
}