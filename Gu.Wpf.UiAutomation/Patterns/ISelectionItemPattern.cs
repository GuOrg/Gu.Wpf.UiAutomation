namespace Gu.Wpf.UiAutomation
{
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