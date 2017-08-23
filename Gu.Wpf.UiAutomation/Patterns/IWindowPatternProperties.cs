namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

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