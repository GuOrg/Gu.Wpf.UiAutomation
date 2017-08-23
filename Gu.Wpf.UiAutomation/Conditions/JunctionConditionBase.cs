namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public abstract class JunctionConditionBase : ConditionBase
    {
        protected JunctionConditionBase()
        {
            this.Conditions = new List<ConditionBase>();
        }

        public List<ConditionBase> Conditions { get; }

        public int ChildCount => this.Conditions.Count;
    }
}
