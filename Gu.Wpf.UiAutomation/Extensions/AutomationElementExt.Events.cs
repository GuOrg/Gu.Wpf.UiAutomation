namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static IDisposable SubscribeToEvent(this AutomationElement element, AutomationEvent automationEvent, TreeScope treeScope, Action<UiElement, AutomationEvent> action)
        {
            AutomationEventHandler handler = (sender, args) => action(new UiElement((AutomationElement)sender), args.EventId);
            Automation.AddAutomationEventHandler(automationEvent, element, treeScope, handler);
            return Disposable.Create(() => Automation.RemoveAutomationEventHandler(automationEvent, element, handler));
        }

        public static IDisposable SubscribeToStructureChangedEvent(this AutomationElement element, TreeScope treeScope, Action<UiElement, StructureChangeType, int[]> action)
        {
            StructureChangedEventHandler handler = (sender, args) => action(new UiElement((AutomationElement)sender), args.StructureChangeType, args.GetRuntimeId());
            Automation.AddStructureChangedEventHandler(element, treeScope, handler);
            return Disposable.Create(() => Automation.RemoveStructureChangedEventHandler(element, handler));
        }

        public static IDisposable SubscribeToFocusChangedEvent(Action<UiElement> action)
        {
            AutomationFocusChangedEventHandler handler = (sender, args) => action(new UiElement((AutomationElement)sender));
            Automation.AddAutomationFocusChangedEventHandler(handler);
            return Disposable.Create(() => Automation.RemoveAutomationFocusChangedEventHandler(handler));
        }

        public static IDisposable SubscribeToPropertyChangedEvent(this AutomationElement element, TreeScope treeScope, Action<UiElement, AutomationProperty, object> action, params AutomationProperty[] properties)
        {
            return Disposable.Create(properties.Select(p => element.SubscribeToPropertyChangedEvent(treeScope, p, action)));
        }

        public static IDisposable SubscribeToPropertyChangedEvent(this AutomationElement element, TreeScope treeScope, AutomationProperty property, Action<UiElement, AutomationProperty, object> action)
        {
            AutomationPropertyChangedEventHandler handler = (sender, args) => action(new UiElement((AutomationElement)sender), args.Property, args.NewValue);
            Automation.AddAutomationPropertyChangedEventHandler(element, treeScope, handler);
            return Disposable.Create(() => Automation.RemoveAutomationPropertyChangedEventHandler(element, handler));
        }
    }
}