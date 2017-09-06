namespace Gu.Wpf.UiAutomation.UIA3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.EventHandlers;
    using Gu.Wpf.UiAutomation.UIA3.Extensions;
    using Interop.UIAutomationClient;
    using StructureChangeType = Gu.Wpf.UiAutomation.StructureChangeType;
    using TreeScope = Gu.Wpf.UiAutomation.TreeScope;

    public class UIA3BasicAutomationElement : BasicAutomationElementBase
    {
        public UIA3BasicAutomationElement(UIA3Automation automation, Interop.UIAutomationClient.IUIAutomationElement nativeElement)
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
        public Interop.UIAutomationClient.IUIAutomationElement NativeElement { get; }

        /// <summary>
        /// Native object for Windows 8 ui element
        /// </summary>
        public Interop.UIAutomationClient.IUIAutomationElement NativeElement2 => this.GetAutomationElementAs<Interop.UIAutomationClient.IUIAutomationElement2>();

        /// <summary>
        /// Native object for Windows 8.1 ui element
        /// </summary>
        public Interop.UIAutomationClient.IUIAutomationElement NativeElement3 => this.GetAutomationElementAs<Interop.UIAutomationClient.IUIAutomationElement3>();

        public override void SetFocus()
        {
            this.NativeElement.SetFocus();
        }

        public override IReadOnlyList<AutomationElement> FindAll(TreeScope treeScope, ConditionBase condition)
        {
            var nativeFoundElements = CacheRequest.IsCachingActive
                ? this.NativeElement.FindAllBuildCache((Interop.UIAutomationClient.TreeScope)treeScope, condition.ToNative(this.Automation.NativeAutomation), CacheRequest.Current.ToNative(this.Automation))
                : this.NativeElement.FindAll((Interop.UIAutomationClient.TreeScope)treeScope, condition.ToNative(this.Automation.NativeAutomation));
            return AutomationElementConverter.NativeArrayToManaged(this.Automation, nativeFoundElements);
        }

        public override AutomationElement FindFirst(TreeScope treeScope, ConditionBase condition)
        {
            var nativeElement = this.FindFirstNative(
                treeScope,
                condition.ToNative(this.Automation.NativeAutomation));
            return AutomationElementConverter.NativeToManaged(this.Automation, nativeElement);
        }

        public override T FindFirst<T>(TreeScope treeScope, ConditionBase condition, Func<BasicAutomationElementBase, T> wrap)
        {
            var nativeElement = this.FindFirstNative(
                treeScope,
                condition.ToNative(this.Automation.NativeAutomation));
            if (nativeElement == null)
            {
                return null;
            }

            var basicElement = new UIA3BasicAutomationElement(this.Automation, nativeElement);
            return wrap(basicElement);
        }

        public IUIAutomationElement FindFirstNative(TreeScope treeScope, IUIAutomationCondition condition)
        {
            return CacheRequest.IsCachingActive
                ? this.NativeElement.FindFirstBuildCache((Interop.UIAutomationClient.TreeScope)treeScope, condition, CacheRequest.Current.ToNative(this.Automation))
                : this.NativeElement.FindFirst((Interop.UIAutomationClient.TreeScope)treeScope, condition);
        }

        public override bool TryGetClickablePoint(out Point point)
        {
            var tagPoint = new Interop.UIAutomationClient.tagPOINT { x = 0, y = 0 };
            var success = Com.Call(() => this.NativeElement.GetClickablePoint(out tagPoint)) != 0;
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
            this.Automation.NativeAutomation.AddAutomationEventHandler(@event.Id, this.NativeElement, (Interop.UIAutomationClient.TreeScope)treeScope, null, eventHandler);
            return eventHandler;
        }

        public override IAutomationPropertyChangedEventHandler RegisterPropertyChangedEvent(TreeScope treeScope, Action<AutomationElement, PropertyId, object> action, PropertyId[] properties)
        {
            var eventHandler = new UIA3PropertyChangedEventHandler(this.Automation, action);
            var propertyIds = properties.Select(p => p.Id).ToArray();
            this.Automation.NativeAutomation.AddPropertyChangedEventHandler(
                this.NativeElement,
                (Interop.UIAutomationClient.TreeScope)treeScope,
                null,
                eventHandler,
                propertyIds);
            return eventHandler;
        }

        public override IAutomationStructureChangedEventHandler RegisterStructureChangedEvent(TreeScope treeScope, Action<AutomationElement, StructureChangeType, int[]> action)
        {
            var eventHandler = new UIA3StructureChangedEventHandler(this.Automation, action);
            this.Automation.NativeAutomation.AddStructureChangedEventHandler(this.NativeElement, (Interop.UIAutomationClient.TreeScope)treeScope, null, eventHandler);
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

        public override IReadOnlyList<PatternId> GetSupportedPatterns()
        {
            this.Automation.NativeAutomation.PollForPotentialSupportedPatterns(this.NativeElement, out int[] rawIds, out string[] rawNames);
            var patterns = new List<PatternId>(rawIds.Length);
            for (var i = 0; i < rawIds.Length; i++)
            {
                var id = rawIds[i];
                if (!PatternId.TryGet(id, out var patternId))
                {
                    patternId = PatternId.GetOrCreate(id, rawNames[i] + "_NOT_HANDLED", null);
                }

                patterns.Add(patternId);
            }

            return patterns;
        }

        public override IReadOnlyList<PropertyId> GetSupportedProperties()
        {
            this.Automation.NativeAutomation.PollForPotentialSupportedProperties(this.NativeElement, out int[] rawIds, out string[] rawNames);
            var patterns = new List<PropertyId>(rawIds.Length);
            for (var i = 0; i < rawIds.Length; i++)
            {
                var id = rawIds[i];
                if (!PropertyId.TryGet(id, out var propertyId))
                {
                    propertyId = PropertyId.GetOrCreate(id, rawNames[i] + "_NOT_HANDLED");
                }

                patterns.Add(propertyId);
            }

            return patterns;
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

        public override IReadOnlyList<AutomationElement> GetCachedChildren()
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

        protected override object InternalGetPropertyValue(int propertyId, bool cached, bool useDefaultIfNotSupported)
        {
            var ignoreDefaultValue = useDefaultIfNotSupported ? 0 : 1;
            var result = cached ?
                this.NativeElement.GetCachedPropertyValueEx(propertyId, ignoreDefaultValue) :
                this.NativeElement.GetCurrentPropertyValueEx(propertyId, ignoreDefaultValue);
            return result;
        }

        protected override object InternalGetPattern(int patternId, bool cached)
        {
            var result = cached
                ? this.NativeElement.GetCachedPattern(patternId)
                : this.NativeElement.GetCurrentPattern(patternId);
            return result;
        }

        /// <summary>
        /// Tries to cast the automation element to a specific interface.
        /// Throws an exception if that is not possible.
        /// </summary>
        private T GetAutomationElementAs<T>()
            where T : class, Interop.UIAutomationClient.IUIAutomationElement
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
