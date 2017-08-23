namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ITransform2PatternProperties : ITransformPatternProperties
    {
        PropertyId CanZoom { get; }

        PropertyId ZoomLevel { get; }

        PropertyId ZoomMaximum { get; }

        PropertyId ZoomMinimum { get; }
    }
}