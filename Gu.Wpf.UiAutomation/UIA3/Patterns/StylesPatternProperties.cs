namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class StylesPatternProperties : IStylesPatternProperties
    {
        public PropertyId ExtendedProperties => StylesPattern.ExtendedPropertiesProperty;

        public PropertyId FillColor => StylesPattern.FillColorProperty;

        public PropertyId FillPatternColor => StylesPattern.FillPatternColorProperty;

        public PropertyId FillPatternStyle => StylesPattern.FillPatternStyleProperty;

        public PropertyId Shape => StylesPattern.ShapeProperty;

        public PropertyId StyleId => StylesPattern.StyleIdProperty;

        public PropertyId StyleName => StylesPattern.StyleNameProperty;
    }
}