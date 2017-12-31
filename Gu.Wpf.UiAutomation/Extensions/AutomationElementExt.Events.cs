namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static IDisposable SubscribeToEvent(this AutomationElement element, AutomationEvent automationEvent, TreeScope treeScope, AutomationEventHandler handler)
        {
            return Subscribe.ToEvent(element, automationEvent, treeScope, handler);
        }

        public static IDisposable SubscribeToStructureChangedEvent(this AutomationElement element, TreeScope treeScope, StructureChangedEventHandler handler)
        {
            return Subscribe.ToStructureChangedEvent(element, treeScope, handler);
        }

        public static IDisposable SubscribeToFocusChangedEvent(AutomationFocusChangedEventHandler handler)
        {
            return Subscribe.ToFocusChangedEvent(handler);
        }

        public static IDisposable SubscribeToPropertyChangedEvent(this AutomationElement element, TreeScope treeScope, AutomationPropertyChangedEventHandler handler, params AutomationProperty[] properties)
        {
            Automation.AddAutomationPropertyChangedEventHandler(element, treeScope, handler, properties);
            return Disposable.Create(() => Automation.RemoveAutomationPropertyChangedEventHandler(element, handler));
        }

        public static IDisposable SubscribeToPropertyChangedEvent(this AutomationElement element, TreeScope treeScope, AutomationProperty property, AutomationPropertyChangedEventHandler handler)
        {
            return Subscribe.ToPropertyChangedEvent(
                element,
                treeScope,
                property,
                handler);
        }
    }
}