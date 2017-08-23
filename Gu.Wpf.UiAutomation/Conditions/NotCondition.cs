namespace Gu.Wpf.UiAutomation
{
    public class NotCondition : ConditionBase
    {
        public ConditionBase Condition { get; }

        public NotCondition(ConditionBase condition)
        {
            this.Condition = condition;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"NOT ({this.Condition})";
        }
    }
}
