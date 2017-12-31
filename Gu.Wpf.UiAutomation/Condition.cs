namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Windows.Automation;

    public static class Condition
    {
        public static System.Windows.Automation.Condition TrueCondition { get; } = System.Windows.Automation.Condition.TrueCondition;

        public static System.Windows.Automation.Condition Button { get; } = ByControlType(ControlType.Button);

        public static System.Windows.Automation.Condition Calendar { get; } = ByControlType(ControlType.Calendar);

        public static System.Windows.Automation.Condition CheckBox { get; } = ByControlType(ControlType.CheckBox);

        public static System.Windows.Automation.Condition ComboBox { get; } = ByControlType(ControlType.ComboBox);

        public static System.Windows.Automation.Condition ControlTypeCustom { get; } = ByControlType(ControlType.Custom);

        public static System.Windows.Automation.Condition DataGrid { get; } = ByControlType(ControlType.DataGrid);

        public static System.Windows.Automation.Condition DataItem { get; } = ByControlType(ControlType.DataItem);

        public static System.Windows.Automation.Condition ControlTypeDocument { get; } = ByControlType(ControlType.Document);

        public static AndCondition Expander { get; } = new AndCondition(ByControlType(ControlType.Group), ByClassName("Expander"));

        public static System.Windows.Automation.Condition GroupBox { get; } = new AndCondition(ByControlType(ControlType.Group), ByClassName("GroupBox"));

        public static System.Windows.Automation.Condition Header { get; } = ByControlType(ControlType.Header);

        public static System.Windows.Automation.Condition HeaderItem { get; } = ByControlType(ControlType.HeaderItem);

        public static System.Windows.Automation.Condition Hyperlink { get; } = ByControlType(ControlType.Hyperlink);

        public static System.Windows.Automation.Condition Image { get; } = ByControlType(ControlType.Image);

        public static System.Windows.Automation.Condition Label { get; } = new AndCondition(
            ByControlType(ControlType.Text),
            new OrCondition(
                ByClassName("Text"),
                ByClassName("Static")));

        public static System.Windows.Automation.Condition ListBox { get; } = ByControlType(ControlType.List);

        public static System.Windows.Automation.Condition ListBoxItem { get; } = ByControlType(ControlType.ListItem);

        public static System.Windows.Automation.Condition Menu { get; } = ByControlType(ControlType.Menu);

        public static System.Windows.Automation.Condition MenuBar { get; } = ByControlType(ControlType.MenuBar);

        public static System.Windows.Automation.Condition MenuItem { get; } = ByControlType(ControlType.MenuItem);

        public static System.Windows.Automation.Condition Pane { get; } = ByControlType(ControlType.Pane);

        public static System.Windows.Automation.Condition ProgressBar { get; } = ByControlType(ControlType.ProgressBar);

        public static System.Windows.Automation.Condition RadioButton { get; } = ByControlType(ControlType.RadioButton);

        public static System.Windows.Automation.Condition ScrollBar { get; } = ByControlType(ControlType.ScrollBar);

        public static System.Windows.Automation.Condition Separator { get; } = ByControlType(ControlType.Separator);

        public static System.Windows.Automation.Condition Slider { get; } = ByControlType(ControlType.Slider);

        public static System.Windows.Automation.Condition Spinner { get; } = ByControlType(ControlType.Spinner);

        public static System.Windows.Automation.Condition SplitButton { get; } = ByControlType(ControlType.SplitButton);

        public static System.Windows.Automation.Condition StatusBar { get; } = ByControlType(ControlType.StatusBar);

        public static System.Windows.Automation.Condition TabControl { get; } = ByControlType(ControlType.Tab);

        public static System.Windows.Automation.Condition TabItem { get; } = ByControlType(ControlType.TabItem);

        public static System.Windows.Automation.Condition ControlTypeTable { get; } = ByControlType(ControlType.Table);

        public static System.Windows.Automation.Condition TextBlock { get; } = new AndCondition(ByControlType(ControlType.Text), ByClassName("TextBlock"));

        public static System.Windows.Automation.Condition TextBox { get; } = ByControlType(ControlType.Edit);

        public static System.Windows.Automation.Condition Thumb { get; } = ByControlType(ControlType.Thumb);

        public static System.Windows.Automation.Condition TitleBar { get; } = ByControlType(ControlType.TitleBar);

        public static System.Windows.Automation.Condition ToolBar { get; } = ByControlType(ControlType.ToolBar);

        public static System.Windows.Automation.Condition ToolTip { get; } = ByControlType(ControlType.ToolTip);

        public static System.Windows.Automation.Condition TreeView { get; } = ByControlType(ControlType.Tree);

        public static System.Windows.Automation.Condition TreeViewItem { get; } = ByControlType(ControlType.TreeItem);

        public static System.Windows.Automation.Condition UserControl { get; } = new AndCondition(
            ByControlType(ControlType.Custom),
            new PropertyCondition(AutomationElement.IsContentElementProperty, true),
            new PropertyCondition(AutomationElement.IsControlElementProperty, true));

        public static System.Windows.Automation.Condition Window { get; } = ByControlType(ControlType.Window);

        public static PropertyCondition ByAutomationId(string automationId)
        {
            return new PropertyCondition(AutomationElementIdentifiers.AutomationIdProperty, automationId);
        }

        public static PropertyCondition ByClassName(string className)
        {
            return new PropertyCondition(AutomationElementIdentifiers.ClassNameProperty, className);
        }

        public static PropertyCondition ByControlType(ControlType controlType)
        {
            return new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, controlType);
        }

        public static PropertyCondition ByLocalizedControlType(string localizedControlType)
        {
            return new PropertyCondition(AutomationElementIdentifiers.LocalizedControlTypeProperty, localizedControlType);
        }

        public static PropertyCondition ByHelpTextProperty(string helpText)
        {
            return new PropertyCondition(AutomationElementIdentifiers.HelpTextProperty, helpText);
        }

        public static PropertyCondition ByName(string name)
        {
            return new PropertyCondition(AutomationElementIdentifiers.NameProperty, name);
        }

        public static OrCondition ByNameOrAutomationId(string key)
        {
            return new OrCondition(
                Condition.ByName(key),
                Condition.ByAutomationId(key));
        }

        public static PropertyCondition ByProcessId(int processId)
        {
            return new PropertyCondition(AutomationElementIdentifiers.ProcessIdProperty, processId);
        }

        public static PropertyCondition ByValue(string value)
        {
            return new PropertyCondition(ValuePatternIdentifiers.ValueProperty, value);
        }

        public static string Description(this System.Windows.Automation.Condition condition)
        {
            switch (condition)
            {
                case PropertyCondition propertyCondition:
                    {
                        if (Equals(propertyCondition.Property, AutomationElementIdentifiers.ControlTypeProperty))
                        {
                            return
                                $"ControlType == {ControlType.LookupById((int)propertyCondition.Value).ProgrammaticName.TrimStart("ControlType.")}";
                        }

                        return $"{propertyCondition.Property.ProgrammaticName.TrimStart("AutomationElementIdentifiers.").TrimEnd("Property")} == {propertyCondition.Value}";
                    }

                case AndCondition andCondition:
                    return $"({string.Join(" && ", andCondition.GetConditions().Select(x => x.Description()))})";
                case OrCondition orCondition:
                    return $"({string.Join(" || ", orCondition.GetConditions().Select(x => x.Description()))})";
                case NotCondition notCondition:
                    return $"!{notCondition.Condition.Description()}";
                case var c when c == System.Windows.Automation.Condition.TrueCondition:
                    return "Condition.TrueCondition";
                case var c when c == System.Windows.Automation.Condition.FalseCondition:
                    return "Condition.FalseCondition";
                default:
                    return condition.ToString();
            }
        }

        internal static bool IsMatch(AutomationElement element, System.Windows.Automation.Condition condition)
        {
            switch (condition)
            {
                case PropertyCondition propertyCondition:
                    {
                        var value = element.GetCurrentPropertyValue(propertyCondition.Property);
                        if (Equals(propertyCondition.Property, AutomationElement.ControlTypeProperty) &&
                            value is ControlType controlType &&
                            propertyCondition.Value is int id)
                        {
                            return controlType.Id == id;
                        }

                        switch (propertyCondition.Flags)
                        {
                            case PropertyConditionFlags.None:
                                return Equals(value, propertyCondition.Value);
                            case PropertyConditionFlags.IgnoreCase:
                                return string.Equals((string)value, (string)propertyCondition.Value, StringComparison.OrdinalIgnoreCase);
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                case AndCondition andCondition:
                    return andCondition.GetConditions().All(c => IsMatch(element, c));
                case OrCondition orCondition:
                    return orCondition.GetConditions().Any(c => IsMatch(element, c));
                case NotCondition notCondition:
                    return !IsMatch(element, notCondition.Condition);
                case var c when c == System.Windows.Automation.Condition.TrueCondition:
                    return true;
                case var c when c == System.Windows.Automation.Condition.FalseCondition:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(condition), condition, "Condition not suported");
            }
        }
    }
}