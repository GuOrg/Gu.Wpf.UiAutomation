namespace Gu.Wpf.UiAutomation
{
    using Accessibility;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public interface ILegacyIAccessiblePattern : IPattern
    {
        ILegacyIAccessiblePatternProperties Properties { get; }

        AutomationProperty<int> ChildId { get; }

        AutomationProperty<string> DefaultAction { get; }

        AutomationProperty<string> Description { get; }

        AutomationProperty<string> Help { get; }

        AutomationProperty<string> KeyboardShortcut { get; }

        AutomationProperty<string> Name { get; }

        AutomationProperty<AccessibilityRole> Role { get; }

        AutomationProperty<AutomationElement[]> Selection { get; }

        AutomationProperty<AccessibilityState> State { get; }

        AutomationProperty<string> Value { get; }

        void DoDefaultAction();

        IAccessible GetIAccessible();

        void Select(int flagsSelect);

        void SetValue(string value);
    }
}