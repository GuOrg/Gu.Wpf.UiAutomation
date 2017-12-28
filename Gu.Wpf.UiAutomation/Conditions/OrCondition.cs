namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class OrCondition : JunctionConditionBase
    {
        public OrCondition(ConditionBase condition1, ConditionBase condition2)
            : this(new[] { condition1, condition2 })
        {
        }

        public OrCondition(ConditionBase condition1, ConditionBase condition2, ConditionBase condition3)
            : this(new[] { condition1, condition2, condition3 })
        {
        }

        public OrCondition(IEnumerable<ConditionBase> conditions)
        {
            this.Conditions.AddRange(conditions);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({string.Join(" OR ", this.Conditions.Select(c => c.ToString()))})";
        }

        public override Condition ToNative()
        {
            return new System.Windows.Automation.OrCondition(this.Conditions.Select(x => x.ToNative()).ToArray());
        }
    }
}
