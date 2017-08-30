namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Threading;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <summary>
    /// Cell element for grids and tables.
    /// </summary>
    public class GridCell : Control
    {
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

        public string Value => this.Properties.Name.Value;

        protected IGridItemPattern GridItemPattern => this.Patterns.GridItem.Pattern;

        protected ITableItemPattern TableItemPattern => this.Patterns.TableItem.Pattern;

        /// <summary>
        /// Simulate typing in text. This is slower than setting Text but raises more events.
        /// </summary>
        public void Enter(string value)
        {
            this.Click();
            var lines = value.Replace("\r\n", "\n").Split('\n');
            Keyboard.Type(lines[0]);
            foreach (var line in lines.Skip(1))
            {
                Keyboard.Type(VirtualKeyShort.RETURN);
                Keyboard.Type(line);
            }

            // give some time to process input.
            var stopTime = DateTime.Now.AddSeconds(1);
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