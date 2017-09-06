namespace Gu.Wpf.UiAutomation
{
    public interface ITransform2Pattern : ITransformPattern
    {
        new ITransform2PatternProperties Properties { get; }

        AutomationProperty<bool> CanZoom { get; }

        AutomationProperty<double> ZoomLevel { get; }

        AutomationProperty<double> ZoomMaximum { get; }

        AutomationProperty<double> ZoomMinimum { get; }

        void Zoom(double zoom);

        void ZoomByUnit(ZoomUnit zoomUnit);
    }
}