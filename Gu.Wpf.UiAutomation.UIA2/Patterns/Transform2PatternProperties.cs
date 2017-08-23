namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class Transform2PatternProperties : TransformPatternProperties, ITransform2PatternProperties
    {
        public PropertyId CanZoom => PropertyId.NotSupportedByFramework;

        public PropertyId ZoomLevel => PropertyId.NotSupportedByFramework;

        public PropertyId ZoomMaximum => PropertyId.NotSupportedByFramework;

        public PropertyId ZoomMinimum => PropertyId.NotSupportedByFramework;
    }
}