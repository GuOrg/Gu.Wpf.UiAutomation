namespace Gu.Wpf.UiAutomation
{
    public interface IDockPattern : IPattern
    {
        IDockPatternProperties Properties { get; }

        AutomationProperty<DockPosition> DockPosition { get; }

        void SetDockPosition(DockPosition dockPos);
    }
}