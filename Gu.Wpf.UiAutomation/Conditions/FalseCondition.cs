namespace Gu.Wpf.UiAutomation
{
    public class FalseCondition : BoolCondition
    {
        private FalseCondition()
            : base((bool)false)
        {
        }

        public static FalseCondition Default { get; } = new FalseCondition();
    }
}