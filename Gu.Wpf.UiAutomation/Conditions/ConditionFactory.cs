namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    /// <summary>
    /// Helper class with some commonly used conditions
    /// </summary>
    public sealed class ConditionFactory
    {
        private ConditionFactory()
        {
        }

        public static ConditionFactory Instance { get; } = new ConditionFactory();

        public PropertyCondition ByAutomationId(string automationId)
        {
            return new PropertyCondition(AutomationElementIdentifiers.AutomationIdProperty, automationId);
        }

        public PropertyCondition ByControlType(ControlType controlType)
        {
            return new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, controlType);
        }

        public PropertyCondition ByClassName(string className)
        {
            return new PropertyCondition(AutomationElementIdentifiers.ClassNameProperty, className);
        }

        public PropertyCondition ByName(string name)
        {
            return new PropertyCondition(AutomationElementIdentifiers.NameProperty, name);
        }

        [Obsolete("Not sure about keeping it as it does return this.ByName(text);")]
        public PropertyCondition ByText(string text)
        {
            return this.ByName(text);
        }

        public PropertyCondition ByValue(string text)
        {
            throw new NotImplementedException();
            // return new PropertyCondition(this.propertyLibrary.Value.Value, text);
        }

        public PropertyCondition ByProcessId(int processId)
        {
            return new PropertyCondition(AutomationElementIdentifiers.ProcessIdProperty, processId);
        }

        public PropertyCondition ByLocalizedControlType(string localizedControlType)
        {
            return new PropertyCondition(AutomationElementIdentifiers.LocalizedControlTypeProperty, localizedControlType);
        }

        public PropertyCondition ByHelpTextProperty(string helpText)
        {
            return new PropertyCondition(AutomationElementIdentifiers.HelpTextProperty, helpText);
        }

        /// <summary>
        /// Searches for a Menu/MenuBar
        /// </summary>
        public OrCondition Menu()
        {
            return new OrCondition(this.ByControlType(ControlType.Menu), this.ByControlType(ControlType.MenuBar));
        }

        /// <summary>
        /// Searches for a DataGrid/List
        /// </summary>
        public OrCondition Grid()
        {
            return new OrCondition(this.ByControlType(ControlType.DataGrid), this.ByControlType(ControlType.List));
        }

        public OrCondition HScrollBar()
        {
            return new OrCondition(this.ByControlType(ControlType.ScrollBar), this.ByName(LocalizedStrings.HorizontalScrollBar));
        }

        public OrCondition VScrollBar()
        {
            return new OrCondition(this.ByControlType(ControlType.ScrollBar), this.ByName(LocalizedStrings.VerticalScrollBar));
        }
    }
}
