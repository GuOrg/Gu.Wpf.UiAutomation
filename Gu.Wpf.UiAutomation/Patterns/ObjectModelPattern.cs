using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

namespace Gu.Wpf.UiAutomation.Patterns
{
    public interface IObjectModelPattern : IPattern
    {
        object GetUnderlyingObjectModel();
    }
}
