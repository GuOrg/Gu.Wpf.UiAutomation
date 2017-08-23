namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class TransformPattern : TransformPatternBase<UIA.IUIAutomationTransformPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_TransformPatternId, "Transform", AutomationObjectIds.IsTransformPatternAvailableProperty);
        public static readonly PropertyId CanMoveProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_TransformCanMovePropertyId, "CanMove");
        public static readonly PropertyId CanResizeProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_TransformCanResizePropertyId, "CanResize");
        public static readonly PropertyId CanRotateProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_TransformCanRotatePropertyId, "CanRotate");

        public TransformPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationTransformPattern nativePattern)
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
