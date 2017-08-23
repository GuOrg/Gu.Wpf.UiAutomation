namespace Gu.Wpf.UiAutomation
{
    public abstract class StylesPatternBase<TNativePattern> : PatternBase<TNativePattern>, IStylesPattern
        where TNativePattern : class
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

        /// <inheritdoc/>
        public IStylesPatternProperties Properties => this.Automation.PropertyLibrary.Styles;

        /// <inheritdoc/>
        public AutomationProperty<string> ExtendedProperties => this.GetOrCreate(ref this.extendedProperties, this.Properties.ExtendedProperties);

        /// <inheritdoc/>
        public AutomationProperty<int> FillColor => this.GetOrCreate(ref this.fillColor, this.Properties.FillColor);

        /// <inheritdoc/>
        public AutomationProperty<int> FillPatternColor => this.GetOrCreate(ref this.fillPatternColor, this.Properties.FillPatternColor);

        /// <inheritdoc/>
        public AutomationProperty<string> FillPatternStyle => this.GetOrCreate(ref this.fillPatternStyle, this.Properties.FillPatternStyle);

        /// <inheritdoc/>
        public AutomationProperty<string> Shape => this.GetOrCreate(ref this.shape, this.Properties.Shape);

        /// <inheritdoc/>
        public AutomationProperty<StyleType> Style => this.GetOrCreate(ref this.style, this.Properties.StyleId);

        /// <inheritdoc/>
        public AutomationProperty<string> StyleName => this.GetOrCreate(ref this.styleName, this.Properties.StyleName);
    }
}
