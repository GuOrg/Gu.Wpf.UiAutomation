namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <summary>
    /// Cell element for grids and tables.
    /// </summary>
    public class GridCell : Control
    {
        private static readonly Regex NewItemPlaceHolderRegex = new Regex("^Item: {NewItemPlaceholder}, Column Display Index: \\d$", RegexOptions.Singleline|RegexOptions.Compiled);

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
                var valuePattern = this.Patterns.Value.PatternOrDefault;
                if (valuePattern != null)
                {
                    return valuePattern.IsReadOnly;
                }

                if (this.IsOffscreen)
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
                var valuePattern = this.Patterns.Value.PatternOrDefault;
                var value = valuePattern != null
                    ? valuePattern.Value
                    : this.Properties.Name.Value;

                return NewItemPlaceHolderRegex.IsMatch(value);
            }
        }

        public string Value
        {
            get
            {
                var valuePattern = this.Patterns.Value.PatternOrDefault;
                var value = valuePattern != null
                    ? valuePattern.Value
                    : this.Properties.Name.Value;
                return NewItemPlaceHolderRegex.IsMatch(value) ? string.Empty : value;
            }

            set
            {
                var valuePattern = this.Patterns.Value.PatternOrDefault;
                if (valuePattern != null)
                {
                    valuePattern.SetValue(value);
                    return;
                }

                this.Enter(value);
                Keyboard.Type(VirtualKeyShort.ENTER);
            }
        }

        protected IGridItemPattern GridItemPattern => this.Patterns.GridItem.Pattern;

        protected ITableItemPattern TableItemPattern => this.Patterns.TableItem.Pattern;

        /// <summary>
        /// Simulate typing in text. This is slower than setting Text but raises more events.
        /// </summary>
        public void Enter(string value, TimeSpan? delay = null)
        {
            this.Click();
            var lines = value.Replace("\r\n", "\n").Split('\n');
            Keyboard.Type(lines[0]);
            foreach (var line in lines.Skip(1))
            {
                Keyboard.Type(VirtualKeyShort.RETURN);
                Keyboard.Type(line);
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
        }
    }
}