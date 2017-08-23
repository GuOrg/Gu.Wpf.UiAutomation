namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public class ConditionList : List<ConditionBase>
    {
        public ConditionList(params ConditionBase[] conditions)
        {
            this.AddRange(conditions);
        }
    }
}
