namespace Gu.Wpf.UiAutomation.UIA3.Converters
{
    using System;
    using System.Linq;
    using UIA = Interop.UIAutomationClient;

    public static class ConditionConverter
    {
        public static UIA.IUIAutomationCondition ToNative(UIA3Automation automation, ConditionBase condition)
        {
            if (condition is PropertyCondition propCond)
            {
                return automation.NativeAutomation.CreatePropertyConditionEx(propCond.Property.Id, ValueConverter.ToNative(propCond.Value), (UIA.PropertyConditionFlags)propCond.PropertyConditionFlags);
            }

            if (condition is BoolCondition boolCond)
            {
                return boolCond.BooleanValue ? automation.NativeAutomation.CreateTrueCondition() : automation.NativeAutomation.CreateFalseCondition();
            }

            if (condition is NotCondition notCond)
            {
                return automation.NativeAutomation.CreateNotCondition(ToNative(automation, notCond.Condition));
            }

            if (condition is JunctionConditionBase junctCond)
            {
                if (junctCond.ChildCount == 0)
                {
                    // No condition in the list, so just create a true condition
                    return automation.NativeAutomation.CreateTrueCondition();
                }

                if (junctCond.ChildCount == 1)
                {
                    // Only one condition in the list, so just return that one
                    return ToNative(automation, junctCond.Conditions[0]);
                }

                if (junctCond is AndCondition)
                {
                    // Create the and condition
                    return automation.NativeAutomation.CreateAndConditionFromArray(junctCond.Conditions.Select(c => ToNative(automation, c)).ToArray());
                }

                // Create the or condition
                return automation.NativeAutomation.CreateOrConditionFromArray(junctCond.Conditions.Select(c => ToNative(automation, c)).ToArray());
            }

            throw new ArgumentException("Unknown condition type");
        }
    }
}
