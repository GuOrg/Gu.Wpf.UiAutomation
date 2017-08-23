namespace Gu.Wpf.UiAutomation
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IAutomationPattern<T>
        where T : IPattern
    {
        T Pattern { get; }

        T PatternOrDefault { get; }

        bool TryGetPattern(out T pattern);

        bool IsSupported { get; }
    }
}