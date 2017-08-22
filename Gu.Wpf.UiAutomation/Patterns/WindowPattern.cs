namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IWindowPattern : IPattern
    {
        IWindowPatternProperties Properties { get; }
        IWindowPatternEvents Events { get; }

        AutomationProperty<bool> CanMaximize { get; }
        AutomationProperty<bool> CanMinimize { get; }
        AutomationProperty<bool> IsModal { get; }
        AutomationProperty<bool> IsTopmost { get; }
        AutomationProperty<WindowInteractionState> WindowInteractionState { get; }
        AutomationProperty<WindowVisualState> WindowVisualState { get; }

        void Close();
        void SetWindowVisualState(WindowVisualState state);
        bool WaitForInputIdle(int milliseconds);
    }

    public interface IWindowPatternProperties
    {
        PropertyId CanMaximize { get; }
        PropertyId CanMinimize { get; }
        PropertyId IsModal { get; }
        PropertyId IsTopmost { get; }
        PropertyId WindowInteractionState { get; }
        PropertyId WindowVisualState { get; }
    }

    public interface IWindowPatternEvents
    {
        EventId WindowClosedEvent { get; }
        EventId WindowOpenedEvent { get; }
    }

    public abstract class WindowPatternBase<TNativePattern> : PatternBase<TNativePattern>, IWindowPattern
    {
        private AutomationProperty<bool> canMaximize;
        private AutomationProperty<bool> canMinimize;
        private AutomationProperty<bool> isModal;
        private AutomationProperty<bool> isTopmost;
        private AutomationProperty<WindowInteractionState> windowInteractionState;
        private AutomationProperty<WindowVisualState> windowVisualState;

        protected WindowPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public IWindowPatternProperties Properties => this.Automation.PropertyLibrary.Window;
        public IWindowPatternEvents Events => this.Automation.EventLibrary.Window;

        public AutomationProperty<bool> CanMaximize => this.GetOrCreate(ref this.canMaximize, this.Properties.CanMaximize);
        public AutomationProperty<bool> CanMinimize => this.GetOrCreate(ref this.canMinimize, this.Properties.CanMinimize);
        public AutomationProperty<bool> IsModal => this.GetOrCreate(ref this.isModal, this.Properties.IsModal);
        public AutomationProperty<bool> IsTopmost => this.GetOrCreate(ref this.isTopmost, this.Properties.IsTopmost);
        public AutomationProperty<WindowInteractionState> WindowInteractionState => this.GetOrCreate(ref this.windowInteractionState, this.Properties.WindowInteractionState);
        public AutomationProperty<WindowVisualState> WindowVisualState => this.GetOrCreate(ref this.windowVisualState, this.Properties.WindowVisualState);

        public abstract void Close();
        public abstract void SetWindowVisualState(WindowVisualState state);
        public abstract bool WaitForInputIdle(int milliseconds);
    }
}
