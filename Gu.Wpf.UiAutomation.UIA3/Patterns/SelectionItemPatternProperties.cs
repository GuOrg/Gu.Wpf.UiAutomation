namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class SelectionItemPatternProperties : ISelectionItemPatternProperties
    {
        public PropertyId IsSelected => SelectionItemPattern.IsSelectedProperty;

        public PropertyId SelectionContainer => SelectionItemPattern.SelectionContainerProperty;
    }
}