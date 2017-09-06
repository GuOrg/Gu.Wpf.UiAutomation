namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class TransformPattern : TransformPatternBase<Interop.UIAutomationClient.IUIAutomationTransformPattern>
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_TransformPatternId, "Transform", AutomationObjectIds.IsTransformPatternAvailableProperty);
        public static readonly PropertyId CanMoveProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_TransformCanMovePropertyId, "CanMove");
        public static readonly PropertyId CanResizeProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_TransformCanResizePropertyId, "CanResize");
        public static readonly PropertyId CanRotateProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_TransformCanRotatePropertyId, "CanRotate");

        public TransformPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationTransformPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Move(double x, double y)
        {
            ComCallWrapper.Call(() => this.NativePattern.Move(x, y));
        }

        public override void Resize(double width, double height)
        {
            ComCallWrapper.Call(() => this.NativePattern.Resize(width, height));
        }

        public override void Rotate(double degrees)
        {
            ComCallWrapper.Call(() => this.NativePattern.Rotate(degrees));
        }
    }
}
