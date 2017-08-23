namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class MultipleViewPatternBase<TNativePattern> : PatternBase<TNativePattern>, IMultipleViewPattern
    {
        private AutomationProperty<int> currentView;
        private AutomationProperty<int[]> supportedViews;

        protected MultipleViewPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public IMultipleViewPatternProperties Properties => this.Automation.PropertyLibrary.MultipleView;

        /// <inheritdoc/>
        public AutomationProperty<int> CurrentView => this.GetOrCreate(ref this.currentView, this.Properties.CurrentView);

        /// <inheritdoc/>
        public AutomationProperty<int[]> SupportedViews => this.GetOrCreate(ref this.supportedViews, this.Properties.SupportedViews);

        /// <inheritdoc/>
        public abstract string GetViewName(int view);

        /// <inheritdoc/>
        public abstract void SetCurrentView(int view);
    }
}
