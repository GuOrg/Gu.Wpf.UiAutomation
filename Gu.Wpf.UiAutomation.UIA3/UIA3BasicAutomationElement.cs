namespace Gu.Wpf.UiAutomation.UIA3
{
    using System;
    using System.Linq;
    using System.Windows;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.EventHandlers;
    using Gu.Wpf.UiAutomation.UIA3.Extensions;
    using UIA = Interop.UIAutomationClient;

    public class UIA3BasicAutomationElement : BasicAutomationElementBase
    {
        public UIA3BasicAutomationElement(UIA3Automation automation, UIA.IUIAutomationElement nativeElement)
            : base(automation)
        {
            this.Automation = automation;
            this.NativeElement = nativeElement;
            this.Patterns = new UIA3AutomationElementPatternValues(this);
        }

        public override AutomationElementPatternValuesBase Patterns { get; }

        /// <summary>
        /// Concrete implementation of the automation object
        /// </summary>
        public new UIA3Automation Automation { get; }

        /// <summary>
        /// Native object for the ui element
        /// </summary>
        public UIA.IUIAutomationElement NativeElement { get; }

        /// <summary>
        /// Native object for Windows 8 ui element
        /// </summary>
        public UIA.IUIAutomationElement NativeElement2 => this.GetAutomationElementAs<UIA.IUIAutomationElement2>();

        /// <summary>
        /// Native object for Windows 8.1 ui element
        /// </summary>
        public UIA.IUIAutomationElement NativeElement3 => this.GetAutomationElementAs<UIA.IUIAutomationElement3>();

        public override void SetFocus()
        {
            this.NativeElement.SetFocus();
        }

        protected override object InternalGetPropertyValue(int propertyId, bool cached, bool useDefaultIfNotSupported)
        {
            var ignoreDefaultValue = useDefaultIfNotSupported ? 0 : 1;
            var returnValue = cached ?
                this.NativeElement.GetCachedPropertyValueEx(propertyId, ignoreDefaultValue) :
                this.NativeElement.GetCurrentPropertyValueEx(propertyId, ignoreDefaultValue);
            return returnValue;
        }

        protected override object InternalGetPattern(int patternId, bool cached)
        {
            var returnedValue = cached
                ? this.NativeElement.GetCachedPattern(patternId)
                : this.NativeElement.GetCurrentPattern(patternId);
            return returnedValue;
        }

        public override AutomationElement[] FindAll(TreeScope treeScope, ConditionBase condition)
        {
            var nativeFoundElements = CacheRequest.IsCachingActive
                ? this.NativeElement.FindAllBuildCache((UIA.TreeScope)treeScope, ConditionConverter.ToNative(this.Automation, condition), CacheRequest.Current.ToNative(this.Automation))
                : this.NativeElement.FindAll((UIA.TreeScope)treeScope, ConditionConverter.ToNative(this.Automation, condition));
            return AutomationElementConverter.NativeArrayToManaged(this.Automation, nativeFoundElements);
        }

        public override AutomationElement FindFirst(TreeScope treeScope, ConditionBase condition)
        {
            var nativeFoundElement = CacheRequest.IsCachingActive
                ? this.NativeElement.FindFirstBuildCache((UIA.TreeScope)treeScope, ConditionConverter.ToNative(this.Automation, condition), CacheRequest.Current.ToNative(this.Automation))
                : this.NativeElement.FindFirst((UIA.TreeScope)treeScope, ConditionConverter.ToNative(this.Automation, condition));
            return AutomationElementConverter.NativeToManaged(this.Automation, nativeFoundElement);
        }

        public override bool TryGetClickablePoint(out Point point)
        {
            var tagPoint = new UIA.tagPOINT { x = 0, y = 0 };
            var success = ComCallWrapper.Call(() => this.NativeElement.GetClickablePoint(out tagPoint)) != 0;
            if (success)
            {
                point = new Point(tagPoint.x, tagPoint.y);
            }
            else
            {
                success = this.Properties.ClickablePoint.TryGetValue(out point);
            }

            return success;
        }

        public override IAutomationEventHandler RegisterEvent(EventId @event, TreeScope treeScope, Action<AutomationElement, EventId> action)
        {
            var eventHandler = new UIA3BasicEventHandler(this.Automation, action);
            this.Automation.NativeAutomation.AddAutomationEventHandler(@event.Id, this.NativeElement, (UIA.TreeScope)treeScope, null, eventHandler);
            return eventHandler;
        }

        public override IAutomationPropertyChangedEventHandler RegisterPropertyChangedEvent(TreeScope treeScope, Action<AutomationElement, PropertyId, object> action, PropertyId[] properties)
        {
            var eventHandler = new UIA3PropertyChangedEventHandler(this.Automation, action);
            var propertyIds = properties.Select(p => p.Id).ToArray();
            this.Automation.NativeAutomation.AddPropertyChangedEventHandler(
                this.NativeElement,
                (UIA.TreeScope)treeScope,
                null,
                eventHandler,
                propertyIds);
            return eventHandler;
        }

        public override IAutomationStructureChangedEventHandler RegisterStructureChangedEvent(TreeScope treeScope, Action<AutomationElement, StructureChangeType, int[]> action)
        {
            var eventHandler = new UIA3StructureChangedEventHandler(this.Automation, action);
            this.Automation.NativeAutomation.AddStructureChangedEventHandler(this.NativeElement, (UIA.TreeScope)treeScope, null, eventHandler);
            return eventHandler;
        }

        public override void RemoveAutomationEventHandler(EventId @event, IAutomationEventHandler eventHandler)
        {
            this.Automation.NativeAutomation.RemoveAutomationEventHandler(@event.Id, this.NativeElement, (UIA3BasicEventHandler)eventHandler);
        }

        public override void RemovePropertyChangedEventHandler(IAutomationPropertyChangedEventHandler eventHandler)
        {
            this.Automation.NativeAutomation.RemovePropertyChangedEventHandler(this.NativeElement, (UIA3PropertyChangedEventHandler)eventHandler);
        }

        public override void RemoveStructureChangedEventHandler(IAutomationStructureChangedEventHandler eventHandler)
        {
            this.Automation.NativeAutomation.RemoveStructureChangedEventHandler(this.NativeElement, (UIA3StructureChangedEventHandler)eventHandler);
        }

        public override PatternId[] GetSupportedPatterns()
        {
            this.Automation.NativeAutomation.PollForPotentialSupportedPatterns(this.NativeElement, out int[] rawIds, out string[] rawPatternNames);
            return rawIds.Select(id => PatternId.Find(id)).ToArray();
        }

        public override PropertyId[] GetSupportedProperties()
        {
            this.Automation.NativeAutomation.PollForPotentialSupportedProperties(this.NativeElement, out int[] rawIds, out string[] rawPatternNames);
            return rawIds.Select(id => PropertyId.Find(id)).ToArray();
        }

        public override AutomationElement GetUpdatedCache()
        {
            if (CacheRequest.Current != null)
            {
                var updatedElement = this.NativeElement.BuildUpdatedCache(CacheRequest.Current.ToNative(this.Automation));
                return AutomationElementConverter.NativeToManaged(this.Automation, updatedElement);
            }

            return null;
        }

        public override AutomationElement[] GetCachedChildren()
        {
            var cachedChildren = this.NativeElement.GetCachedChildren();
            return AutomationElementConverter.NativeArrayToManaged(this.Automation, cachedChildren);
        }

        public override AutomationElement GetCachedParent()
        {
            var cachedParent = this.NativeElement.GetCachedParent();
            return AutomationElementConverter.NativeToManaged(this.Automation, cachedParent);
        }

        public override int GetHashCode()
        {
            return this.NativeElement.GetHashCode();
        }

        /// <summary>
        /// Tries to cast the automation element to a specific interface.
        /// Throws an exception if that is not possible.
        /// </summary>
        private T GetAutomationElementAs<T>()
            where T : class, UIA.IUIAutomationElement
        {
            var element = this.NativeElement as T;
            if (element == null)
            {
                throw new NotSupportedException($"OS does not have {typeof(T).Name} support.");
            }

            return element;
        }
    }
}
