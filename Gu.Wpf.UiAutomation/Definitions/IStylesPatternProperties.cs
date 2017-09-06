namespace Gu.Wpf.UiAutomation
{
    public interface IStylesPatternProperties
    {
        PropertyId ExtendedProperties { get; }

        PropertyId FillColor { get; }

        PropertyId FillPatternColor { get; }

        PropertyId FillPatternStyle { get; }

        PropertyId Shape { get; }

        PropertyId StyleId { get; }

        PropertyId StyleName { get; }
    }
}