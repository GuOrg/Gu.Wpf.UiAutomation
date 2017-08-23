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
            return $"BOOL: {this.BooleanValue}";
        }
    }
}
