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
        public virtual bool IsEditable => this.FindAllChildren().Any(c => c.ControlType == ControlType.Edit &&
                                                                          c.Patterns.Value.PatternOrDefault?.IsReadOnly.ValueOrDefault() == false);

        /// <summary>
        /// Flag which indicates, if the <see cref="ComboBox"/> is readonly or not.
        /// Note that readonly only affects text input.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                if (this.Patterns.Value.TryGetPattern(out var valuePattern) &&
                    valuePattern.IsReadOnly.TryGetValue(out var value))
                {
                    return value;
                }

                if (this.Patterns.Selection.TryGetPattern(out var selectPattern) &&
                    selectPattern.Selection.IsSupported)
                {
                    return false;
                }

                return true;
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
                if (this.SelectionPattern == null)
                {
                    return this.Items.Where(x => x.IsSelected).ToArray();
                }

                return this.SelectionPattern.Selection.Value.Select(x => new ComboBoxItem(x.AutomationElement)).ToArray();
            }
        }

        /// <summary>
        /// Gets the first selected item or null otherwise.
        /// </summary>
        public ComboBoxItem SelectedItem => this.SelectedItems?.FirstOrDefault();

        /// <summary>
        /// Gets all items.
        /// </summary>
        public IReadOnlyList<ComboBoxItem> Items
        {
            get
            {
                this.Expand();
                if (this.FrameworkType == FrameworkType.WinForms || this.FrameworkType == FrameworkType.Win32)
                {
                    // WinForms and Win32
                    var listElement = this.FindFirstChild(cf => cf.ByControlType(ControlType.List));
                    return listElement.FindAllChildren(x => new ComboBoxItem(x));
                }

                // WPF
                return this.FindAllChildren(
                    this.CreateCondition(ControlType.ListItem),
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
                    var itemsList = this.FindFirstChild(cf => cf.ByControlType(ControlType.List));

                    // UIA3 does not see the list if it is collapsed
                    return itemsList != null && !itemsList.Properties.IsOffscreen.Value;
                }

                // WPF
                var ecp = this.Patterns.ExpandCollapse.PatternOrDefault;
                return ecp?.ExpandCollapseState.ValueOrDefault() == ExpandCollapseState.Expanded;
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
            get => this.ValuePattern.Value.Value;
            set => this.ValuePattern.SetValue(value);
        }

        protected IValuePattern ValuePattern => this.Patterns.Value.Pattern;

        protected ISelectionPattern SelectionPattern => this.Patterns.Selection.PatternOrDefault;

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

            var valuePattern = this.Patterns.Value.PatternOrDefault;
            valuePattern?.SetValue(string.Empty);
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
            if (!this.Properties.IsEnabled.Value ||
                this.IsDropDownOpen)
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

                    // Wait a bit in case there is an open animation
                    Wait.For(TimeSpan.FromMilliseconds(50));
                }
            }
        }

        public void Collapse()
        {
            if (!this.IsEnabled ||
                !this.IsDropDownOpen)
            {
                return;
            }

            if (this.FrameworkType == FrameworkType.WinForms)
            {
                // WinForms
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
                var ecp = this.Patterns.ExpandCollapse.PatternOrDefault;
                ecp?.Collapse();
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
            return this.FindFirstChild(cf => cf.ByControlType(ControlType.Edit));
        }
    }
}
