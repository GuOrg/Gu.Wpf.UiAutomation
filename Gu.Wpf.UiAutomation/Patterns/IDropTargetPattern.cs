namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IDropTargetPattern : IPattern
    {
        IDropTargetPatternProperties Properties { get; }

        IDropTargetPatternEvents Events { get; }

        AutomationProperty<string> DropTargetEffect { get; }

        AutomationProperty<string[]> DropTargetEffects { get; }
    }
}