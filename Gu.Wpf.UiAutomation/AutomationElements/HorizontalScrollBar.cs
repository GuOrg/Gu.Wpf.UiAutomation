namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public class HorizontalScrollBar : ScrollBar
    {
        public HorizontalScrollBar(AutomationElement automationElement)
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
                        return "PART_LineLeftButton";
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
                        return "PART_LineRightButton";
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
                        return "PageLeft";
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
                        return "PageRight";
                    case FrameworkType.WinForms:
                    case FrameworkType.Win32:
                        return "UpPageButton";
                    default:
                        throw new InvalidOperationException($"Did not find {nameof(this.LargeIncrementText)} for framework type: {this.FrameworkType}");
                }
            }
        }

        public virtual void ScrollLeft()
        {
            this.SmallDecrementButton.Click();
        }

        public virtual void ScrollRight()
        {
            this.SmallIncrementButton.Click();
        }

        public virtual void ScrollLeftLarge()
        {
            this.LargeDecrementButton.Click();
        }

        public virtual void ScrollRightLarge()
        {
            this.LargeIncrementButton.Click();
        }
    }
}
