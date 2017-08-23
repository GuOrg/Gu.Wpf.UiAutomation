namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using System;
    using Accessibility;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using UIA = Interop.UIAutomationClient;

    public class LegacyIAccessiblePattern : LegacyIAccessiblePatternBase<UIA.IUIAutomationLegacyIAccessiblePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_LegacyIAccessiblePatternId, "LegacyIAccessible", AutomationObjectIds.IsLegacyIAccessiblePatternAvailableProperty);
        public static readonly PropertyId ChildIdProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleChildIdPropertyId, "ChildId");
        public static readonly PropertyId DefaultActionProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleDefaultActionPropertyId, "DefaultAction");
        public static readonly PropertyId DescriptionProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleDescriptionPropertyId, "Description");
        public static readonly PropertyId HelpProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleHelpPropertyId, "Help");
        public static readonly PropertyId KeyboardShortcutProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleKeyboardShortcutPropertyId, "KeyboardShortcut");
        public static readonly PropertyId NameProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleNamePropertyId, "Name");
        public static readonly PropertyId RoleProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleRolePropertyId, "Role").SetConverter((a, o) => (AccessibilityRole)Convert.ToUInt32(o));
        public static readonly PropertyId SelectionProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleSelectionPropertyId, "Selection").SetConverter(AutomationElementConverter.NativeArrayToManaged);
        public static readonly PropertyId StateProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleStatePropertyId, "State").SetConverter((a, o) => (AccessibilityState)Convert.ToUInt32(o));
        public static readonly PropertyId ValueProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LegacyIAccessibleValuePropertyId, "Value");

        public LegacyIAccessiblePattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationLegacyIAccessiblePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void DoDefaultAction()
        {
            ComCallWrapper.Call(() => this.NativePattern.DoDefaultAction());
        }

        public override IAccessible GetIAccessible()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return ComCallWrapper.Call(() => (IAccessible)this.NativePattern.GetIAccessible());
        }

        public override void Select(int flagsSelect)
        {
            ComCallWrapper.Call(() => this.NativePattern.Select(flagsSelect));
        }

        public override void SetValue(string value)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetValue(value));
        }
    }
}
