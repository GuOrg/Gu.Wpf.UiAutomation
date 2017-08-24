namespace Gu.Wpf.UiAutomation
{
    using System.Threading;

    public class Expander : Control
    {
        public Expander(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;

        public bool IsExpanded
        {
            get
            {
                var ecp = this.Patterns.ExpandCollapse.PatternOrDefault;
                return ecp?.ExpandCollapseState.ValueOrDefault == ExpandCollapseState.Expanded;
            }

            set
            {
                if (value)
                {
                    this.Expand();
                }
                else
                {
                    this.Collapse();
                }
            }
        }

        public void Expand()
        {
            if (!this.Properties.IsEnabled.Value ||
                this.IsExpanded)
            {
                return;
            }

            if (this.FrameworkType == FrameworkType.WinForms)
            {
                // WinForms
                var openButton = this.FindButton();
                openButton.Invoke();
            }
            else
            {
                // WPF
                var ecp = this.Patterns.ExpandCollapse.PatternOrDefault;
                if (ecp != null)
                {
                    ecp.Expand();
                    for (int i = 0; i < 5; i++)
                    {
                        if (!this.IsExpanded)
                        {
                            // Wait a bit in case there is an open animation
                            Thread.Sleep(50);
                        }
                    }
                }
            }
        }

        public void Collapse()
        {
            if (!this.Properties.IsEnabled ||
                !this.IsExpanded)
            {
                return;
            }

            if (this.FrameworkType == FrameworkType.WinForms)
            {
                // WinForms
                var openButton = this.FindButton();
                openButton.Invoke();
            }
            else
            {
                // WPF
                var ecp = this.Patterns.ExpandCollapse.PatternOrDefault;
                ecp?.Collapse();
            }

            Wait.UntilResponsive(this);
        }
    }
}