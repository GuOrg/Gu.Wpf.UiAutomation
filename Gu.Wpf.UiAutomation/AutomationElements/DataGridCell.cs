namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Automation;

    public class DataGridCell : SelectionItemControl
    {
        public static readonly Regex NewItemPlaceHolderRegex = new Regex("^[^:]+: {NewItemPlaceholder}, [^:]+: \\d+$", RegexOptions.Singleline | RegexOptions.Compiled);

        public DataGridCell(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public DataGrid ContainingDataGrid => (DataGrid)FromAutomationElement(this.GridItemPattern.Current.ContainingGrid);

        /// <summary>
        /// Get the parent of the cell (which should be the row)
        /// </summary>
        public DataGridRow ContainingRow => (DataGridRow)FromAutomationElement(this.AutomationElement.Parent());

        public bool IsReadOnly
        {
            get
            {
                if (this.AutomationElement.TryGetValuePattern(out var pattern))
                {
                    return pattern.Current.IsReadOnly;
                }

                if (this.IsNewItemPlaceholder)
                {
                    if (this.GridItemPattern.Current.Row > 0)
                    {
                        return this.GridItemPattern.Current.ContainingGrid.GridPattern()
                                   .GetItem(0, this.GridItemPattern.Current.Column)
                                   .ValuePattern().Current.IsReadOnly;
                    }
                }

                if (!this.IsOffscreen)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsNewItemPlaceholder => NewItemPlaceHolderRegex.IsMatch(this.AutomationElement.Name());

        public string Value
        {
            get
            {
                string GetValue()
                {
                    if (this.AutomationElement.TryGetValuePattern(out var valuePattern) &&
                        valuePattern.Current.Value is string text &&
                        text != string.Empty)
                    {
                        return text;
                    }

                    if (this.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.TextBlock, out var textBlockOrLabel) ||
                        this.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.Label, out textBlockOrLabel))
                    {
                        return textBlockOrLabel.Name();
                    }

                    if (this.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.TextBox, out var textBox) &&
                        textBox.TryGetValuePattern(out var pattern))
                    {
                        return pattern.Current.Value;
                    }

                    if (this.AutomationElement.TryGetValuePattern(out valuePattern))
                    {
                        return valuePattern.Current.Value;
                    }

                    return this.Name;
                }

                var value = GetValue();
                if (string.IsNullOrEmpty(value))
                {
                    return value ?? string.Empty;
                }

                return NewItemPlaceHolderRegex.IsMatch(value) ? string.Empty : value;
            }

            set
            {
                if (this.AutomationElement.TryGetValuePattern(out var valuePattern))
                {
                    valuePattern.SetValue(value);
                    _ = Wait.UntilResponsive(this);
                    if (this.AutomationElement.TryFindFirst(
                        TreeScope.Children,
                        Conditions.TextBox,
                        out var textBox))
                    {
                        if (textBox.TryGetValuePattern(out valuePattern) &&
                            !valuePattern.Current.IsReadOnly)
                        {
                            valuePattern.SetValue(value);
                        }
                    }

                    return;
                }

                this.Enter(value);
                Keyboard.Type(Key.ENTER);
            }
        }

        public GridItemPattern GridItemPattern => this.AutomationElement.GridItemPattern();

        public TableItemPattern TableItemPattern => this.AutomationElement.TableItemPattern();

        /// <summary>
        /// Simulate typing in text. This is slower than setting Text but raises more events.
        /// </summary>
        public void Enter(string value, TimeSpan? delay = null)
        {
            if (value != null &&
                value.Contains('\n'))
            {
                throw new NotSupportedException("Only single line allowed for now.");
            }

            if (this.AutomationElement.TryGetSelectionItemPattern(out var selectionItemPattern))
            {
                if (!selectionItemPattern.Current.IsSelected)
                {
                    selectionItemPattern.Select();
                }
            }
            else if (!this.HasKeyboardFocus)
            {
                this.Click();
                this.Click();
            }

            if (this.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.TextBox, out var element))
            {
                new TextBox(element).Enter(value);
            }
            else
            {
                Keyboard.Type(value);
            }

            if (delay != null)
            {
                // give some time to process input.
                var stopTime = DateTime.Now + delay.Value;
                while (DateTime.Now < stopTime)
                {
                    if (this.Value == value)
                    {
                        return;
                    }

                    if (!Thread.Yield())
                    {
                        Thread.Sleep(10);
                    }
                }
            }
            else
            {
                _ = Wait.UntilResponsive(this);
            }
        }
    }
}
