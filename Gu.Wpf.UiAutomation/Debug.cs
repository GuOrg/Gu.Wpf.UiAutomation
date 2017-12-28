namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Text;

    public static class Debug
    {
        /// <summary>
        /// Gets the XPath to the element until the desktop or the given root element.
        /// Warning: This is quite a heavy operation
        /// </summary>
        public static string GetXPathToElement(UiElement element, UiElement rootElement = null)
        {
            var treeWalker = element.Automation.TreeWalkerFactory.GetControlViewWalker();
            return GetXPathToElement(element, treeWalker, rootElement);
        }

        public static string Details(UiElement uiElement)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                var cr = new CacheRequest { AutomationElementMode = AutomationElementMode.None };

                // Add the element properties
                cr.Add(uiElement.Automation.PropertyLibrary.Element.AutomationId);
                cr.Add(uiElement.Automation.PropertyLibrary.Element.ControlType);
                cr.Add(uiElement.Automation.PropertyLibrary.Element.Name);
                cr.Add(uiElement.Automation.PropertyLibrary.Element.HelpText);
                cr.Add(uiElement.Automation.PropertyLibrary.Element.BoundingRectangle);
                cr.Add(uiElement.Automation.PropertyLibrary.Element.ClassName);
                cr.Add(uiElement.Automation.PropertyLibrary.Element.IsOffscreen);
                cr.Add(uiElement.Automation.PropertyLibrary.Element.FrameworkId);
                cr.Add(uiElement.Automation.PropertyLibrary.Element.ProcessId);

                // Add the pattern availability properties
                uiElement.Automation.PropertyLibrary.PatternAvailability.AllForCurrentFramework.ToList().ForEach(x => cr.Add(x));
                cr.TreeScope = TreeScope.Subtree;
                cr.TreeFilter = TrueCondition.Default;

                // Activate the cache request
                using (cr.Activate())
                {
                    // Re-find the root element with caching activated
                    uiElement = uiElement.FindFirst(TreeScope.Element, TrueCondition.Default);
                    Details(stringBuilder, uiElement, string.Empty);
                }

                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to dump info: " + ex);
                return string.Empty;
            }
        }

        private static string GetXPathToElement(UiElement element, ITreeWalker treeWalker, UiElement rootElement = null)
        {
            var parent = treeWalker.GetParent(element);
            if (parent == null || (rootElement != null && parent.Equals(rootElement)))
            {
                return string.Empty;
            }

            // Get the index
            var allChildren = parent.FindAllChildren(cf => cf.ByControlType(element.Properties.ControlType.Value));
            var currentItemText = $"{element.Properties.ControlType.Value}";
            if (allChildren.Count > 1)
            {
                // There is more than one matching child, find out the index
                var indexInParent = 1; // Index starts with 1
                foreach (var child in allChildren)
                {
                    if (child.Equals(element))
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
            WriteWithPadding(stringBuilder, "AutomationId: " + uiElement.Properties.AutomationId, displayPadding);
            WriteWithPadding(stringBuilder, "ControlType: " + uiElement.Properties.ControlType, displayPadding);
            WriteWithPadding(stringBuilder, "Name: " + uiElement.Properties.Name, displayPadding);
            WriteWithPadding(stringBuilder, "HelpText: " + uiElement.Properties.HelpText, displayPadding);
            WriteWithPadding(stringBuilder, "Bounding rectangle: " + uiElement.Properties.BoundingRectangle, displayPadding);
            WriteWithPadding(stringBuilder, "ClassName: " + uiElement.Properties.ClassName, displayPadding);
            WriteWithPadding(stringBuilder, "IsOffScreen: " + uiElement.Properties.IsOffscreen, displayPadding);
            WriteWithPadding(stringBuilder, "FrameworkId: " + uiElement.Properties.FrameworkId, displayPadding);
            WriteWithPadding(stringBuilder, "ProcessId: " + uiElement.Properties.ProcessId, displayPadding);
        }

        private static void WritePattern(UiElement uiElement, StringBuilder stringBuilder, string displayPadding)
        {
            var availablePatterns = uiElement.GetSupportedPatterns();
            foreach (var automationPattern in availablePatterns)
            {
                WriteWithPadding(stringBuilder, automationPattern.ToString(), displayPadding);
            }

            stringBuilder.AppendLine();
        }

        private static void WriteWithPadding(StringBuilder stringBuilder, string message, string padding)
        {
            stringBuilder.Append(padding).AppendLine(message);
        }
    }
}
