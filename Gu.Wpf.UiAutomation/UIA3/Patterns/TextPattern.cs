namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using System.Windows;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Extensions;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class TextPattern : TextPatternBase<Interop.UIAutomationClient.IUIAutomationTextPattern>
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_TextPatternId, "Text", AutomationObjectIds.IsTextPatternAvailableProperty);
        public static readonly EventId TextChangedEvent = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_Text_TextChangedEventId, "TextChanged");
        public static readonly EventId TextSelectionChangedEvent = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_Text_TextSelectionChangedEventId, "TextSelectionChanged");

        public TextPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationTextPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override ITextRange DocumentRange
        {
            get
            {
                var nativeRange = Com.Call(() => this.NativePattern.DocumentRange);
                return TextRangeConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeRange);
            }
        }

        public override SupportedTextSelection SupportedTextSelection
        {
            get
            {
                var nativeObject = Com.Call(() => this.NativePattern.SupportedTextSelection);
                return (SupportedTextSelection)nativeObject;
            }
        }

        public override ITextRange[] GetSelection()
        {
            var nativeRanges = Com.Call(() => this.NativePattern.GetSelection());
            return TextRangeConverter.NativeArrayToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeRanges);
        }

        public override ITextRange[] GetVisibleRanges()
        {
            var nativeRanges = Com.Call(() => this.NativePattern.GetVisibleRanges());
            return TextRangeConverter.NativeArrayToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeRanges);
        }

        public override ITextRange RangeFromChild(AutomationElement child)
        {
            var nativeChild = child.ToNative();
            var nativeRange = Com.Call(() => this.NativePattern.RangeFromChild(nativeChild));
            return TextRangeConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeRange);
        }

        public override ITextRange RangeFromPoint(Point point)
        {
            var nativeRange = Com.Call(() => this.NativePattern.RangeFromPoint(point.ToTagPoint()));
            return TextRangeConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeRange);
        }
    }
}
