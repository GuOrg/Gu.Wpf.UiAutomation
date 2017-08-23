namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class TransformPatternProperties : ITransformPatternProperties
    {
        public PropertyId CanMove => TransformPattern.CanMoveProperty;

        public PropertyId CanResize => TransformPattern.CanResizeProperty;

        public PropertyId CanRotate => TransformPattern.CanRotateProperty;
    }
}