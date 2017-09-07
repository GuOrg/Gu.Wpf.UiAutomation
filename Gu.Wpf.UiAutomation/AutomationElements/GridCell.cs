namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;

    /// <summary>
    /// Cell element for grids and tables.
    /// </summary>
    public class GridCell : SelectionItemAutomationElement
    {
        private static readonly Regex NewItemPlaceHolderRegex = new Regex("^Item: {NewItemPlaceholder}, Column Display Index: \\d$", RegexOptions.Singleline | RegexOptions.Compiled);

        public GridCell(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public GridView ContainingGridView => this.GridItemPattern.ContainingGrid.Value.AsDataGrid();

        public GridRow ContainingRow
        {
            get
            {
                // Get the parent of the cell (which should be the row)
                var rowElement = this.Automation.TreeWalkerFactory.GetControlViewWalker().GetParent(this);
                return rowElement?.AsGridRow();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                if (this.Patterns.Value.TryGetPattern(out var valuePattern) &&
                    valuePattern.IsReadOnly.TryGetValue(out var isReadonly))
                {
                    return isReadonly;
                }

                if (this.IsNewItemPlaceholder)
                {
                    if (this.GridItemPattern.Row.Value > 0)
                    {
                        return this.GridItemPattern.ContainingGrid.Value.AsDataGrid()[0, this.GridItemPattern.Column.Value]
                                   .IsReadOnly;
                    }
                }

                if (!this.IsOffscreen)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsNewItemPlaceholder
        {
            get
            {
                string GetValue()
                {
                    if (this.Patterns.Value.TryGetPattern(out var valuePattern) &&
                        valuePattern.Value.TryGetValue(out var v))
                    {
                        return v;
                    }

                    return this.Properties.Name.ValueOrDefault();
                }

                var value = GetValue();
                if (value == null)
                {
                    return true;
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    return false;
                }

                return NewItemPlaceHolderRegex.IsMatch(value);
            }
        }

        public string Value
        {
            get
            {
                string GetValue()
                {
                    if (this.TryFindFirst(
                        TreeScope.Children,
                        this.CreateCondition(ControlType.Text),
                        x => new TextBlock(x),
                        TimeSpan.Zero,
                        out var textBlock))
                    {
                        return textBlock.Text;
                    }

                    if (this.TryFindFirst(
                        TreeScope.Children,
                        this.CreateCondition(ControlType.Edit),
                        x => new TextBox(x),
                        TimeSpan.Zero,
                        out var textBox))
                    {
                        return textBox.Text;
                    }

                    if (this.Patterns.Value.TryGetPattern(out var valuePattern) &&
                        valuePattern.Value.TryGetValue(out var text))
                    {
                        return text;
                    }

                    return this.Properties.Name.ValueOrDefault();
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
                if (this.Patterns.Value.TryGetPattern(out var valuePattern) &&
                    valuePattern.Value.IsSupported)
                {
                    valuePattern.SetValue(value);
                    Wait.UntilResponsive(this);
                    if (this.TryFindFirst(
                        TreeScope.Children,
                        this.CreateCondition(ControlType.Edit),
                        x => new TextBox(x),
                        TimeSpan.Zero,
                        out var textBox))
                    {
                        if (textBox.Patterns.Value.TryGetPattern(out valuePattern) &&
                            !valuePattern.IsReadOnly.ValueOrDefault())
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

        protected IGridItemPattern GridItemPattern => this.Patterns.GridItem.Pattern;

        protected ITableItemPattern TableItemPattern => this.Patterns.TableItem.Pattern;

        /// <summary>
        /// Simulate typing in text. This is slower than setting Text but raises more events.
        /// </summary>
        public void Enter(string value, TimeSpan? delay = null)
        {
            if (value != null &&
                value.Contains('\n'))
            {
                throw new ArgumentException("Only single line allowed for now.");
            }

            if (this.Patterns.SelectionItem.IsSupported)
            {
                if (!this.IsSelected)
                {
                    this.Select();
                }
            }
            else if (!this.HasKeyboardFocus)
            {
                this.Click();
                this.Click();
            }

            var child = this.FindFirstChild();
            if (child.ControlType == ControlType.Edit)
            {
                child.AsTextBox().Enter(value);
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
                Wait.UntilResponsive(this);
            }
        }
    }
}