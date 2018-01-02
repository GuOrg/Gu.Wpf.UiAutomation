namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;

    public static class ItemContainerPatternExt
    {
        public static IEnumerable<AutomationElement> AllItems(this ItemContainerPattern pattern)
        {
            AutomationElement item = null;
            while (true)
            {
                item = pattern.FindItemByProperty(item, null, null);
                if (item == null)
                {
                    break;
                }

                yield return item;
            }
        }

        public static IEnumerable<T> AllItems<T>(this ItemContainerPattern pattern, Func<AutomationElement, T> wrap)
        {
            AutomationElement item = null;
            while (true)
            {
                item = pattern.FindItemByProperty(item, null, null);
                if (item == null)
                {
                    break;
                }

                yield return wrap(item);
            }
        }

        public static AutomationElement FindAtIndex(this ItemContainerPattern pattern, int index)
        {
            var current = 0;
            AutomationElement item = null;
            while (true)
            {
                item = pattern.FindItemByProperty(item, null, null);
                if (item == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, "Could not get item at index.");
                }

                if (current == index)
                {
                    if (item.TryGetScrollItemPattern(out var scrollItem))
                    {
                        scrollItem.ScrollIntoView();
                    }

                    return item;
                }

                current++;
            }
        }

        public static T FindAtIndex<T>(this ItemContainerPattern pattern, int index, Func<AutomationElement, T> wrap)
        {
            return wrap(FindAtIndex(pattern, index));
        }

        public static AutomationElement FindByText(this ItemContainerPattern pattern, string text)
        {
            var item = pattern.FindItemByProperty(null, AutomationElement.NameProperty, text);
            if (item != null)
            {
                return item;
            }

            var byNameCondition = new PropertyCondition(AutomationElement.NameProperty, text);
            while (true)
            {
                item = pattern.FindItemByProperty(item, null, null);
                if (item == null)
                {
                    throw new InvalidOperationException($"Did not find an item by text {text}");
                }

                if (item.Name() == text)
                {
                    return item;
                }

                if (item.IsContentElement() &&
                    item.TryFindFirst(TreeScope.Children, byNameCondition, out _))
                {
                    return item;
                }
            }
        }
    }
}
