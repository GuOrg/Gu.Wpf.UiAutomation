namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public sealed class FalseCondition : BoolCondition
    {
        private FalseCondition()
            : base(booleanValue: false)
        {
        }

        public static FalseCondition Default { get; } = new FalseCondition();

        public override Condition ToNative() => Condition.FalseCondition;
    }
}