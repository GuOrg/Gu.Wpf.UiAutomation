namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class StylesPatternBase<TNativePattern> : PatternBase<TNativePattern>, IStylesPattern
    {
        private AutomationProperty<string> extendedProperties;
        private AutomationProperty<int> fillColor;
        private AutomationProperty<int> fillPatternColor;
        private AutomationProperty<string> fillPatternStyle;
        private AutomationProperty<string> shape;
        private AutomationProperty<StyleType> style;
        private AutomationProperty<string> styleName;

        protected StylesPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public IStylesPatternProperties Properties => this.Automation.PropertyLibrary.Styles;

        public AutomationProperty<string> ExtendedProperties => this.GetOrCreate(ref this.extendedProperties, this.Properties.ExtendedProperties);

        public AutomationProperty<int> FillColor => this.GetOrCreate(ref this.fillColor, this.Properties.FillColor);

        public AutomationProperty<int> FillPatternColor => this.GetOrCreate(ref this.fillPatternColor, this.Properties.FillPatternColor);

        public AutomationProperty<string> FillPatternStyle => this.GetOrCreate(ref this.fillPatternStyle, this.Properties.FillPatternStyle);

        public AutomationProperty<string> Shape => this.GetOrCreate(ref this.shape, this.Properties.Shape);

        public AutomationProperty<StyleType> Style => this.GetOrCreate(ref this.style, this.Properties.StyleId);

        public AutomationProperty<string> StyleName => this.GetOrCreate(ref this.styleName, this.Properties.StyleName);
    }
}
