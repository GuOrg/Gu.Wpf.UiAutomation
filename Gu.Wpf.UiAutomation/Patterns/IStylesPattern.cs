namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

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