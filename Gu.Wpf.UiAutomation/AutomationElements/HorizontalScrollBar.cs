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
                return this.FrameworkType switch
                {
                    FrameworkType.Wpf => "PART_LineLeftButton",
                    FrameworkType.WinForms or FrameworkType.Win32 => "UpButton",
                    _ => throw new InvalidOperationException($"Did not find {nameof(this.SmallDecrementText)} for framework type: {this.FrameworkType}"),
                };
            }
        }

        /// <inheritdoc/>
        protected override string SmallIncrementText
        {
            get
            {
                return this.FrameworkType switch
                {
                    FrameworkType.Wpf => "PART_LineRightButton",
                    FrameworkType.WinForms or FrameworkType.Win32 => "DownButton",
                    _ => throw new InvalidOperationException($"Did not find {nameof(this.SmallIncrementText)} for framework type: {this.FrameworkType}"),
                };
            }
        }

        /// <inheritdoc/>
        protected override string LargeDecrementText => this.FrameworkType switch
        {
            FrameworkType.Wpf => "PageLeft",
            FrameworkType.WinForms or FrameworkType.Win32 => "DownPageButton",
            _ => throw new InvalidOperationException($"Did not find {nameof(this.LargeDecrementText)} for framework type: {this.FrameworkType}"),
        };

        /// <inheritdoc/>
        protected override string LargeIncrementText => this.FrameworkType switch
        {
            FrameworkType.Wpf => "PageRight",
            FrameworkType.WinForms or FrameworkType.Win32 => "UpPageButton",
            _ => throw new InvalidOperationException($"Did not find {nameof(this.LargeIncrementText)} for framework type: {this.FrameworkType}"),
        };

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
