namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public static class Subscribe
    {
        public static IDisposable ToEvent(AutomationElement element, AutomationEvent automationEvent, TreeScope treeScope, AutomationEventHandler handler)
        {
            Automation.AddAutomationEventHandler(automationEvent, element, treeScope, handler);
            return Disposable.Create(() => Automation.RemoveAutomationEventHandler(automationEvent, element, handler));
        }

        public static IDisposable ToStructureChangedEvent(AutomationElement element, TreeScope treeScope, StructureChangedEventHandler handler)
        {
            Automation.AddStructureChangedEventHandler(element, treeScope, handler);
            return Disposable.Create(() => Automation.RemoveStructureChangedEventHandler(element, handler));
        }

        /// <summary>
        /// Subscribe to value changes.
        /// </summary>
        /// <param name="element">The <see cref="AutomationElement"/>.</param>
        /// <param name="treeScope">The <see cref="TreeScope"/>.</param>
        /// <param name="property">The <see cref="AutomationProperty"/> to subscribe to changes for.</param>
        /// <param name="handler"> The <see cref="AutomationPropertyChangedEventHandler"/>. </param>
        /// <returns>A <see cref="IDisposable"/> that unsubcribes on dispose.</returns>
        public static IDisposable ToPropertyChangedEvent(AutomationElement element, TreeScope treeScope, AutomationProperty property, AutomationPropertyChangedEventHandler handler)
        {
            Automation.AddAutomationPropertyChangedEventHandler(element, treeScope, handler, property);
            return Disposable.Create(() => Automation.RemoveAutomationPropertyChangedEventHandler(element, handler));
        }

        public static IDisposable ToFocusChangedEvent(AutomationFocusChangedEventHandler handler)
        {
            Automation.AddAutomationFocusChangedEventHandler(handler);
            return Disposable.Create(() => Automation.RemoveAutomationFocusChangedEventHandler(handler));
        }
    }
}