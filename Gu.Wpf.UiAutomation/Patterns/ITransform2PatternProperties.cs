namespace Gu.Wpf.UiAutomation
{
    public interface ITransform2PatternProperties : ITransformPatternProperties
    {
        PropertyId CanZoom { get; }

        PropertyId ZoomLevel { get; }

        PropertyId ZoomMaximum { get; }

        PropertyId ZoomMinimum { get; }
    }
}