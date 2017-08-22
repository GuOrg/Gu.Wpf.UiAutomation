using System.Collections.Generic;

namespace Gu.Wpf.UiAutomation.Conditions
{
    public class ConditionList : List<ConditionBase>
    {
        public ConditionList(params ConditionBase[] conditions)
        {
            AddRange(conditions);
        }
    }
}
