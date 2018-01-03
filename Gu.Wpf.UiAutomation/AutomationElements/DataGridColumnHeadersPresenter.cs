namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public class DataGridColumnHeadersPresenter : Control
    {
        public DataGridColumnHeadersPresenter(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public IReadOnlyList<DataGridColumnHeader> Headers => this.FindAllChildren(Conditions.DataGridColumnHeader, x => new DataGridColumnHeader(x));

        public ItemContainerPattern ItemContainerPattern => this.AutomationElement.ItemContainerPattern();
    }
}
