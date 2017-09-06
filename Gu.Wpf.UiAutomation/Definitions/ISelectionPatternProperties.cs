namespace Gu.Wpf.UiAutomation
{
    public interface ISelectionPatternProperties
    {
        PropertyId CanSelectMultiple { get; }

        PropertyId IsSelectionRequired { get; }

        PropertyId Selection { get; }
    }
}