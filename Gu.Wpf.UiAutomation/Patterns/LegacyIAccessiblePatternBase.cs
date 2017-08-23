namespace Gu.Wpf.UiAutomation.Patterns
{
    using Accessibility;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class LegacyIAccessiblePatternBase<TNativePattern> : PatternBase<TNativePattern>, ILegacyIAccessiblePattern
    {
        private AutomationProperty<int> childId;
        private AutomationProperty<string> defaultAction;
        private AutomationProperty<string> description;
        private AutomationProperty<string> help;
        private AutomationProperty<string> keyboardShortcut;
        private AutomationProperty<string> name;
        private AutomationProperty<AccessibilityRole> role;
        private AutomationProperty<AutomationElement[]> selection;
        private AutomationProperty<AccessibilityState> state;
        private AutomationProperty<string> value;

        protected LegacyIAccessiblePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public ILegacyIAccessiblePatternProperties Properties => this.Automation.PropertyLibrary.LegacyIAccessible;

        public AutomationProperty<int> ChildId => this.GetOrCreate(ref this.childId, this.Properties.ChildId);

        public AutomationProperty<string> DefaultAction => this.GetOrCreate(ref this.defaultAction, this.Properties.DefaultAction);

        public AutomationProperty<string> Description => this.GetOrCreate(ref this.description, this.Properties.Description);

        public AutomationProperty<string> Help => this.GetOrCreate(ref this.help, this.Properties.Help);

        public AutomationProperty<string> KeyboardShortcut => this.GetOrCreate(ref this.keyboardShortcut, this.Properties.KeyboardShortcut);

        public AutomationProperty<string> Name => this.GetOrCreate(ref this.name, this.Properties.Name);

        public AutomationProperty<AccessibilityRole> Role => this.GetOrCreate(ref this.role, this.Properties.Role);

        public AutomationProperty<AutomationElement[]> Selection => this.GetOrCreate(ref this.selection, this.Properties.Selection);

        public AutomationProperty<AccessibilityState> State => this.GetOrCreate(ref this.state, this.Properties.State);

        public AutomationProperty<string> Value => this.GetOrCreate(ref this.value, this.Properties.Value);

        public abstract void DoDefaultAction();

        public abstract IAccessible GetIAccessible();

        public abstract void Select(int flagsSelect);

        public abstract void SetValue(string value);
    }
}
