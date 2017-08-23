namespace Gu.Wpf.UiAutomation
{
    public abstract class EventHandlerBase
    {
        protected EventHandlerBase(AutomationBase automation)
        {
            this.Automation = automation;
        }

        public AutomationBase Automation { get; }
    }
}
