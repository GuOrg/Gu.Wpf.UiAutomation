namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class DropTargetPatternProperties : IDropTargetPatternProperties
    {
        public PropertyId DropTargetEffect => DropTargetPattern.DropTargetEffectProperty;

        public PropertyId DropTargetEffects => DropTargetPattern.DropTargetEffectsProperty;
    }
}