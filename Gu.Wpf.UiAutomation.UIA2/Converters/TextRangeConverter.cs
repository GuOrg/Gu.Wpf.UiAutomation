using System.Linq;
using Gu.Wpf.UiAutomation;
using UIA = System.Windows.Automation;

namespace Gu.Wpf.UiAutomation.UIA2.Converters
{
    public static class TextRangeConverter
    {
        public static ITextRange[] NativeArrayToManaged(UIA2Automation automation, UIA.Text.TextPatternRange[] nativeElements)
        {
            if (nativeElements == null)
            {
                return new ITextRange[0];
            }
            return nativeElements.Select(r => (ITextRange)NativeToManaged(automation, r)).ToArray();
        }

        public static UIA2TextRange NativeToManaged(UIA2Automation automation, UIA.Text.TextPatternRange nativeElement)
        {
            return nativeElement == null ? null : new UIA2TextRange(automation, nativeElement);
        }
    }
}
