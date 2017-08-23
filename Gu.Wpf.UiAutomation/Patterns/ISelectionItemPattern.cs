namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ISelectionItemPattern : IPattern
    {
        ISelectionItemPatternProperties Properties { get; }

        ISelectionItemPatternEvents Events { get; }

        AutomationProperty<bool> IsSelected { get; }

        AutomationProperty<AutomationElement> SelectionContainer { get; }

        void AddToSelection();

        void RemoveFromSelection();

        void Select();
    }
}