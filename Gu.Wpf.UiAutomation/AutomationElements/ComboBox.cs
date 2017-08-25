namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Element which can be used for combobox elements.
    /// </summary>
    public class ComboBox : AutomationElement
    {
        public ComboBox(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// Flag which indicates, if the combobox is editable or not.
        /// </summary>
        public virtual bool IsEditable => this.GetEditableElement() != null;

        /// <summary>
        /// Gets all selected items.
        /// </summary>
        public ComboBoxItem[] SelectedItems
        {
            get
            {
                // In WinForms, there is no selection pattern, so search the items which are selected.
                if (this.SelectionPattern == null)
                {
                    return this.Items.Where(x => x.IsSelected).ToArray();
                }

                return this.SelectionPattern.Selection.Value.Select(x => new ComboBoxItem(x.BasicAutomationElement)).ToArray();
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
                IReadOnlyList<AutomationElement> items;
                if (this.FrameworkType == FrameworkType.WinForms || this.FrameworkType == FrameworkType.Win32)
                {
                    // WinForms and Win32
                    var listElement = this.FindFirstChild(cf => cf.ByControlType(ControlType.List));
                    items = listElement.FindAllChildren();
                }
                else
                {
                    // WPF
                    items = this.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                }

                return items.Select(x => new ComboBoxItem(x.BasicAutomationElement)).ToArray();
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
                    return itemsList != null && !itemsList.Properties.IsOffscreen;
                }

                // WPF
                var ecp = this.Patterns.ExpandCollapse.PatternOrDefault;
                return ecp?.ExpandCollapseState.ValueOrDefault == ExpandCollapseState.Expanded;
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
                var openButton = this.FindFirstChild(cf => cf.ByControlType(ControlType.Button)).AsButton();
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
                    Thread.Sleep(50);
                }
            }
        }

        public void Collapse()
        {
            if (!this.Properties.IsEnabled ||
                !this.IsDropDownOpen)
            {
                return;
            }

            if (this.FrameworkType == FrameworkType.WinForms)
            {
                // WinForms
                var openButton = this.FindFirstChild(cf => cf.ByControlType(ControlType.Button)).AsButton();
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

        private AutomationElement GetEditableElement()
        {
            return this.FindFirstChild(cf => cf.ByControlType(ControlType.Edit));
        }
    }
}
