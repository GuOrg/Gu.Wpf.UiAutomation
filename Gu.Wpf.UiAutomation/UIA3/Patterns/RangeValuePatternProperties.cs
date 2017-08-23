namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class RangeValuePatternProperties : IRangeValuePatternProperties
    {
        public PropertyId IsReadOnly => RangeValuePattern.IsReadOnlyProperty;

        public PropertyId LargeChange => RangeValuePattern.LargeChangeProperty;

        public PropertyId Maximum => RangeValuePattern.MaximumProperty;

        public PropertyId Minimum => RangeValuePattern.MinimumProperty;

        public PropertyId SmallChange => RangeValuePattern.SmallChangeProperty;

        public PropertyId Value => RangeValuePattern.ValueProperty;
    }
}