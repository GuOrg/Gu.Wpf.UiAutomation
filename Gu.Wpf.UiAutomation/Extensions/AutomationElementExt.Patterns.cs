namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static DockPattern DockPattern(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetDockPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support DockPattern");
        }

        public static bool TryGetDockPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out DockPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetExpandCollapsePattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support ExpandCollapsePattern");
        }

        public static bool TryGetExpandCollapsePattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out ExpandCollapsePattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetGridItemPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support GridItemPattern");
        }

        public static bool TryGetGridItemPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out GridItemPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetGridPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support GridPattern");
        }

        public static bool TryGetGridPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out GridPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetInvokePattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support InvokePattern");
        }

        public static bool TryGetInvokePattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out InvokePattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetMultipleViewPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support MultipleViewPattern");
        }

        public static bool TryGetMultipleViewPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out MultipleViewPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetRangeValuePattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support RangeValuePattern");
        }

        public static bool TryGetRangeValuePattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out RangeValuePattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetScrollPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support ScrollPattern");
        }

        public static bool TryGetScrollPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out ScrollPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetScrollItemPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support ScrollItemPattern");
        }

        public static bool TryGetScrollItemPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out ScrollItemPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetSelectionPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support SelectionPattern");
        }

        public static bool TryGetSelectionPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out SelectionPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetSelectionItemPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support SelectionItemPattern");
        }

        public static bool TryGetSelectionItemPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out SelectionItemPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetSynchronizedInputPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support SynchronizedInputPattern");
        }

        public static bool TryGetSynchronizedInputPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out SynchronizedInputPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetTableItemPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support TableItemPattern");
        }

        public static bool TryGetTableItemPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out TableItemPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetTablePattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support TablePattern");
        }

        public static bool TryGetTablePattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out TablePattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support TextPattern");
        }

        public static bool TryGetTextPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out TextPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetTransformPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support TransformPattern");
        }

        public static bool TryGetTransformPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out TransformPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support TogglePattern");
        }

        public static bool TryGetTogglePattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out TogglePattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetValuePattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support ValuePattern");
        }

        public static bool TryGetValuePattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out ValuePattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetWindowPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support WindowPattern");
        }

        public static bool TryGetWindowPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out WindowPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetVirtualizedItemPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support VirtualizedItemPattern");
        }

        public static bool TryGetVirtualizedItemPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out VirtualizedItemPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            if (TryGetItemContainerPattern(element, out var pattern))
            {
                return pattern!;
            }

            throw new System.NotSupportedException($"The element {element} does not support ItemContainerPattern");
        }

        public static bool TryGetItemContainerPattern(this AutomationElement element, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out ItemContainerPattern? result)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

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
