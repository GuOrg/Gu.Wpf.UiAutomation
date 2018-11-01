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
        protected override string SmallDecrementText
        {
            get
            {
                switch (this.FrameworkType)
                {
                    case FrameworkType.Wpf:
                        return "PART_LineUpButton";
                    case FrameworkType.WinForms:
                    case FrameworkType.Win32:
                        return "UpButton";
                    default:
                        throw new InvalidOperationException($"Did not find {nameof(this.SmallDecrementText)} for framework type: {this.FrameworkType}");
                }
            }
        }

        /// <inheritdoc/>
        protected override string SmallIncrementText
        {
            get
            {
                switch (this.FrameworkType)
                {
                    case FrameworkType.Wpf:
                        return "PART_LineDownButton";
                    case FrameworkType.WinForms:
                    case FrameworkType.Win32:
                        return "DownButton";
                    default:
                        throw new InvalidOperationException($"Did not find {nameof(this.SmallIncrementText)} for framework type: {this.FrameworkType}");
                }
            }
        }

        /// <inheritdoc/>
        protected override string LargeDecrementText
        {
            get
            {
                switch (this.FrameworkType)
                {
                    case FrameworkType.Wpf:
                        return "PageUp";
                    case FrameworkType.WinForms:
                    case FrameworkType.Win32:
                        return "DownPageButton";
                    default:
                        throw new InvalidOperationException($"Did not find {nameof(this.LargeDecrementText)} for framework type: {this.FrameworkType}");
                }
            }
        }

        /// <inheritdoc/>
        protected override string LargeIncrementText
        {
            get
            {
                switch (this.FrameworkType)
                {
                    case FrameworkType.Wpf:
                        return "PageDown";
                    case FrameworkType.WinForms:
                    case FrameworkType.Win32:
                        return "UpPageButton";
                    default:
                        throw new InvalidOperationException($"Did not find {nameof(this.LargeIncrementText)} for framework type: {this.FrameworkType}");
                }
            }
        }

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
