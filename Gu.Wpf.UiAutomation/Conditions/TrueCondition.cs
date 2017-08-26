namespace Gu.Wpf.UiAutomation
{
    public sealed class TrueCondition : BoolCondition
    {
        private TrueCondition()
            : base(booleanValue: true)
        {
        }

        public static TrueCondition Default { get; } = new TrueCondition();
    }
}