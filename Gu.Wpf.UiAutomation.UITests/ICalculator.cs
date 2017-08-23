namespace Gu.Wpf.UiAutomation.UITests
{
    using Gu.Wpf.UiAutomation.AutomationElements;

    public interface ICalculator
    {
        Button Button1 { get; }

        Button Button2 { get; }

        Button Button3 { get; }

        Button Button4 { get; }

        Button Button5 { get; }

        Button Button6 { get; }

        Button Button7 { get; }

        Button Button8 { get; }

        Button ButtonAdd { get; }

        Button ButtonEquals { get; }

        string Result { get; }
    }
}