using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
using Gu.Wpf.UiAutomation.Identifiers;
using Gu.Wpf.UiAutomation.Patterns;
using Gu.Wpf.UiAutomation.Tools;
using Gu.Wpf.UiAutomation.UIA3.Converters;
using Gu.Wpf.UiAutomation.UIA3.Identifiers;
using UIA = Interop.UIAutomationClient;

namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class Text2Pattern : TextPattern, IText2Pattern
    {
        public new static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_TextPattern2Id, "Text2", AutomationObjectIds.IsTextPattern2AvailableProperty);

        public Text2Pattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationTextPattern2 nativePattern) : base(basicAutomationElement, nativePattern)
        {
            ExtendedNativePattern = nativePattern;
        }

        public UIA.IUIAutomationTextPattern2 ExtendedNativePattern { get; }

        public ITextRange GetCaretRange(out bool isActive)
        {
            var rawIsActive = 0;
            var nativeTextRange = ComCallWrapper.Call(() => ExtendedNativePattern.GetCaretRange(out rawIsActive));
            isActive = rawIsActive != 0;
            return TextRangeConverter.NativeToManaged((UIA3Automation)BasicAutomationElement.Automation, nativeTextRange);
        }

        public ITextRange RangeFromAnnotation(AutomationElement annotation)
        {
            var nativeInputElement = annotation.ToNative();
            var nativeElement = ComCallWrapper.Call(() => ExtendedNativePattern.RangeFromAnnotation(nativeInputElement));
            return TextRangeConverter.NativeToManaged((UIA3Automation)BasicAutomationElement.Automation, nativeElement);
        }
    }
}
