namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class SelectionPatternProperties : ISelectionPatternProperties
    {
        public PropertyId CanSelectMultiple => SelectionPattern.CanSelectMultipleProperty;

        public PropertyId IsSelectionRequired => SelectionPattern.IsSelectionRequiredProperty;

        public PropertyId Selection => SelectionPattern.SelectionProperty;
    }
}