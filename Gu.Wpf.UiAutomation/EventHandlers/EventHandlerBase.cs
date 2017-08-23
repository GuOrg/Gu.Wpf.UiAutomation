namespace Gu.Wpf.UiAutomation
{
    public abstract class EventHandlerBase
    {
        public AutomationBase Automation { get; private set; }

        protected EventHandlerBase(AutomationBase automation)
        {
            this.Automation = automation;
        }
    }
}
