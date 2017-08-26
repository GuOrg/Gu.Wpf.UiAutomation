namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class AnnotationPattern : AnnotationPatternBase<Interop.UIAutomationClient.IUIAutomationAnnotationPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(Interop.UIAutomationClient.UIA_PatternIds.UIA_AnnotationPatternId, "Annotation", AutomationObjectIds.IsAnnotationPatternAvailableProperty);
        public static readonly PropertyId AnnotationTypeIdProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_AnnotationAnnotationTypeIdPropertyId, "AnnotationTypeId");
        public static readonly PropertyId AnnotationTypeNameProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_AnnotationAnnotationTypeNamePropertyId, "AnnotationTypeName");
        public static readonly PropertyId AuthorProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_AnnotationAuthorPropertyId, "Author");
        public static readonly PropertyId DateTimeProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_AnnotationDateTimePropertyId, "DateTime");
        public static readonly PropertyId TargetProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_AnnotationTargetPropertyId, "Target").SetConverter(AutomationElementConverter.NativeToManaged);

        public AnnotationPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationAnnotationPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }
    }
}
