namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class MultipleViewPattern : MultipleViewPatternBase<Interop.UIAutomationClient.IUIAutomationMultipleViewPattern>
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_MultipleViewPatternId, "MultipleView", AutomationObjectIds.IsMultipleViewPatternAvailableProperty);
        public static readonly PropertyId CurrentViewProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_MultipleViewCurrentViewPropertyId, "CurrentView");
        public static readonly PropertyId SupportedViewsProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_MultipleViewSupportedViewsPropertyId, "SupportedViews");

        public MultipleViewPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationMultipleViewPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override string GetViewName(int view)
        {
            return Com.Call(() => this.NativePattern.GetViewName(view));
        }

        public override void SetCurrentView(int view)
        {
            Com.Call(() => this.NativePattern.SetCurrentView(view));
        }
    }
}
