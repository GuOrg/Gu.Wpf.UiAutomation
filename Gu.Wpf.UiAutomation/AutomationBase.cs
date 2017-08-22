namespace Gu.Wpf.UiAutomation
{
    using System;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Conditions;
    using Gu.Wpf.UiAutomation.EventHandlers;
    using Gu.Wpf.UiAutomation.Overlay;
    using Gu.Wpf.UiAutomation.Shapes;

    /// <summary>
    /// Base class for the native automation object
    /// </summary>
    public abstract class AutomationBase : IDisposable
    {
        protected AutomationBase(IPropertyLibray propertyLibrary, IEventLibrary eventLibrary, IPatternLibrary patternLibrary)
        {
            PropertyLibrary = propertyLibrary;
            EventLibrary = eventLibrary;
            PatternLibrary = patternLibrary;
            ConditionFactory = new ConditionFactory(propertyLibrary);
            OverlayManager = new WinFormsOverlayManager();
            // Make sure all pattern ids are initialized
            var unused = PatternLibrary.AllForCurrentFramework;
        }

        public IPropertyLibray PropertyLibrary { get; }

        public IEventLibrary EventLibrary { get; }

        public IPatternLibrary PatternLibrary { get; }

        public abstract ITreeWalkerFactory TreeWalkerFactory { get; }

        public ConditionFactory ConditionFactory { get; }

        public IOverlayManager OverlayManager { get; }

        /// <summary>
        /// The automation type of the automation implementation
        /// </summary>
        public abstract AutomationType AutomationType { get; }

        /// <summary>
        /// Object which represents the "Not Supported" value
        /// </summary>
        public abstract object NotSupportedValue { get; }

        /// <summary>
        /// Gets the desktop (root) element
        /// </summary>
        public abstract AutomationElement GetDesktop();

        /// <summary>
        /// Creates an <see cref="AutomationElement" /> from a given point
        /// </summary>
        public abstract AutomationElement FromPoint(Point point);

        /// <summary>
        /// Creates an <see cref="AutomationElement" /> from a given windows handle (HWND)
        /// </summary>
        public abstract AutomationElement FromHandle(IntPtr hwnd);

        public abstract AutomationElement FocusedElement();

        public abstract IAutomationFocusChangedEventHandler RegisterFocusChangedEvent(Action<AutomationElement> action);

        public abstract void UnRegisterFocusChangedEvent(IAutomationFocusChangedEventHandler eventHandler);

        /// <summary>
        /// Removes all registered event handlers
        /// </summary>
        public abstract void UnregisterAllEvents();

        public abstract bool Compare(AutomationElement element1, AutomationElement element2);

        /// <summary>
        /// Cleans up the resources
        /// </summary>
        public void Dispose()
        {
            UnregisterAllEvents();
            OverlayManager.Dispose();
        }
    }
}
