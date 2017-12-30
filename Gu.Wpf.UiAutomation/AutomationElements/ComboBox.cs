namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Automation;

    /// <summary>
    /// Element which can be used for combobox elements.
    /// </summary>
    public class ComboBox : Control
    {
        public ComboBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Flag which indicates, if the <see cref="ComboBox"/> is editable or not.
        /// </summary>
        public virtual bool IsEditable => this.FindAllChildren(Condition.ControlTypeEdit)
                                              .Any(c => c.AutomationElement.TryGetValuePattern(out var pattern) &&
                                                        !pattern.Current.IsReadOnly);

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

        /// <summary>
        /// Gets all selected items.
        /// </summary>
        public IReadOnlyList<ComboBoxItem> SelectedItems
        {
            get
            {
                // In WinForms, there is no selection pattern, so search the items which are selected.
                if (this.FrameworkType == FrameworkType.WinForms)
                {
                    return this.Items.Where(x => x.IsSelected).ToArray();
                }

                if (this.AutomationElement.TryGetSelectionPattern(out var pattern))
                {
                    return pattern.Current.GetSelection().Select(x => new ComboBoxItem(x)).ToArray();
                }

                return new ComboBoxItem[0];
            }
        }

        /// <summary>
        /// Gets the first selected item or null otherwise.
        /// </summary>
        public ComboBoxItem SelectedItem => this.SelectedItems?.SingleOrDefault();

        /// <summary>
        /// Gets all items.
        /// </summary>
        public IReadOnlyList<ComboBoxItem> Items
        {
            get
            {
                this.Expand();
                if (this.FrameworkType == FrameworkType.WinForms ||
                    this.FrameworkType == FrameworkType.Win32)
                {
                    // WinForms and Win32
                    var listElement = this.FindFirstChild(Condition.List);
                    return listElement.FindAllChildren(x => new ComboBoxItem(x));
                }

                // WPF
                return this.FindAllChildren(
                    Condition.ListItem,
                    x => new ComboBoxItem(x));
            }
        }

        public bool IsDropDownOpen
        {
            get
            {
                if (this.FrameworkType == FrameworkType.WinForms)
                {
                    // WinForms
                    var itemsList = this.FindFirstChild(Condition.List);

                    // UIA3 does not see the list if it is collapsed
                    return itemsList != null && !itemsList.IsOffscreen;
                }

                // WPF
                if (this.AutomationElement.TryGetExpandCollapsePattern(out var pattern))
                {
                    return pattern.Current.ExpandCollapseState == System.Windows.Automation.ExpandCollapseState.Expanded;
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

        protected ValuePattern ValuePattern => this.AutomationElement.ValuePattern();

        protected SelectionPattern SelectionPattern => this.AutomationElement.SelectionPattern();

        /// <summary>
        /// Gets the editable element
        /// </summary>
        protected virtual TextBox EditableItem
        {
            get
            {
                var edit = this.GetEditableElement();
                if (edit != null)
                {
                    return edit.AsTextBox();
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
                    throw new InvalidOperationException("Combobox must be enabled for expanding");
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
                    throw new InvalidOperationException("Combobox must be enabled for expanding");
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

            Wait.UntilResponsive(this);
        }

        /// <summary>
        /// Select an item by index.
        /// </summary>
        public ComboBoxItem Select(int index)
        {
            var foundItem = this.Items[index];
            foundItem.Select();
            return foundItem;
        }

        /// <summary>
        /// Select the first item which matches the given text.
        /// </summary>
        /// <param name="textToFind">The text to search for.</param>
        /// <returns>The first found item or null if no item matches.</returns>
        public ComboBoxItem Select(string textToFind)
        {
            var foundItem = this.Items.FirstOrDefault(item => item.Text.Equals(textToFind));
            foundItem?.Select();
            return foundItem;
        }

        private UiElement GetEditableElement()
        {
            return this.FindFirstChild(Condition.ControlTypeEdit);
        }
    }
}
