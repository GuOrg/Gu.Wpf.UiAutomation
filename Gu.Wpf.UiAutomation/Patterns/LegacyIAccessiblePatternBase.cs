namespace Gu.Wpf.UiAutomation.Patterns
{
    using Accessibility;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;
    using Gu.Wpf.UiAutomation.WindowsAPI;

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

        /// <inheritdoc/>
        public ILegacyIAccessiblePatternProperties Properties => this.Automation.PropertyLibrary.LegacyIAccessible;

        /// <inheritdoc/>
        public AutomationProperty<int> ChildId => this.GetOrCreate(ref this.childId, this.Properties.ChildId);

        /// <inheritdoc/>
        public AutomationProperty<string> DefaultAction => this.GetOrCreate(ref this.defaultAction, this.Properties.DefaultAction);

        /// <inheritdoc/>
        public AutomationProperty<string> Description => this.GetOrCreate(ref this.description, this.Properties.Description);

        /// <inheritdoc/>
        public AutomationProperty<string> Help => this.GetOrCreate(ref this.help, this.Properties.Help);

        /// <inheritdoc/>
        public AutomationProperty<string> KeyboardShortcut => this.GetOrCreate(ref this.keyboardShortcut, this.Properties.KeyboardShortcut);

        /// <inheritdoc/>
        public AutomationProperty<string> Name => this.GetOrCreate(ref this.name, this.Properties.Name);

        /// <inheritdoc/>
        public AutomationProperty<AccessibilityRole> Role => this.GetOrCreate(ref this.role, this.Properties.Role);

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement[]> Selection => this.GetOrCreate(ref this.selection, this.Properties.Selection);

        /// <inheritdoc/>
        public AutomationProperty<AccessibilityState> State => this.GetOrCreate(ref this.state, this.Properties.State);

        /// <inheritdoc/>
        public AutomationProperty<string> Value => this.GetOrCreate(ref this.value, this.Properties.Value);

        /// <inheritdoc/>
        public abstract void DoDefaultAction();

        /// <inheritdoc/>
        public abstract IAccessible GetIAccessible();

        /// <inheritdoc/>
        public abstract void Select(int flagsSelect);

        /// <inheritdoc/>
        public abstract void SetValue(string value);
    }
}
