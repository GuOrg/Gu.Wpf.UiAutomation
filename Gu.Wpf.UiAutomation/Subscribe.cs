namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public static class Subscribe
    {
        public static IDisposable ToEvent(AutomationElement element, AutomationEvent automationEvent, TreeScope treeScope, Action<UiElement, AutomationEvent> action)
        {
            AutomationEventHandler handler = (sender, args) => action(new UiElement((AutomationElement)sender), args.EventId);
            Automation.AddAutomationEventHandler(automationEvent, element, treeScope, handler);
            return Disposable.Create(() => Automation.RemoveAutomationEventHandler(automationEvent, element, handler));
        }

        public static IDisposable ToStructureChangedEvent(AutomationElement element, TreeScope treeScope, Action<UiElement, StructureChangeType, int[]> action)
        {
            StructureChangedEventHandler handler = (sender, args) => action(new UiElement((AutomationElement)sender), args.StructureChangeType, args.GetRuntimeId());
            Automation.AddStructureChangedEventHandler(element, treeScope, handler);
            return Disposable.Create(() => Automation.RemoveStructureChangedEventHandler(element, handler));
        }

        public static IDisposable ToPropertyChangedEvent(AutomationElement element, TreeScope treeScope, AutomationProperty property, Action<UiElement, AutomationProperty, object> action)
        {
            AutomationPropertyChangedEventHandler handler = (sender, args) => action(new UiElement((AutomationElement)sender), args.Property, args.NewValue);
            Automation.AddAutomationPropertyChangedEventHandler(element, treeScope, handler, property);
            return Disposable.Create(() => Automation.RemoveAutomationPropertyChangedEventHandler(element, handler));
        }

        public static IDisposable ToFocusChangedEvent(Action<UiElement> action)
        {
            AutomationFocusChangedEventHandler handler = (sender, args) => action(new UiElement((AutomationElement)sender));
            Automation.AddAutomationFocusChangedEventHandler(handler);
            return Disposable.Create(() => Automation.RemoveAutomationFocusChangedEventHandler(handler));
        }
    }
}