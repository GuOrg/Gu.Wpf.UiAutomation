namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IObjectModelPattern : IPattern
    {
        object GetUnderlyingObjectModel();
    }
}
