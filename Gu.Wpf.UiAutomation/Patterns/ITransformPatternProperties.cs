namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ITransformPatternProperties
    {
        PropertyId CanMove { get; }

        PropertyId CanResize { get; }

        PropertyId CanRotate { get; }
    }
}