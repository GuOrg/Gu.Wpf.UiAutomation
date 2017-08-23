namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class StylesPattern : StylesPatternBase<UIA.IUIAutomationStylesPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_StylesPatternId, "Styles", AutomationObjectIds.IsStylesPatternAvailableProperty);
        public static readonly PropertyId ExtendedPropertiesProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_StylesExtendedPropertiesPropertyId, "ExtendedProperties");
        public static readonly PropertyId FillColorProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_StylesFillColorPropertyId, "FillColor");
        public static readonly PropertyId FillPatternColorProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_StylesFillPatternColorPropertyId, "FillPatternColor");
        public static readonly PropertyId FillPatternStyleProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_StylesFillPatternStylePropertyId, "FillPatternStyle");
        public static readonly PropertyId ShapeProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_StylesShapePropertyId, "Shape");
        public static readonly PropertyId StyleIdProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_StylesStyleIdPropertyId, "StyleId").SetConverter((a, o) => StyleTypeConverter.ToStyleType(o));
        public static readonly PropertyId StyleNameProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_StylesStyleNamePropertyId, "StyleName");

        public StylesPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationStylesPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        // TODO: Any way to implement that?
        ////public void GetCachedExtendedPropertiesAsArray(IntPtr propertyArray, out int propertyCount){}
        ////public void GetCurrentExtendedPropertiesAsArray(IntPtr propertyArray, out int propertyCount){}
    }
}
