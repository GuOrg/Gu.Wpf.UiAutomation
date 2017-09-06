namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using Interop.UIAutomationClient;

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

        public override IUIAutomationCondition ToNative(IUIAutomation automation)
        {
            return automation.CreateAndConditionFromArray(this.Conditions.Select(c => c.ToNative(automation)).ToArray());
        }
    }
}
