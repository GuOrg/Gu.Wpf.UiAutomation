namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA2.Identifiers;
    using UIA = System.Windows.Automation;

    public class MultipleViewPattern : MultipleViewPatternBase<UIA.MultipleViewPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.MultipleViewPattern.Pattern.Id, "MultipleView", AutomationObjectIds.IsMultipleViewPatternAvailableProperty);
        public static readonly PropertyId CurrentViewProperty = PropertyId.Register(AutomationType.UIA2, UIA.MultipleViewPattern.CurrentViewProperty.Id, "CurrentView");
        public static readonly PropertyId SupportedViewsProperty = PropertyId.Register(AutomationType.UIA2, UIA.MultipleViewPattern.SupportedViewsProperty.Id, "SupportedViews");

        public MultipleViewPattern(BasicAutomationElementBase basicAutomationElement, UIA.MultipleViewPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override string GetViewName(int view)
        {
            return this.NativePattern.GetViewName(view);
        }

        public override void SetCurrentView(int view)
        {
            this.NativePattern.SetCurrentView(view);
        }
    }

    public class MultipleViewPatternProperties : IMultipleViewPatternProperties
    {
        public PropertyId CurrentView => MultipleViewPattern.CurrentViewProperty;

        public PropertyId SupportedViews => MultipleViewPattern.SupportedViewsProperty;
    }
}
