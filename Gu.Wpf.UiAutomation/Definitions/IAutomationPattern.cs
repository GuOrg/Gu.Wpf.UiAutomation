namespace Gu.Wpf.UiAutomation
{
    public interface IAutomationPattern<T>
        where T : IPattern
    {
        T Pattern { get; }

        T PatternOrDefault { get; }

        bool IsSupported { get; }

        bool TryGetPattern(out T pattern);
    }
}