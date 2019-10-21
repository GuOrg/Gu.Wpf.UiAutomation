namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;

    public static class ItemContainerPatternExt
    {
        public static AutomationElement? FirstOrDefault(this ItemContainerPattern pattern)
        {
            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            var item = pattern.FindItemByProperty(null, null, null);
            if (item != null &&
                item.TryGetVirtualizedItemPattern(out var virtualizedItemPattern))
            {
                virtualizedItemPattern.Realize();
            }

            return item;
        }

        public static AutomationElement? LastOrDefault(this ItemContainerPattern pattern)
        {
            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            var item = pattern.FindItemByProperty(null, null, null);
            if (item == null)
            {
                return null;
            }

            while (true)
            {
                var temp = pattern.FindItemByProperty(item, null, null);
                if (temp == null)
                {
                    if (item.TryGetVirtualizedItemPattern(out var virtualizedItemPattern))
                    {
                        virtualizedItemPattern.Realize();
                    }

                    return item;
                }

                item = temp;
            }
        }

        public static IEnumerable<AutomationElement> AllItems(this ItemContainerPattern pattern)
        {
            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            AutomationElement? item = null;
            while (true)
            {
                item = pattern.FindItemByProperty(item, null, null);
                if (item == null)
                {
                    break;
                }

                if (item.TryGetVirtualizedItemPattern(out var virtualizedItemPattern))
                {
                    virtualizedItemPattern.Realize();
                }

                yield return item;
            }
        }

        public static IEnumerable<T> AllItems<T>(this ItemContainerPattern pattern, Func<AutomationElement, T> wrap)
        {
            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            AutomationElement? item = null;
            while (true)
            {
                item = pattern.FindItemByProperty(item, null, null);
                if (item == null)
                {
                    break;
                }

                if (item.TryGetVirtualizedItemPattern(out var virtualizedItemPattern))
                {
                    virtualizedItemPattern.Realize();
                }

                yield return wrap(item);
            }
        }

        public static AutomationElement FindAtIndex(this ItemContainerPattern pattern, int index)
        {
            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            var current = 0;
            AutomationElement? item = null;
            while (true)
            {
                item = pattern.FindItemByProperty(item, null, null);
                if (item == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, "Could not get item at index.");
                }

                if (current == index)
                {
                    if (item.TryGetVirtualizedItemPattern(out var virtualizedItemPattern))
                    {
                        virtualizedItemPattern.Realize();
                    }

                    return item;
                }

                current++;
            }
        }

        public static T FindAtIndex<T>(this ItemContainerPattern pattern, int index, Func<AutomationElement, T> wrap)
        {
            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            return wrap(FindAtIndex(pattern, index));
        }

        public static AutomationElement FindByText(this ItemContainerPattern pattern, string text)
        {
            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

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

                if (item.TryGetVirtualizedItemPattern(out var virtualizedItemPattern))
                {
                    virtualizedItemPattern.Realize();
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
