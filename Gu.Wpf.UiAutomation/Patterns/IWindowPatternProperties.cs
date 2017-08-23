namespace Gu.Wpf.UiAutomation
{
    public interface IWindowPatternProperties
    {
        PropertyId CanMaximize { get; }

        PropertyId CanMinimize { get; }

        PropertyId IsModal { get; }

        PropertyId IsTopmost { get; }

        PropertyId WindowInteractionState { get; }

        PropertyId WindowVisualState { get; }
    }
}