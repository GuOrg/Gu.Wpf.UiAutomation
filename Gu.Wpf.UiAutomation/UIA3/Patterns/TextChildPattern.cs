namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class TextChildPattern : PatternBase<Interop.UIAutomationClient.IUIAutomationTextChildPattern>, ITextChildPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(Interop.UIAutomationClient.UIA_PatternIds.UIA_TextChildPatternId, "TextChild", AutomationObjectIds.IsTextChildPatternAvailableProperty);

        public TextChildPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationTextChildPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public AutomationElement TextContainer
        {
            get
            {
                var nativeElement = ComCallWrapper.Call(() => this.NativePattern.TextContainer);
                return AutomationElementConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeElement);
            }
        }

        public ITextRange TextRange
        {
            get
            {
                var nativeRange = ComCallWrapper.Call(() => this.NativePattern.TextRange);
                return TextRangeConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeRange);
            }
        }
    }
}
