namespace Gu.Wpf.UiAutomation
{
    using System;

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
                return ecp?.ExpandCollapseState.ValueOrDefault() == ExpandCollapseState.Expanded;
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
                ecp?.Expand();
            }
        }

        /// <summary>
        /// Invokes <see cref="Expand()"/>.
        /// Then waits for <paramref name="delay"/>, useful if there is an animation.
        /// </summary>
        /// <param name="delay">The time to wait after the click. Useful if there is an animation for example.</param>
        public void Expand(TimeSpan delay)
        {
            this.Expand();
            Wait.For(delay);
        }

        public void Collapse()
        {
            if (!this.IsEnabled ||
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