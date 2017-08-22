using System;

namespace Gu.Wpf.UiAutomation.Conditions
{
    public class NotCondition : ConditionBase
    {
        public ConditionBase Condition { get; }

        public NotCondition(ConditionBase condition)
        {
            Condition = condition;
        }

        public override string ToString()
        {
            return String.Format("NOT ({0})", Condition);
        }
    }
}
