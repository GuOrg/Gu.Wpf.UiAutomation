namespace Gu.Wpf.UiAutomation
{
    public interface IDropTargetPattern : IPattern
    {
        IDropTargetPatternProperties Properties { get; }

        IDropTargetPatternEvents Events { get; }

        AutomationProperty<string> DropTargetEffect { get; }

        AutomationProperty<string[]> DropTargetEffects { get; }
    }
}