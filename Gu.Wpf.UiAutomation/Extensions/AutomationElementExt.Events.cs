namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static IDisposable SubscribeToEvent(this AutomationElement element, AutomationEvent automationEvent, TreeScope treeScope, Action<UiElement, AutomationEvent> action)
        {
            return Subscribe.ToEvent(element, automationEvent, treeScope, action);
        }

        public static IDisposable SubscribeToStructureChangedEvent(this AutomationElement element, TreeScope treeScope, Action<UiElement, StructureChangeType, int[]> action)
        {
            return Subscribe.ToStructureChangedEvent(element, treeScope, action);
        }

        public static IDisposable SubscribeToFocusChangedEvent(Action<UiElement> action)
        {
            return Subscribe.ToFocusChangedEvent(action);
        }

        public static IDisposable SubscribeToPropertyChangedEvent(this AutomationElement element, TreeScope treeScope, Action<UiElement, AutomationProperty, object> action, params AutomationProperty[] properties)
        {
            AutomationPropertyChangedEventHandler handler = (sender, args) => action(new UiElement((AutomationElement)sender), args.Property, args.NewValue);
            Automation.AddAutomationPropertyChangedEventHandler(element, treeScope, handler, properties);
            return Disposable.Create(() => Automation.RemoveAutomationPropertyChangedEventHandler(element, handler));
        }

        public static IDisposable SubscribeToPropertyChangedEvent(this AutomationElement element, TreeScope treeScope, AutomationProperty property, Action<UiElement, AutomationProperty, object> action)
        {
            return Subscribe.ToPropertyChangedEvent(element, treeScope, property, action);
        }
    }
}