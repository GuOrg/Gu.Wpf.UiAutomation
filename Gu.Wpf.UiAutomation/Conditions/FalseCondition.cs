namespace Gu.Wpf.UiAutomation
{
    using Interop.UIAutomationClient;

    public sealed class FalseCondition : BoolCondition
    {
        private FalseCondition()
            : base(booleanValue: false)
        {
        }

        public static FalseCondition Default { get; } = new FalseCondition();

        public override IUIAutomationCondition ToNative(IUIAutomation automation) => automation.CreateTrueCondition();
    }
}