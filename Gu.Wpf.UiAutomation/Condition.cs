namespace Gu.Wpf.UiAutomation
{
    using System.Linq;
    using System.Windows.Automation;

    public static class Condition
    {
        public static System.Windows.Automation.Condition TrueCondition { get; } = System.Windows.Automation.Condition.TrueCondition;

        public static PropertyCondition Button { get; } = ByControlType(ControlType.Button);

        public static PropertyCondition Calendar { get; } = ByControlType(ControlType.Calendar);

        public static PropertyCondition CheckBox { get; } = ByControlType(ControlType.CheckBox);

        public static PropertyCondition ComboBox { get; } = ByControlType(ControlType.ComboBox);

        public static PropertyCondition ControlTypeCustom { get; } = ByControlType(ControlType.Custom);

        public static PropertyCondition DataGrid { get; } = ByControlType(ControlType.DataGrid);

        public static PropertyCondition DataItem { get; } = ByControlType(ControlType.DataItem);

        public static PropertyCondition ControlTypeDocument { get; } = ByControlType(ControlType.Document);

        public static PropertyCondition TextBox { get; } = ByControlType(ControlType.Edit);

        public static PropertyCondition ControlTypeGroup { get; } = ByControlType(ControlType.Group);

        public static PropertyCondition Header { get; } = ByControlType(ControlType.Header);

        public static PropertyCondition HeaderItem { get; } = ByControlType(ControlType.HeaderItem);

        public static PropertyCondition Hyperlink { get; } = ByControlType(ControlType.Hyperlink);

        public static PropertyCondition Image { get; } = ByControlType(ControlType.Image);

        public static System.Windows.Automation.Condition Label { get; } = new AndCondition(ByControlType(ControlType.Text), ByClassName("Text"));

        public static PropertyCondition List { get; } = ByControlType(ControlType.List);

        public static PropertyCondition ListItem { get; } = ByControlType(ControlType.ListItem);

        public static PropertyCondition Menu { get; } = ByControlType(ControlType.Menu);

        public static PropertyCondition ControlTypeMenuBar { get; } = ByControlType(ControlType.MenuBar);

        public static PropertyCondition MenuItem { get; } = ByControlType(ControlType.MenuItem);

        public static PropertyCondition Pane { get; } = ByControlType(ControlType.Pane);

        public static PropertyCondition ProgressBar { get; } = ByControlType(ControlType.ProgressBar);

        public static PropertyCondition RadioButton { get; } = ByControlType(ControlType.RadioButton);

        public static PropertyCondition ScrollBar { get; } = ByControlType(ControlType.ScrollBar);

        public static PropertyCondition Separator { get; } = ByControlType(ControlType.Separator);

        public static PropertyCondition Slider { get; } = ByControlType(ControlType.Slider);

        public static PropertyCondition Spinner { get; } = ByControlType(ControlType.Spinner);

        public static PropertyCondition SplitButton { get; } = ByControlType(ControlType.SplitButton);

        public static PropertyCondition StatusBar { get; } = ByControlType(ControlType.StatusBar);

        public static PropertyCondition Tab { get; } = ByControlType(ControlType.Tab);

        public static PropertyCondition TabItem { get; } = ByControlType(ControlType.TabItem);

        public static PropertyCondition ControlTypeTable { get; } = ByControlType(ControlType.Table);

        public static System.Windows.Automation.Condition TextBlock { get; } = new AndCondition(ByControlType(ControlType.Text), ByClassName("TextBlock"));

        public static PropertyCondition Thumb { get; } = ByControlType(ControlType.Thumb);

        public static PropertyCondition TitleBar { get; } = ByControlType(ControlType.TitleBar);

        public static PropertyCondition ToolBar { get; } = ByControlType(ControlType.ToolBar);

        public static PropertyCondition ToolTip { get; } = ByControlType(ControlType.ToolTip);

        public static PropertyCondition Tree { get; } = ByControlType(ControlType.Tree);

        public static PropertyCondition TreeItem { get; } = ByControlType(ControlType.TreeItem);

        public static PropertyCondition Window { get; } = ByControlType(ControlType.Window);

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
            if (condition is PropertyCondition propertyCondition)
            {
                if (Equals(propertyCondition.Property, AutomationElementIdentifiers.ControlTypeProperty))
                {
                    return $"ControlType == {ControlType.LookupById((int)propertyCondition.Value).ProgrammaticName.Split('.')[1]}";
                }

                return $"{propertyCondition.Property.ProgrammaticName.Split('.')[1].Replace("Property", string.Empty)} == {propertyCondition.Value}";
            }

            if (condition is AndCondition andCondition)
            {
                return $"({string.Join(" && ", andCondition.GetConditions().Select(x => x.Description()))})";
            }

            if (condition is OrCondition orCondition)
            {
                return $"({string.Join(" || ", orCondition.GetConditions().Select(x => x.Description()))})";
            }

            if (condition is NotCondition notCondition)
            {
                return $"!{notCondition.Condition.Description()}";
            }

            return condition.ToString();
        }
    }
}