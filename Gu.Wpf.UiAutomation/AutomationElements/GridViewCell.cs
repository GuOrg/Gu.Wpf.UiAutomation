namespace Gu.Wpf.UiAutomation
{
    using System;

    /// <summary>
    /// Cell element for grids and tables.
    /// </summary>
    public class GridViewCell : SelectionItemControl
    {
        public GridViewCell(UiElement content)
            : base(content?.AutomationElement ?? throw new ArgumentNullException(nameof(content)))
        {
            this.Content = content;
        }

        public UiElement Content { get; }

        public ListView ContainingListView => (ListView)FromAutomationElement(this.SelectionItemPattern.Current.SelectionContainer);

        public string Text
        {
            get
            {
                if (this.Content.AutomationElement.ControlType().Id == System.Windows.Automation.ControlType.Text.Id)
                {
                    return this.Content.Name;
                }

                if (this.Content.AutomationElement.TryGetValuePattern(out var valuePattern))
                {
                    return valuePattern.Current.Value;
                }

                return null;
            }
        }

        public static GridViewCell Create(UiElement content)
        {
            return new GridViewCell(content);
        }
    }
}
