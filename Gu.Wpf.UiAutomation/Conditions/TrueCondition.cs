namespace Gu.Wpf.UiAutomation
{
    using Interop.UIAutomationClient;

    public sealed class TrueCondition : BoolCondition
    {
        private TrueCondition()
            : base(booleanValue: true)
        {
        }

        public static TrueCondition Default { get; } = new TrueCondition();

        public override IUIAutomationCondition ToNative(IUIAutomation automation) => automation.CreateTrueCondition();
    }
}