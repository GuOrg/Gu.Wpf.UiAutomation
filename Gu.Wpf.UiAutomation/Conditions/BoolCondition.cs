namespace Gu.Wpf.UiAutomation.Conditions
{
    public class BoolCondition : ConditionBase
    {
        public BoolCondition(bool booleanValue)
        {
            this.BooleanValue = booleanValue;
        }

        public bool BooleanValue { get; }

        public override string ToString()
        {
            return string.Format("BOOL: {0}", this.BooleanValue);
        }
    }

    public class TrueCondition : BoolCondition
    {
        public TrueCondition()
            : base(true)
        {
        }
    }

    public class FalseCondition : BoolCondition
    {
        public FalseCondition()
            : base(false)
        {
        }
    }
}
