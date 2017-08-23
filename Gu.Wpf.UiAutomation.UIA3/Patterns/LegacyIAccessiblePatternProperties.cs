namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class LegacyIAccessiblePatternProperties : ILegacyIAccessiblePatternProperties
    {
        public PropertyId ChildId => LegacyIAccessiblePattern.ChildIdProperty;

        public PropertyId DefaultAction => LegacyIAccessiblePattern.DefaultActionProperty;

        public PropertyId Description => LegacyIAccessiblePattern.DescriptionProperty;

        public PropertyId Help => LegacyIAccessiblePattern.HelpProperty;

        public PropertyId KeyboardShortcut => LegacyIAccessiblePattern.KeyboardShortcutProperty;

        public PropertyId Name => LegacyIAccessiblePattern.NameProperty;

        public PropertyId Role => LegacyIAccessiblePattern.RoleProperty;

        public PropertyId Selection => LegacyIAccessiblePattern.SelectionProperty;

        public PropertyId State => LegacyIAccessiblePattern.StateProperty;

        public PropertyId Value => LegacyIAccessiblePattern.ValueProperty;
    }
}