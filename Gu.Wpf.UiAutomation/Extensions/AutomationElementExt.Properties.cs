namespace Gu.Wpf.UiAutomation
{
    using System.Globalization;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static string AcceleratorKey(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.AcceleratorKeyProperty);
        }

        public static string AccessKey(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.AccessKeyProperty);
        }

        public static string AutomationId(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.AutomationIdProperty);
        }

        public static System.Windows.Rect BoundingRectangle(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (System.Windows.Rect)element.GetCurrentPropertyValue(AutomationElementIdentifiers.BoundingRectangleProperty);
        }

        public static string ClassName(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.ClassNameProperty);
        }

        public static System.Windows.Point ClickablePoint(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (System.Windows.Point)element.GetCurrentPropertyValue(AutomationElementIdentifiers.ClickablePointProperty);
        }

        public static ControlType ControlType(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (ControlType)element.GetCurrentPropertyValue(AutomationElementIdentifiers.ControlTypeProperty);
        }

        public static CultureInfo Culture(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (CultureInfo)element.GetCurrentPropertyValue(AutomationElementIdentifiers.CultureProperty);
        }

        public static string FrameworkId(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.FrameworkIdProperty);
        }

        public static bool HasKeyboardFocus(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.HasKeyboardFocusProperty);
        }

        public static string HelpText(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.HelpTextProperty);
        }

        public static bool IsContentElement(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsContentElementProperty);
        }

        public static bool IsControlElement(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsControlElementProperty);
        }

        public static bool IsDockPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsDockPatternAvailableProperty);
        }

        public static bool IsEnabled(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsEnabledProperty);
        }

        public static bool IsExpandCollapsePatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsExpandCollapsePatternAvailableProperty);
        }

        public static bool IsGridItemPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsGridItemPatternAvailableProperty);
        }

        public static bool IsGridPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsGridPatternAvailableProperty);
        }

        public static bool IsInvokePatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsInvokePatternAvailableProperty);
        }

        public static bool IsItemContainerPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsItemContainerPatternAvailableProperty);
        }

        public static bool IsKeyboardFocusable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsKeyboardFocusableProperty);
        }

        public static bool IsMultipleViewPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsMultipleViewPatternAvailableProperty);
        }

        public static bool IsOffscreen(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsOffscreenProperty);
        }

        public static bool IsPassword(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsPasswordProperty);
        }

        public static bool IsRangeValuePatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsRangeValuePatternAvailableProperty);
        }

        public static bool IsRequiredForForm(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsRequiredForFormProperty);
        }

        public static bool IsScrollItemPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsScrollItemPatternAvailableProperty);
        }

        public static bool IsScrollPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsScrollPatternAvailableProperty);
        }

        public static bool IsSelectionItemPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsSelectionItemPatternAvailableProperty);
        }

        public static bool IsSelectionPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsSelectionPatternAvailableProperty);
        }

        public static bool IsSynchronizedInputPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsSynchronizedInputPatternAvailableProperty);
        }

        public static bool IsTableItemPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsTableItemPatternAvailableProperty);
        }

        public static bool IsTablePatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsTablePatternAvailableProperty);
        }

        public static bool IsTextPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsTextPatternAvailableProperty);
        }

        public static bool IsTogglePatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsTogglePatternAvailableProperty);
        }

        public static bool IsTransformPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsTransformPatternAvailableProperty);
        }

        public static bool IsValuePatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsValuePatternAvailableProperty);
        }

        public static bool IsWindowPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsWindowPatternAvailableProperty);
        }

        public static bool IsVirtualizedItemPatternAvailable(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (bool)element.GetCurrentPropertyValue(AutomationElementIdentifiers.IsVirtualizedItemPatternAvailableProperty);
        }

        public static string ItemStatus(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.ItemStatusProperty);
        }

        public static string ItemType(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.ItemTypeProperty);
        }

        public static object LabeledBy(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return element.GetCurrentPropertyValue(AutomationElementIdentifiers.LabeledByProperty);
        }

        public static string LocalizedControlType(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty);
        }

        public static string Name(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (string)element.GetCurrentPropertyValue(AutomationElementIdentifiers.NameProperty);
        }

        public static int NativeWindowHandle(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (int)element.GetCurrentPropertyValue(AutomationElementIdentifiers.NativeWindowHandleProperty);
        }

        public static OrientationType Orientation(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (OrientationType)element.GetCurrentPropertyValue(AutomationElementIdentifiers.OrientationProperty);
        }

        public static int ProcessId(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (int)element.GetCurrentPropertyValue(AutomationElementIdentifiers.ProcessIdProperty);
        }

        public static int[] RuntimeId(this AutomationElement element)
        {
            if (element is null)
            {
                throw new System.ArgumentNullException(nameof(element));
            }

            return (int[])element.GetCurrentPropertyValue(AutomationElementIdentifiers.RuntimeIdProperty);
        }
    }
}
