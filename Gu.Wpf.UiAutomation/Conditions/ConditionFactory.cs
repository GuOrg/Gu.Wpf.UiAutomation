namespace Gu.Wpf.UiAutomation
{
    /// <summary>
    /// Helper class with some commonly used conditions
    /// </summary>
    public class ConditionFactory
    {
        private readonly IPropertyLibray propertyLibrary;

        public ConditionFactory(IPropertyLibray propertyLibrary)
        {
            this.propertyLibrary = propertyLibrary;
        }

        public PropertyCondition ByAutomationId(string automationId)
        {
            return new PropertyCondition(this.propertyLibrary.Element.AutomationId, automationId);
        }

        public PropertyCondition ByControlType(ControlType controlType)
        {
            return new PropertyCondition(this.propertyLibrary.Element.ControlType, controlType);
        }

        public PropertyCondition ByClassName(string className)
        {
            return new PropertyCondition(this.propertyLibrary.Element.ClassName, className);
        }

        public PropertyCondition ByName(string name)
        {
            return new PropertyCondition(this.propertyLibrary.Element.Name, name);
        }

        public PropertyCondition ByValue(string text)
        {
            return new PropertyCondition(this.propertyLibrary.Value.Value, text);
        }

        public PropertyCondition ByProcessId(int processId)
        {
            return new PropertyCondition(this.propertyLibrary.Element.ProcessId, processId);
        }

        public PropertyCondition ByLocalizedControlType(string localizedControlType)
        {
           return new PropertyCondition(this.propertyLibrary.Element.LocalizedControlType, localizedControlType);
        }

        public PropertyCondition ByHelpTextProperty(string helpText)
        {
           return new PropertyCondition(this.propertyLibrary.Element.HelpText, helpText);
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
