namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({string.Join(" AND ", this.Conditions.Select(c => c.ToString()))})";
        }

        public override Condition ToNative()
        {
            return new System.Windows.Automation.AndCondition(this.Conditions.Select(x => x.ToNative()).ToArray());
        }
    }
}
