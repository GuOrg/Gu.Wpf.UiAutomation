namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public class VerticalScrollBar : ScrollBar
    {
        public VerticalScrollBar(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <inheritdoc/>
        protected override string SmallDecrementText => this.FrameworkType switch
        {
            FrameworkType.Wpf => "PART_LineUpButton",
            FrameworkType.WinForms or FrameworkType.Win32 => "UpButton",
            _ => throw new InvalidOperationException($"Did not find {nameof(this.SmallDecrementText)} for framework type: {this.FrameworkType}"),
        };

        /// <inheritdoc/>
        protected override string SmallIncrementText => this.FrameworkType switch
        {
            FrameworkType.Wpf => "PART_LineDownButton",
            FrameworkType.WinForms or FrameworkType.Win32 => "DownButton",
            _ => throw new InvalidOperationException($"Did not find {nameof(this.SmallIncrementText)} for framework type: {this.FrameworkType}"),
        };

        /// <inheritdoc/>
        protected override string LargeDecrementText => this.FrameworkType switch
        {
            FrameworkType.Wpf => "PageUp",
            FrameworkType.WinForms or FrameworkType.Win32 => "DownPageButton",
            _ => throw new InvalidOperationException($"Did not find {nameof(this.LargeDecrementText)} for framework type: {this.FrameworkType}"),
        };

        /// <inheritdoc/>
        protected override string LargeIncrementText => this.FrameworkType switch
        {
            FrameworkType.Wpf => "PageDown",
            FrameworkType.WinForms or FrameworkType.Win32 => "UpPageButton",
            _ => throw new InvalidOperationException($"Did not find {nameof(this.LargeIncrementText)} for framework type: {this.FrameworkType}"),
        };

        public void ScrollUp()
        {
            this.SmallDecrementButton.Invoke();
        }

        public void ScrollDown()
        {
            this.SmallIncrementButton.Invoke();
        }

        public void ScrollUpLarge()
        {
            this.LargeDecrementButton.Invoke();
        }

        public void ScrollDownLarge()
        {
            this.LargeIncrementButton.Invoke();
        }
    }
}
