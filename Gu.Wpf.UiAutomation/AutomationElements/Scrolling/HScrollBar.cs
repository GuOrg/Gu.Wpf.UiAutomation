namespace Gu.Wpf.UiAutomation.AutomationElements.Scrolling
{
    using System;

    public class HScrollBar : ScrollBarBase
    {
        public HScrollBar(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

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
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

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
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

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
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

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
                        throw new ArgumentOutOfRangeException();
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
