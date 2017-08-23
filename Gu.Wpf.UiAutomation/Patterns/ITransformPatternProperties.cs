namespace Gu.Wpf.UiAutomation
{
    public interface ITransformPatternProperties
    {
        PropertyId CanMove { get; }

        PropertyId CanResize { get; }

        PropertyId CanRotate { get; }
    }
}