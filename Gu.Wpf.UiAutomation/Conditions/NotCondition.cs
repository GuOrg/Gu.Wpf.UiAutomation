namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class NotCondition : ConditionBase
    {
        public NotCondition(ConditionBase condition)
        {
            this.Condition = condition;
        }

        public ConditionBase Condition { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"NOT ({this.Condition})";
        }

        public override Condition ToNative()
        {
            return new System.Windows.Automation.NotCondition(this.Condition.ToNative());
        }
    }
}
