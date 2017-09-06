namespace Gu.Wpf.UiAutomation
{
    using Interop.UIAutomationClient;

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

        public override IUIAutomationCondition ToNative(IUIAutomation automation)
        {
            return automation.CreateNotCondition(this.Condition.ToNative(automation));
        }
    }
}
