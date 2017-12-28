namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static bool TryFindFirst(this AutomationElement element, TreeScope treeScope, ConditionBase condition, out AutomationElement match)
        {
            match = element.FindFirst(treeScope, condition.ToNative());
            return match != null;
        }

        public static AutomationElement FindFirst(this AutomationElement element, TreeScope treeScope, ConditionBase condition)
        {
            return element.FindFirst(treeScope, condition.ToNative());
        }

        public static T FindFirst<T>(this AutomationElement element, TreeScope treeScope, ConditionBase condition, Func<AutomationElement, T> wrap)
        {
            return wrap(element.FindFirst(treeScope, condition.ToNative()));
        }

        public static AutomationElementCollection FindAllChildren(this AutomationElement element, ConditionBase condition)
        {
            return element.FindAll(TreeScope.Children, condition.ToNative());
        }

        public static AutomationElementCollection FindAll(this AutomationElement element, TreeScope treeScope, ConditionBase condition)
        {
            return element.FindAll(treeScope, condition.ToNative());
        }

        public static IReadOnlyList<T> FindAll<T>(this AutomationElement element, TreeScope treeScope, ConditionBase condition, Func<AutomationElement, T> wrap)
        {
            var elements = FindAll(element, treeScope, condition);
            var result = new T[elements.Count];
            for (var i = 0; i < elements.Count; i++)
            {
                result[i] = wrap(elements[i]);
            }

            return result;
        }

        public static bool TryFindIndexed(this AutomationElement element, TreeScope treeScope, ConditionBase condition, int index, out AutomationElement result)
        {
            result = null;
            if (index < 0)
            {
                return false;
            }

            var elements = element.FindAll(treeScope, condition.ToNative());
            if (index >= elements.Count)
            {
                return false;
            }

            result = elements[index];
            return true;
        }

        public static AutomationElement FindIndexed(this AutomationElement element, TreeScope treeScope, ConditionBase condition, int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be greater than or equalt to zero.");
            }

            var elements = element.FindAll(treeScope, condition.ToNative());
            if (index >= elements.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be less than count.");
            }

            return elements[index];
        }

        public static T FindIndexed<T>(this AutomationElement element, TreeScope treeScope, ConditionBase condition, int index, Func<AutomationElement, T> wrap)
        {
            return wrap(FindIndexed(element, treeScope, condition, index));
        }
    }
}