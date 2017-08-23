namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class ExpandCollapsePatternProperties : IExpandCollapsePatternProperties
    {
        public PropertyId ExpandCollapseState => ExpandCollapsePattern.ExpandCollapseStateProperty;
    }
}