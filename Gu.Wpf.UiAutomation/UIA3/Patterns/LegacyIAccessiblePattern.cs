namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using System;
    using Accessibility;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class LegacyIAccessiblePattern : LegacyIAccessiblePatternBase<Interop.UIAutomationClient.IUIAutomationLegacyIAccessiblePattern>
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_LegacyIAccessiblePatternId, "LegacyIAccessible", AutomationObjectIds.IsLegacyIAccessiblePatternAvailableProperty);
        public static readonly PropertyId ChildIdProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleChildIdPropertyId, "ChildId");
        public static readonly PropertyId DefaultActionProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleDefaultActionPropertyId, "DefaultAction");
        public static readonly PropertyId DescriptionProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleDescriptionPropertyId, "Description");
        public static readonly PropertyId HelpProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleHelpPropertyId, "Help");
        public static readonly PropertyId KeyboardShortcutProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleKeyboardShortcutPropertyId, "KeyboardShortcut");
        public static readonly PropertyId NameProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleNamePropertyId, "Name");
        public static readonly PropertyId RoleProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleRolePropertyId, "Role", (a, o) => (AccessibilityRole)Convert.ToUInt32(o));
        public static readonly PropertyId SelectionProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleSelectionPropertyId, "Selection", AutomationElementConverter.NativeArrayToManaged);
        public static readonly PropertyId StateProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleStatePropertyId, "State", (a, o) => (AccessibilityState)Convert.ToUInt32(o));
        public static readonly PropertyId ValueProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_LegacyIAccessibleValuePropertyId, "Value");

        public LegacyIAccessiblePattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationLegacyIAccessiblePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void DoDefaultAction()
        {
            Com.Call(() => this.NativePattern.DoDefaultAction());
        }

        public override IAccessible GetIAccessible()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return Com.Call(() => (IAccessible)this.NativePattern.GetIAccessible());
        }

        public override void Select(int flagsSelect)
        {
            Com.Call(() => this.NativePattern.Select(flagsSelect));
        }

        public override void SetValue(string value)
        {
            Com.Call(() => this.NativePattern.SetValue(value));
        }
    }
}
