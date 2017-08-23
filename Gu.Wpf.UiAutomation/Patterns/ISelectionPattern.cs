namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ISelectionPattern : IPattern
    {
        ISelectionPatternProperties Properties { get; }

        ISelectionPatternEvents Events { get; }

        AutomationProperty<bool> CanSelectMultiple { get; }

        AutomationProperty<bool> IsSelectionRequired { get; }

        AutomationProperty<AutomationElement[]> Selection { get; }
    }
}