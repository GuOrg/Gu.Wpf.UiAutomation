namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IMultipleViewPattern : IPattern
    {
        IMultipleViewPatternProperties Properties { get; }

        AutomationProperty<int> CurrentView { get; }

        AutomationProperty<int[]> SupportedViews { get; }

        string GetViewName(int view);

        void SetCurrentView(int view);
    }
}