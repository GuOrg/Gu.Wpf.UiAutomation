namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class ValuePatternProperties : IValuePatternProperties
    {
        public PropertyId IsReadOnly => ValuePattern.IsReadOnlyProperty;

        public PropertyId Value => ValuePattern.ValueProperty;
    }
}