namespace Gu.Wpf.UiAutomation
{
    public interface IValuePattern : IPattern
    {
        IValuePatternProperties Properties { get; }

        AutomationProperty<bool> IsReadOnly { get; }

        AutomationProperty<string> Value { get; }

        void SetValue(string value);
    }
}