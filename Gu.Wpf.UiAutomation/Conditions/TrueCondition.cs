namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public sealed class TrueCondition : BoolCondition
    {
        private TrueCondition()
            : base(booleanValue: true)
        {
        }

        public static TrueCondition Default { get; } = new TrueCondition();

        public override Condition ToNative() => System.Windows.Automation.Condition.TrueCondition;
    }
}