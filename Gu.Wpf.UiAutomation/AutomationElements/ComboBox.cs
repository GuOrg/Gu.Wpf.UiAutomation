namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Windows.Automation;

    /// <summary>
    /// Element which can be used for ComboBox elements.
    /// </summary>
    public class ComboBox : Selector<ComboBoxItem>
    {
        public ComboBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Flag which indicates, if the <see cref="ComboBox"/> is editable or not.
        /// </summary>
        public virtual bool IsEditable => this.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.TextBox, out var textBox) &&
                                              textBox.TryGetValuePattern(out var valuePattern) &&
                                                !valuePattern.Current.IsReadOnly;

        /// <summary>
        /// Flag which indicates, if the <see cref="ComboBox"/> is readonly or not.
        /// Note that readonly only affects text input.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                if (this.AutomationElement.TryGetValuePattern(out var valuePattern))
                {
                    return valuePattern.Current.IsReadOnly;
                }

                return !this.AutomationElement.TryGetSelectionPattern(out _);
            }
        }

        public bool IsDropDownOpen
        {
            get
            {
                if (this.FrameworkType == FrameworkType.WinForms)
                {
                    // WinForms
                    var itemsList = this.FindFirstChild(Conditions.ListBox);

                    // UIA3 does not see the list if it is collapsed
                    return itemsList != null && !itemsList.IsOffscreen;
                }

                // WPF
                if (this.AutomationElement.TryGetExpandCollapsePattern(out var pattern))
                {
                    return pattern.Current.ExpandCollapseState == ExpandCollapseState.Expanded;
                }

                return false;
            }
        }

        /// <summary>
        /// The text of the editable element inside the combobox.
        /// Only works if the combobox is editable.
        /// </summary>
        public string EditableText
        {
            get => this.EditableItem.Text;
            set => this.EditableItem.Text = value;
        }

        /// <summary>
        /// Getter / setter for the selected value.
        /// </summary>
        public string Value
        {
            get => this.ValuePattern.Current.Value;
            set => this.ValuePattern.SetValue(value);
        }

        public ValuePattern ValuePattern => this.AutomationElement.ValuePattern();

        /// <summary>
        /// Gets the editable element
        /// </summary>
        protected virtual TextBox EditableItem
        {
            get
            {
                if (this.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.TextBox, out var element))
                {
                    return new TextBox(element);
                }

                throw new Exception("ComboBox is not editable.");
            }
        }

        /// <summary>
        /// Simulate typing in text. This is slower than setting Text but raises more events.
        /// </summary>
        public void Enter(string value)
        {
            if (this.IsOffscreen)
            {
                throw new InvalidOperationException("Cannot click when off screen.");
            }

            if (!this.IsKeyboardFocusable)
            {
                throw new InvalidOperationException("Cannot enter when not KeyboardFocusable.");
            }

            if (!this.HasKeyboardFocus)
            {
                this.Focus();
            }

            this.ValuePattern.SetValue(string.Empty);
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            var lines = value.Replace("\r\n", "\n").Split('\n');
            Keyboard.Type(lines[0]);
            foreach (var line in lines.Skip(1))
            {
                Keyboard.Type(Key.RETURN);
                Keyboard.Type(line);
            }

            // give some time to process input.
            var stopTime = DateTime.Now.AddSeconds(1);
            while (DateTime.Now < stopTime)
            {
                if (this.EditableText == value)
                {
                    return;
                }

                if (!Thread.Yield())
                {
                    Thread.Sleep(10);
                }
            }
        }

        public void Expand()
        {
            if (this.IsDropDownOpen)
            {
                return;
            }

            if (this.FrameworkType == FrameworkType.WinForms)
            {
                // WinForms
                if (!this.IsEnabled)
                {
                    throw new InvalidOperationException("ComboBox must be enabled for expanding");
                }

                var openButton = this.FindButton();
                openButton.Invoke();
            }
            else
            {
                // WPF
                this.AutomationElement.ExpandCollapsePattern().Expand();

                // Wait a bit in case there is an open animation
                Wait.For(TimeSpan.FromMilliseconds(50));
            }
        }

        public void Collapse()
        {
            if (!this.IsDropDownOpen)
            {
                return;
            }

            if (this.FrameworkType == FrameworkType.WinForms)
            {
                // WinForms
                if (!this.IsEnabled)
                {
                    throw new InvalidOperationException("ComboBox must be enabled for expanding");
                }

                var openButton = this.FindButton();
                if (this.IsEditable)
                {
                    // WinForms editable combo box only closes on click and not on invoke
                    openButton.Click();
                }
                else
                {
                    openButton.Invoke();
                }
            }
            else
            {
                // WPF
                this.AutomationElement.ExpandCollapsePattern().Collapse();
            }

            _ = Wait.UntilResponsive(this);
        }
    }
}
