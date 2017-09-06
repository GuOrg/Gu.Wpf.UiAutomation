namespace Gu.Wpf.UiAutomation
{
    public interface IStylesPattern : IPattern
    {
        IStylesPatternProperties Properties { get; }

        AutomationProperty<string> ExtendedProperties { get; }

        AutomationProperty<int> FillColor { get; }

        AutomationProperty<int> FillPatternColor { get; }

        AutomationProperty<string> FillPatternStyle { get; }

        AutomationProperty<string> Shape { get; }

        AutomationProperty<StyleType> Style { get; }

        AutomationProperty<string> StyleName { get; }
    }
}