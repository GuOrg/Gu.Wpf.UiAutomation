namespace Gu.Wpf.UiAutomation
{
    public class TrueCondition : BoolCondition
    {
        private TrueCondition()
            : base((bool)true)
        {
        }

        public static TrueCondition Default { get; } = new TrueCondition();
    }
}