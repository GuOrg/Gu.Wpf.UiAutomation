namespace Gu.Wpf.UiAutomation.Conditions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AndCondition : JunctionConditionBase
    {
        public AndCondition(ConditionBase condition1, ConditionBase condition2)
            : this(new[] { condition1, condition2 })
        {
        }

        public AndCondition(IEnumerable<ConditionBase> conditions)
        {
            this.Conditions.AddRange(conditions);
        }

        public override string ToString()
        {
#if NET35
            var conditions = String.Join(" AND ", Conditions.Select(c => c.ToString()).ToArray());
#else
            var conditions = string.Join(" AND ", this.Conditions.Select(c => c.ToString()));
#endif
            return $"({conditions})";
        }
    }
}
