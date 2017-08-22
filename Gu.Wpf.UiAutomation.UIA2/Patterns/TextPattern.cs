namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Shapes;
    using Gu.Wpf.UiAutomation.UIA2.Converters;
    using Gu.Wpf.UiAutomation.UIA2.Identifiers;
    using UIA = System.Windows.Automation;

    public class TextPattern : TextPatternBase<UIA.TextPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.TextPattern.Pattern.Id, "Text", AutomationObjectIds.IsTextPatternAvailableProperty);
        public static readonly EventId TextChangedEvent = EventId.Register(AutomationType.UIA2, UIA.TextPattern.TextChangedEvent.Id, "TextChanged");
        public static readonly EventId TextSelectionChangedEvent = EventId.Register(AutomationType.UIA2, UIA.TextPattern.TextSelectionChangedEvent.Id, "TextSelectionChanged");

        public TextPattern(BasicAutomationElementBase basicAutomationElement, UIA.TextPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override ITextRange DocumentRange
        {
            get
            {
                var nativeRange = this.NativePattern.DocumentRange;
                return TextRangeConverter.NativeToManaged((UIA2Automation)this.BasicAutomationElement.Automation, nativeRange);
            }
        }

        public override SupportedTextSelection SupportedTextSelection
        {
            get
            {
                var nativeObject = this.NativePattern.SupportedTextSelection;
                return (SupportedTextSelection)nativeObject;
            }
        }

        public override ITextRange[] GetSelection()
        {
            var nativeRanges = this.NativePattern.GetSelection();
            return TextRangeConverter.NativeArrayToManaged((UIA2Automation)this.BasicAutomationElement.Automation, nativeRanges);
        }

        public override ITextRange[] GetVisibleRanges()
        {
            var nativeRanges = this.NativePattern.GetVisibleRanges();
            return TextRangeConverter.NativeArrayToManaged((UIA2Automation)this.BasicAutomationElement.Automation, nativeRanges);
        }

        public override ITextRange RangeFromChild(AutomationElement child)
        {
            var nativeChild = child.ToNative();
            var nativeRange = this.NativePattern.RangeFromChild(nativeChild);
            return TextRangeConverter.NativeToManaged((UIA2Automation)this.BasicAutomationElement.Automation, nativeRange);
        }

        public override ITextRange RangeFromPoint(Point point)
        {
            var nativeRange = this.NativePattern.RangeFromPoint(ValueConverter.ToNative(point));
            return TextRangeConverter.NativeToManaged((UIA2Automation)this.BasicAutomationElement.Automation, nativeRange);
        }
    }

    public class TextPatternEvents : ITextPatternEvents
    {
        public EventId TextChangedEvent => TextPattern.TextChangedEvent;
        public EventId TextSelectionChangedEvent => TextPattern.TextSelectionChangedEvent;
    }
}
