namespace Gu.Wpf.UiAutomation
{
    public interface ITogglePattern : IPattern
    {
        ITogglePatternProperties Properties { get; }

        AutomationProperty<ToggleState> ToggleState { get; }

        void Toggle();
    }
}