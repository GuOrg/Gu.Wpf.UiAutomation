namespace Gu.Wpf.UiAutomation
{
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
    }
}
