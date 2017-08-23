namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;
    using Gu.Wpf.UiAutomation.Shapes;

    public abstract class TextPatternBase<TNativePattern> : PatternBase<TNativePattern>, ITextPattern
    {
        protected TextPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public ITextPatternEvents Events => this.Automation.EventLibrary.Text;

        public abstract ITextRange DocumentRange { get; }

        public abstract SupportedTextSelection SupportedTextSelection { get; }

        public abstract ITextRange[] GetSelection();

        public abstract ITextRange[] GetVisibleRanges();

        public abstract ITextRange RangeFromChild(AutomationElement child);

        public abstract ITextRange RangeFromPoint(Point point);
    }
}
