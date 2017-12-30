namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public static class Condition
    {
        public static System.Windows.Automation.Condition TrueCondition => System.Windows.Automation.Condition.TrueCondition;

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
    }
}