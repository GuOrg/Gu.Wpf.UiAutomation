namespace Gu.Wpf.UiAutomation
{
    public interface ISelectionPattern : IPattern
    {
        ISelectionPatternProperties Properties { get; }

        ISelectionPatternEvents Events { get; }

        AutomationProperty<bool> CanSelectMultiple { get; }

        AutomationProperty<bool> IsSelectionRequired { get; }

        AutomationProperty<AutomationElement[]> Selection { get; }
    }
}