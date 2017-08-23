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

        public IMultipleViewPatternProperties Properties => this.Automation.PropertyLibrary.MultipleView;

        public AutomationProperty<int> CurrentView => this.GetOrCreate(ref this.currentView, this.Properties.CurrentView);

        public AutomationProperty<int[]> SupportedViews => this.GetOrCreate(ref this.supportedViews, this.Properties.SupportedViews);

        public abstract string GetViewName(int view);

        public abstract void SetCurrentView(int view);
    }
}
