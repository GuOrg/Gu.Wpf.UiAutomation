namespace Gu.Wpf.UiAutomation.Conditions
{
    using System;

    public class NotCondition : ConditionBase
    {
        public ConditionBase Condition { get; }

        public NotCondition(ConditionBase condition)
        {
            this.Condition = condition;
        }

        public override string ToString()
        {
            return String.Format("NOT ({0})", this.Condition);
        }
    }
}
