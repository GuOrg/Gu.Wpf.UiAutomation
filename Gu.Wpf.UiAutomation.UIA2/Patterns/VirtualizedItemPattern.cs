#if !NET35
using Gu.Wpf.UiAutomation.Identifiers;
using Gu.Wpf.UiAutomation.Patterns;
using Gu.Wpf.UiAutomation.Patterns.Infrastructure;
using Gu.Wpf.UiAutomation.UIA2.Identifiers;
using UIA = System.Windows.Automation;

namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    public class VirtualizedItemPattern : PatternBase<UIA.VirtualizedItemPattern>, IVirtualizedItemPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.VirtualizedItemPattern.Pattern.Id, "VirtualizedItem", AutomationObjectIds.IsVirtualizedItemPatternAvailableProperty);

        public VirtualizedItemPattern(BasicAutomationElementBase basicAutomationElement, UIA.VirtualizedItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public void Realize()
        {
            this.NativePattern.Realize();
        }
    }
}
#endif