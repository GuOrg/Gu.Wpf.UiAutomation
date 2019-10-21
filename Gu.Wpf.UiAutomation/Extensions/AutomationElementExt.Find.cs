namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static AutomationElement Parent(this AutomationElement element)
        {
            return TreeWalker.RawViewWalker.GetParent(element);
        }

        public static IEnumerable<AutomationElement> Children(this AutomationElement element)
        {
            return TreeWalker.RawViewWalker.Children(element);
        }

        public static bool TryFindFirst(this AutomationElement element, TreeScope treeScope, Condition condition, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out AutomationElement? match)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            match = treeScope == TreeScope.Ancestors
                ? new TreeWalker(condition).Ancestors(element).FirstOrDefault()
                : element.FindFirst(treeScope, condition);
            if (match == null)
            {
                switch (treeScope)
                {
                    case TreeScope.Children:
                        foreach (var child in TreeWalker.RawViewWalker.Children(element))
                        {
                            if (Conditions.IsMatch(child, condition))
                            {
                                match = child;
                                return true;
                            }
                        }

                        break;
                    case TreeScope.Descendants:
                        foreach (var child in TreeWalker.RawViewWalker.Descendants(element))
                        {
                            if (Conditions.IsMatch(child, condition))
                            {
                                match = child;
                                return true;
                            }
                        }

                        break;
                    case TreeScope.Parent:
                        break;
                    case TreeScope.Ancestors:
                        break;
                    case TreeScope.Subtree:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(treeScope), treeScope, null);
                }
            }

            return match != null;
        }

        public static AutomationElement FindFirst(this AutomationElement element, TreeScope treeScope, Condition condition)
        {
            if (TryFindFirst(element, treeScope, condition, out var first))
            {
                return first;
            }

            throw new InvalidOperationException($"Did not find a {treeScope} matching {condition.Description()}.");
        }

        public static AutomationElement FindFirstChild(this AutomationElement element, Condition condition)
        {
            return FindFirst(element, TreeScope.Children, condition);
        }

        public static T FindFirst<T>(this AutomationElement element, TreeScope treeScope, Condition condition, Func<AutomationElement, T> wrap)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            return wrap(FindFirst(element, treeScope, condition));
        }

        public static bool TryFindSingleChild(this AutomationElement element, Condition condition, [NotNullWhen(true)]out AutomationElement? match)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            var collection = element.FindAll(TreeScope.Children, condition);
            if (collection?.Count == 1)
            {
                match = collection[0];
                return true;
            }

            match = null;
            return false;
        }

        public static AutomationElementCollection FindAllChildren(this AutomationElement element, Condition condition)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return element.FindAll(TreeScope.Children, condition);
        }

        public static AutomationElementCollection FindAll(this AutomationElement element, TreeScope treeScope, Condition condition)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return element.FindAll(treeScope, condition);
        }

        public static IReadOnlyList<T> FindAll<T>(this AutomationElement element, TreeScope treeScope, Condition condition, Func<AutomationElement, T> wrap)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            var elements = FindAll(element, treeScope, condition);
            var result = new T[elements.Count];
            for (var i = 0; i < elements.Count; i++)
            {
                result[i] = wrap(elements[i]);
            }

            return result;
        }

        public static bool TryFindIndexed(this AutomationElement element, TreeScope treeScope, Condition condition, int index, [NotNullWhen(true)]out AutomationElement? result)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            result = null;
            if (index < 0)
            {
                return false;
            }

            var elements = element.FindAll(treeScope, condition);
            if (index >= elements.Count)
            {
                return false;
            }

            result = elements[index];
            return true;
        }

        public static AutomationElement FindIndexed(this AutomationElement element, TreeScope treeScope, Condition condition, int index)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be greater than or equalt to zero.");
            }

            var elements = element.FindAll(treeScope, condition);
            if (index >= elements.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be less than count.");
            }

            return elements[index];
        }

        public static T FindIndexed<T>(this AutomationElement element, TreeScope treeScope, Condition condition, int index, Func<AutomationElement, T> wrap)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            return wrap(FindIndexed(element, treeScope, condition, index));
        }
    }
}
