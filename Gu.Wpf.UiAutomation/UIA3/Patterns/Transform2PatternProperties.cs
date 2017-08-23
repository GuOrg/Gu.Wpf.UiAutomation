namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class Transform2PatternProperties : TransformPatternProperties, ITransform2PatternProperties
    {
        public PropertyId CanZoom => Transform2Pattern.CanZoomProperty;

        public PropertyId ZoomLevel => Transform2Pattern.ZoomLevelProperty;

        public PropertyId ZoomMaximum => Transform2Pattern.ZoomMaximumProperty;

        public PropertyId ZoomMinimum => Transform2Pattern.ZoomMinimumProperty;
    }
}