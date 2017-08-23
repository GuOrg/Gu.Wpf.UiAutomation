namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class MultipleViewPattern : MultipleViewPatternBase<UIA.IUIAutomationMultipleViewPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_MultipleViewPatternId, "MultipleView", AutomationObjectIds.IsMultipleViewPatternAvailableProperty);
        public static readonly PropertyId CurrentViewProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_MultipleViewCurrentViewPropertyId, "CurrentView");
        public static readonly PropertyId SupportedViewsProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_MultipleViewSupportedViewsPropertyId, "SupportedViews");

        public MultipleViewPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationMultipleViewPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override string GetViewName(int view)
        {
            return ComCallWrapper.Call(() => this.NativePattern.GetViewName(view));
        }

        public override void SetCurrentView(int view)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetCurrentView(view));
        }
    }
}
