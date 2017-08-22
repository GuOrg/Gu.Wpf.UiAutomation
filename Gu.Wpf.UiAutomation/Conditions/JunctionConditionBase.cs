namespace Gu.Wpf.UiAutomation.Conditions
{
    using System.Collections.Generic;

    public abstract class JunctionConditionBase : ConditionBase
    {
        protected JunctionConditionBase()
        {
            Conditions = new List<ConditionBase>();
        }

        public List<ConditionBase> Conditions { get; }

        public int ChildCount => Conditions.Count;
    }
}
