namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public interface ISelectionPattern : IPattern
    {
        ISelectionPatternProperties Properties { get; }

        ISelectionPatternEvents Events { get; }

        AutomationProperty<bool> CanSelectMultiple { get; }

        AutomationProperty<bool> IsSelectionRequired { get; }

        AutomationProperty<IReadOnlyList<AutomationElement>> Selection { get; }
    }
}