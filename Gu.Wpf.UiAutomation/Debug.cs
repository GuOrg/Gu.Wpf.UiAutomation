namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Text;
    using System.Windows.Automation;

    public static class Debug
    {
        /// <summary>
        /// Gets the XPath to the element until the desktop or the given root element.
        /// Warning: This is quite a heavy operation.
        /// </summary>
        public static string GetXPathToElement(UiElement element, UiElement? rootElement = null)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return GetXPathToElement(element.AutomationElement, TreeWalker.ControlViewWalker, rootElement);
        }

        public static string Details(UiElement uiElement)
        {
            if (uiElement is null)
            {
                throw new ArgumentNullException(nameof(uiElement));
            }

            try
            {
                var stringBuilder = new StringBuilder();
                var cr = new CacheRequest { AutomationElementMode = AutomationElementMode.None };

                // Add the element properties
                cr.Add(AutomationElementIdentifiers.AutomationIdProperty);
                cr.Add(AutomationElementIdentifiers.ControlTypeProperty);
                cr.Add(AutomationElementIdentifiers.NameProperty);
                cr.Add(AutomationElementIdentifiers.HelpTextProperty);
                cr.Add(AutomationElementIdentifiers.BoundingRectangleProperty);
                cr.Add(AutomationElementIdentifiers.ClassNameProperty);
                cr.Add(AutomationElementIdentifiers.IsOffscreenProperty);
                cr.Add(AutomationElementIdentifiers.FrameworkIdProperty);
                cr.Add(AutomationElementIdentifiers.ProcessIdProperty);

                // Add the pattern availability properties
                cr.Add(AutomationElementIdentifiers.IsDockPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsExpandCollapsePatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsGridPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsGridItemPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsInvokePatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsItemContainerPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsMultipleViewPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsRangeValuePatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsScrollItemPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsScrollPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsSelectionItemPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsSynchronizedInputPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsTablePatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsTableItemPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsTextPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsTogglePatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsTransformPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsValuePatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsVirtualizedItemPatternAvailableProperty);
                cr.Add(AutomationElementIdentifiers.IsWindowPatternAvailableProperty);

                cr.TreeScope = TreeScope.Subtree;
                cr.TreeFilter = Condition.TrueCondition;

                // Activate the cache request
                using (cr.Activate())
                {
                    // Re-find the root element with caching activated
                    uiElement = uiElement.FindFirst(TreeScope.Element, Condition.TrueCondition);
                    Details(stringBuilder, uiElement, string.Empty);
                }

                return stringBuilder.ToString();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Console.WriteLine("Failed to dump info: " + ex);
                return string.Empty;
            }
        }

        private static string GetXPathToElement(AutomationElement element, TreeWalker treeWalker, UiElement? rootElement = null)
        {
            var parent = treeWalker.GetParent(element);
            if (parent is null ||
                (rootElement is { } &&
                 Equals(parent, rootElement.AutomationElement)))
            {
                return string.Empty;
            }

            // Get the index
            var allChildren = parent.FindAllChildren(Conditions.ByControlType(element.Current.ControlType));
            var currentItemText = $"{element.Current.ControlType}";
            if (allChildren.Count > 1)
            {
                // There is more than one matching child, find out the index
                var indexInParent = 1; // Index starts with 1
                foreach (var child in allChildren)
                {
                    if (element.Equals(child))
                    {
                        break;
                    }

                    indexInParent++;
                }

                currentItemText += $"[{indexInParent}]";
            }

            return $"{GetXPathToElement(parent, treeWalker, rootElement)}/{currentItemText}";
        }

        private static void Details(StringBuilder stringBuilder, UiElement uiElement, string displayPadding)
        {
            const string indent = "    ";
            WriteDetail(uiElement, stringBuilder, displayPadding);
            WritePattern(uiElement, stringBuilder, displayPadding);
            var children = uiElement.CachedChildren;
            foreach (var child in children)
            {
                Details(stringBuilder, child, displayPadding + indent);
            }
        }

        private static void WriteDetail(UiElement uiElement, StringBuilder stringBuilder, string displayPadding)
        {
            WriteWithPadding(stringBuilder, "AutomationId: " + uiElement.AutomationId, displayPadding);
            WriteWithPadding(stringBuilder, "ControlType: " + uiElement.ControlType, displayPadding);
            WriteWithPadding(stringBuilder, "Name: " + uiElement.Name, displayPadding);
            WriteWithPadding(stringBuilder, "HelpText: " + uiElement.HelpText, displayPadding);
            WriteWithPadding(stringBuilder, "Bounding rectangle: " + uiElement.Bounds, displayPadding);
            WriteWithPadding(stringBuilder, "ClassName: " + uiElement.ClassName, displayPadding);
            WriteWithPadding(stringBuilder, "IsOffScreen: " + uiElement.IsOffscreen, displayPadding);
            WriteWithPadding(stringBuilder, "FrameworkId: " + uiElement.FrameworkType, displayPadding);
            WriteWithPadding(stringBuilder, "ProcessId: " + uiElement.ProcessId, displayPadding);
        }

        private static void WritePattern(UiElement uiElement, StringBuilder stringBuilder, string displayPadding)
        {
            var availablePatterns = uiElement.GetSupportedPatterns();
            foreach (var automationPattern in availablePatterns)
            {
                WriteWithPadding(stringBuilder, automationPattern.ToString()!, displayPadding);
            }

            stringBuilder.AppendLine();
        }

        private static void WriteWithPadding(StringBuilder stringBuilder, string message, string padding)
        {
            stringBuilder.Append(padding).AppendLine(message);
        }
    }
}
