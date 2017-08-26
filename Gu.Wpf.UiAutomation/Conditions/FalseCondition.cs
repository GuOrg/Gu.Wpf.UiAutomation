namespace Gu.Wpf.UiAutomation
{
    public sealed class FalseCondition : BoolCondition
    {
        private FalseCondition()
            : base(booleanValue: false)
        {
        }

        public static FalseCondition Default { get; } = new FalseCondition();
    }
}