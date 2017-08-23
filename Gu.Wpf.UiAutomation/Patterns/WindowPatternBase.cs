namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

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

        /// <inheritdoc/>
        public IWindowPatternProperties Properties => this.Automation.PropertyLibrary.Window;

        /// <inheritdoc/>
        public IWindowPatternEvents Events => this.Automation.EventLibrary.Window;

        /// <inheritdoc/>
        public AutomationProperty<bool> CanMaximize => this.GetOrCreate(ref this.canMaximize, this.Properties.CanMaximize);

        /// <inheritdoc/>
        public AutomationProperty<bool> CanMinimize => this.GetOrCreate(ref this.canMinimize, this.Properties.CanMinimize);

        /// <inheritdoc/>
        public AutomationProperty<bool> IsModal => this.GetOrCreate(ref this.isModal, this.Properties.IsModal);

        /// <inheritdoc/>
        public AutomationProperty<bool> IsTopmost => this.GetOrCreate(ref this.isTopmost, this.Properties.IsTopmost);

        /// <inheritdoc/>
        public AutomationProperty<WindowInteractionState> WindowInteractionState => this.GetOrCreate(ref this.windowInteractionState, this.Properties.WindowInteractionState);

        /// <inheritdoc/>
        public AutomationProperty<WindowVisualState> WindowVisualState => this.GetOrCreate(ref this.windowVisualState, this.Properties.WindowVisualState);

        /// <inheritdoc/>
        public abstract void Close();

        /// <inheritdoc/>
        public abstract void SetWindowVisualState(WindowVisualState state);

        /// <inheritdoc/>
        public abstract bool WaitForInputIdle(int milliseconds);
    }
}
