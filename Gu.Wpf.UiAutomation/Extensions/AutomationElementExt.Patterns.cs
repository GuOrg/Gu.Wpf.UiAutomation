namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static IReadOnlyList<AutomationPattern> Patterns = new[]
                {
                    System.Windows.Automation.DockPattern.Pattern,
                    System.Windows.Automation.ExpandCollapsePattern.Pattern,
                    System.Windows.Automation.GridItemPattern.Pattern,
                    System.Windows.Automation.GridPattern.Pattern,
                    System.Windows.Automation.InvokePattern.Pattern,
                    System.Windows.Automation.MultipleViewPattern.Pattern,
                    System.Windows.Automation.RangeValuePattern.Pattern,
                    System.Windows.Automation.ScrollPattern.Pattern,
                    System.Windows.Automation.ScrollItemPattern.Pattern,
                    System.Windows.Automation.SelectionPattern.Pattern,
                    System.Windows.Automation.SelectionItemPattern.Pattern,
                    System.Windows.Automation.SynchronizedInputPattern.Pattern,
                    System.Windows.Automation.TableItemPattern.Pattern,
                    System.Windows.Automation.TablePattern.Pattern,
                    System.Windows.Automation.TextPattern.Pattern,
                    System.Windows.Automation.TransformPattern.Pattern,
                    System.Windows.Automation.TogglePattern.Pattern,
                    System.Windows.Automation.ValuePattern.Pattern,
                    System.Windows.Automation.WindowPattern.Pattern,
                    System.Windows.Automation.VirtualizedItemPattern.Pattern,
                    System.Windows.Automation.ItemContainerPattern.Pattern,
                };

        public static DockPattern DockPattern(this AutomationElement element)
        {
            if (TryGetDockPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support DockPattern");
        }

        public static bool TryGetDockPattern(this AutomationElement element, out DockPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.DockPattern.Pattern, out var pattern))
            {
                result = (DockPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static ExpandCollapsePattern ExpandCollapsePattern(this AutomationElement element)
        {
            if (TryGetExpandCollapsePattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support ExpandCollapsePattern");
        }

        public static bool TryGetExpandCollapsePattern(this AutomationElement element, out ExpandCollapsePattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.ExpandCollapsePattern.Pattern, out var pattern))
            {
                result = (ExpandCollapsePattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static GridItemPattern GridItemPattern(this AutomationElement element)
        {
            if (TryGetGridItemPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support GridItemPattern");
        }

        public static bool TryGetGridItemPattern(this AutomationElement element, out GridItemPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.GridItemPattern.Pattern, out var pattern))
            {
                result = (GridItemPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static GridPattern GridPattern(this AutomationElement element)
        {
            if (TryGetGridPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support GridPattern");
        }

        public static bool TryGetGridPattern(this AutomationElement element, out GridPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.GridPattern.Pattern, out var pattern))
            {
                result = (GridPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static InvokePattern InvokePattern(this AutomationElement element)
        {
            if (TryGetInvokePattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support InvokePattern");
        }

        public static bool TryGetInvokePattern(this AutomationElement element, out InvokePattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.InvokePattern.Pattern, out var pattern))
            {
                result = (InvokePattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static MultipleViewPattern MultipleViewPattern(this AutomationElement element)
        {
            if (TryGetMultipleViewPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support MultipleViewPattern");
        }

        public static bool TryGetMultipleViewPattern(this AutomationElement element, out MultipleViewPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.MultipleViewPattern.Pattern, out var pattern))
            {
                result = (MultipleViewPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static RangeValuePattern RangeValuePattern(this AutomationElement element)
        {
            if (TryGetRangeValuePattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support RangeValuePattern");
        }

        public static bool TryGetRangeValuePattern(this AutomationElement element, out RangeValuePattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.RangeValuePattern.Pattern, out var pattern))
            {
                result = (RangeValuePattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static ScrollPattern ScrollPattern(this AutomationElement element)
        {
            if (TryGetScrollPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support ScrollPattern");
        }

        public static bool TryGetScrollPattern(this AutomationElement element, out ScrollPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.ScrollPattern.Pattern, out var pattern))
            {
                result = (ScrollPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static ScrollItemPattern ScrollItemPattern(this AutomationElement element)
        {
            if (TryGetScrollItemPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support ScrollItemPattern");
        }

        public static bool TryGetScrollItemPattern(this AutomationElement element, out ScrollItemPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.ScrollItemPattern.Pattern, out var pattern))
            {
                result = (ScrollItemPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static SelectionPattern SelectionPattern(this AutomationElement element)
        {
            if (TryGetSelectionPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support SelectionPattern");
        }

        public static bool TryGetSelectionPattern(this AutomationElement element, out SelectionPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.SelectionPattern.Pattern, out var pattern))
            {
                result = (SelectionPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static SelectionItemPattern SelectionItemPattern(this AutomationElement element)
        {
            if (TryGetSelectionItemPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support SelectionItemPattern");
        }

        public static bool TryGetSelectionItemPattern(this AutomationElement element, out SelectionItemPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.SelectionItemPattern.Pattern, out var pattern))
            {
                result = (SelectionItemPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static SynchronizedInputPattern SynchronizedInputPattern(this AutomationElement element)
        {
            if (TryGetSynchronizedInputPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support SynchronizedInputPattern");
        }

        public static bool TryGetSynchronizedInputPattern(this AutomationElement element, out SynchronizedInputPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.SynchronizedInputPattern.Pattern, out var pattern))
            {
                result = (SynchronizedInputPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static TableItemPattern TableItemPattern(this AutomationElement element)
        {
            if (TryGetTableItemPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support TableItemPattern");
        }

        public static bool TryGetTableItemPattern(this AutomationElement element, out TableItemPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.TableItemPattern.Pattern, out var pattern))
            {
                result = (TableItemPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static TablePattern TablePattern(this AutomationElement element)
        {
            if (TryGetTablePattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support TablePattern");
        }

        public static bool TryGetTablePattern(this AutomationElement element, out TablePattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.TablePattern.Pattern, out var pattern))
            {
                result = (TablePattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static TextPattern TextPattern(this AutomationElement element)
        {
            if (TryGetTextPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support TextPattern");
        }

        public static bool TryGetTextPattern(this AutomationElement element, out TextPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.TextPattern.Pattern, out var pattern))
            {
                result = (TextPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static TransformPattern TransformPattern(this AutomationElement element)
        {
            if (TryGetTransformPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support TransformPattern");
        }

        public static bool TryGetTransformPattern(this AutomationElement element, out TransformPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.TransformPattern.Pattern, out var pattern))
            {
                result = (TransformPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static TogglePattern TogglePattern(this AutomationElement element)
        {
            if (TryGetTogglePattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support TogglePattern");
        }

        public static bool TryGetTogglePattern(this AutomationElement element, out TogglePattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.TogglePattern.Pattern, out var pattern))
            {
                result = (TogglePattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static ValuePattern ValuePattern(this AutomationElement element)
        {
            if (TryGetValuePattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support ValuePattern");
        }

        public static bool TryGetValuePattern(this AutomationElement element, out ValuePattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.ValuePattern.Pattern, out var pattern))
            {
                result = (ValuePattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static WindowPattern WindowPattern(this AutomationElement element)
        {
            if (TryGetWindowPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support WindowPattern");
        }

        public static bool TryGetWindowPattern(this AutomationElement element, out WindowPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.WindowPattern.Pattern, out var pattern))
            {
                result = (WindowPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static VirtualizedItemPattern VirtualizedItemPattern(this AutomationElement element)
        {
            if (TryGetVirtualizedItemPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support VirtualizedItemPattern");
        }

        public static bool TryGetVirtualizedItemPattern(this AutomationElement element, out VirtualizedItemPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.VirtualizedItemPattern.Pattern, out var pattern))
            {
                result = (VirtualizedItemPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

        public static ItemContainerPattern ItemContainerPattern(this AutomationElement element)
        {
            if (TryGetItemContainerPattern(element, out var pattern))
            {
                return pattern;
            }

            throw new System.NotSupportedException($"The element {element} does not support ItemContainerPattern");
        }

        public static bool TryGetItemContainerPattern(this AutomationElement element, out ItemContainerPattern result)
        {
            if (element.TryGetCurrentPattern(System.Windows.Automation.ItemContainerPattern.Pattern, out var pattern))
            {
                result = (ItemContainerPattern)pattern;
                return true;
            }

            result = null;
            return false;
        }

    }
}