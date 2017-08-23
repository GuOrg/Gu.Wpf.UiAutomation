namespace Gu.Wpf.UiAutomation.Conditions
{
    public class NotCondition : ConditionBase
    {
        public ConditionBase Condition { get; }

        public NotCondition(ConditionBase condition)
        {
            this.Condition = condition;
        }

        public override string ToString()
        {
            return string.Format("NOT ({0})", this.Condition);
        }
    }
}
