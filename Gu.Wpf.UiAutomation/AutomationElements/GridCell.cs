namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Automation;

    /// <summary>
    /// Cell element for grids and tables.
    /// </summary>
    public class GridCell : SelectionItemAutomationElement
    {
        private static readonly Regex NewItemPlaceHolderRegex = new Regex("^[^:]+: {NewItemPlaceholder}, [^:]+: \\d+$", RegexOptions.Singleline | RegexOptions.Compiled);

        public GridCell(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public GridView ContainingGridView
        {
            get
            {
                var containingGrid = this.GridItemPattern.Current.ContainingGrid;
                if (containingGrid.Current.ControlType.Id == ControlType.DataGrid.Id)
                {
                    return new DataGrid(containingGrid);
                }

                return new ListView(containingGrid);
            }
        }

        /// <summary>
        /// Get the parent of the cell (which should be the row)
        /// </summary>
        public GridRow ContainingRow => new GridRow(TreeWalker.ContentViewWalker.GetParent(this.AutomationElement));

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

        public bool IsNewItemPlaceholder
        {
            get
            {
                string GetValue()
                {
                    if (this.AutomationElement.TryGetValuePattern(out var pattern))
                    {
                        return pattern.Current.Value;
                    }

                    return this.Name;
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
                        Condition.ByControlType(ControlType.Text),
                        x => new TextBlock(x),
                        TimeSpan.Zero,
                        out var textBlock))
                    {
                        return textBlock.Text;
                    }

                    if (this.TryFindFirst(
                        TreeScope.Children,
                        Condition.ByControlType(ControlType.Edit),
                        x => new TextBox(x),
                        TimeSpan.Zero,
                        out var textBox))
                    {
                        return textBox.Text;
                    }

                    if (this.AutomationElement.TryGetValuePattern(out var valuePattern))
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
                    Wait.UntilResponsive(this);
                    if (this.AutomationElement.TryFindFirst(
                        TreeScope.Children,
                        Condition.ByControlType(ControlType.Edit),
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

        protected GridItemPattern GridItemPattern => this.AutomationElement.GridItemPattern();

        protected TableItemPattern TableItemPattern => this.AutomationElement.TableItemPattern();

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

            if (this.AutomationElement.TryGetSelectionItemPattern(out var selectionItemPattern))
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
            if (child.ControlType.Id == ControlType.Edit.Id)
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