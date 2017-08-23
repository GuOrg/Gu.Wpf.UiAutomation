namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class SelectionItemPatternProperties : ISelectionItemPatternProperties
    {
        public PropertyId IsSelected => SelectionItemPattern.IsSelectedProperty;

        public PropertyId SelectionContainer => SelectionItemPattern.SelectionContainerProperty;
    }
}