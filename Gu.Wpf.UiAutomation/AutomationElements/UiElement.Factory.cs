namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;

    public partial class UiElement
    {
        private static readonly List<ConditionAndCreate> ConditionAndCreates = new List<ConditionAndCreate>
        {
            new ConditionAndCreate(Conditions.Button, Button.Create),
            //// Calendar
            new ConditionAndCreate(Conditions.Calendar, e => new Calendar(e)),
            new ConditionAndCreate(Conditions.CalendarDayButton, e => new CalendarDayButton(e)),
            new ConditionAndCreate(Conditions.CheckBox, e => new CheckBox(e)),
            new ConditionAndCreate(Conditions.ComboBox, e => new ComboBox(e)),
            new ConditionAndCreate(Conditions.ContextMenu, e => new ContextMenu(e)),
            //// Custom
            new ConditionAndCreate(Conditions.DataGrid, e => new DataGrid(e)),
            new ConditionAndCreate(Conditions.DataGridCell, e => new DataGridCell(e)),
            new ConditionAndCreate(Conditions.DataGridColumnHeader, e => new DataGridColumnHeader(e)),
            new ConditionAndCreate(Conditions.DataGridColumnHeadersPresenter, e => new DataGridColumnHeadersPresenter(e)),
            new ConditionAndCreate(Conditions.DataGridDetailsPresenter, e => new DataGridDetailsPresenter(e)),
            new ConditionAndCreate(Conditions.DataGridRowHeader, e => new DataGridRowHeader(e)),
            new ConditionAndCreate(Conditions.DataGridRow, e => new DataGridRow(e)),
            new ConditionAndCreate(Conditions.DatePicker, e => new DatePicker(e)),
            //// DataItem
            //// ControlTypeDocument
            new ConditionAndCreate(Conditions.Expander, e => new Expander(e)),
            new ConditionAndCreate(Conditions.GridSplitter, e => new GridSplitter(e)),
            new ConditionAndCreate(Conditions.GridViewColumnHeader, e => new GridViewColumnHeader(e)),
            new ConditionAndCreate(Conditions.GridViewHeaderRowPresenter, e => new GridViewHeaderRowPresenter(e)),
            new ConditionAndCreate(Conditions.GridViewRowHeader, e => new GridViewRowHeader(e)),
            new ConditionAndCreate(Conditions.GroupBox, e => new GroupBox(e)),
            ////new ConditionAndCreate(Conditions.Header, e => new Header(e)),
            ////new ConditionAndCreate(Conditions.HeaderItem, e => new HeaderItem(e)),
            new ConditionAndCreate(Conditions.HorizontalScrollBar, e => new HorizontalScrollBar(e)),
            ////new ConditionAndCreate(Conditions.Hyperlink, e => new Hyperlink(e)),
            ////new ConditionAndCreate(Conditions.Image, e => new Image(e)),
            new ConditionAndCreate(Conditions.Label, e => new Label(e)),
            new ConditionAndCreate(Conditions.ListBox, e => new ListBox(e)),
            new ConditionAndCreate(Conditions.ListBoxItem, ListBoxItem.Create),
            new ConditionAndCreate(Conditions.ListView, e => new ListView(e)),
            new ConditionAndCreate(Conditions.ListViewItem, e => new ListViewItem(e)),
            new ConditionAndCreate(Conditions.Menu, e => new Menu(e)),
            new ConditionAndCreate(Conditions.MenuBar, e => new MenuBar(e)),
            new ConditionAndCreate(Conditions.MenuItem, e => new MenuItem(e)),
            new ConditionAndCreate(Conditions.MessageBox, e => new MessageBox(e)),
            new ConditionAndCreate(Conditions.OpenFileDialog, e => new OpenFileDialog(e)),
            new ConditionAndCreate(Conditions.SaveFileDialog, e => new SaveFileDialog(e)),
            new ConditionAndCreate(Conditions.ModalWindow, e => new Window(e, isMainWindow: false)),
            ////new ConditionAndCreate(Conditions.Pane, e => new Pane(e)),
            new ConditionAndCreate(Conditions.PasswordBox, e => new PasswordBox(e)),
            new ConditionAndCreate(Conditions.ProgressBar, e => new ProgressBar(e)),
            new ConditionAndCreate(Conditions.RadioButton, e => new RadioButton(e)),
            new ConditionAndCreate(Conditions.RepeatButton, e => new RepeatButton(e)),
            new ConditionAndCreate(Conditions.RichTextBox, e => new RichTextBox(e)),
            new ConditionAndCreate(Conditions.ScrollViewer, e => new ScrollViewer(e)),
            new ConditionAndCreate(Conditions.Separator, e => new Separator(e)),
            ////new ConditionAndCreate(Conditions.ScrollBar, e => new ScrollBar(e)),
            ////new ConditionAndCreate(Conditions.Separator, e => new Separator(e)),
            new ConditionAndCreate(Conditions.Slider, e => new Slider(e)),
            ////new ConditionAndCreate(Conditions.Spinner, e => new Spinner(e)),
            ////new ConditionAndCreate(Conditions.SplitButton, e => new SplitButton(e)),
            new ConditionAndCreate(Conditions.StatusBar, e => new StatusBar(e)),
            new ConditionAndCreate(Conditions.TabControl, e => new TabControl(e)),
            new ConditionAndCreate(Conditions.TabItem, e => new TabItem(e)),
            new ConditionAndCreate(Conditions.TextBlock, e => new TextBlock(e)),
            new ConditionAndCreate(Conditions.TextBoxBase, e => TextBoxBase.Create(e)),
            new ConditionAndCreate(Conditions.TextBox, e => new TextBox(e)),
            new ConditionAndCreate(Conditions.Thumb, e => new Thumb(e)),
            new ConditionAndCreate(Conditions.TitleBar, e => new TitleBar(e)),
            new ConditionAndCreate(Conditions.ToggleButton, e => new ToggleButton(e)),
            new ConditionAndCreate(Conditions.ToolBar, e => new ToolBar(e)),
            new ConditionAndCreate(Conditions.ToolTip, e => new ToolTip(e)),
            new ConditionAndCreate(Conditions.TreeView, e => new TreeView(e)),
            new ConditionAndCreate(Conditions.TreeViewItem, e => new TreeViewItem(e)),
            new ConditionAndCreate(Conditions.UserControl, e => new UserControl(e)),
            new ConditionAndCreate(Conditions.VerticalScrollBar, e => new VerticalScrollBar(e)),
            new ConditionAndCreate(Conditions.Window, e => new Window(e)),
        };

        public static UiElement FromAutomationElementOrNull(AutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            return FromAutomationElement(element);
        }

        public static UiElement FromAutomationElement(AutomationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            foreach (var conditionAndCreate in ConditionAndCreates)
            {
                if (conditionAndCreate.TryCreate(element, out var uiElement))
                {
                    if (element.Parent() is AutomationElement parent &&
                        Conditions.IsMatch(parent, Conditions.ListViewItem))
                    {
                        return new GridViewCell(uiElement);
                    }

                    return uiElement;
                }
            }

            return new UiElement(element);
        }

        /// <summary>
        /// Register a custom type for <see cref="FromAutomationElement"/>
        /// </summary>
        /// <param name="condition">The condition that matches the type</param>
        /// <param name="create">The factory method for creating an instance</param>
        public static void Register(Condition condition, Func<AutomationElement, UiElement> create)
        {
            ConditionAndCreates.Insert(0, new ConditionAndCreate(condition, create));
        }

        private class ConditionAndCreate
        {
            private readonly Condition condition;
            private readonly Func<AutomationElement, UiElement> create;

            public ConditionAndCreate(Condition condition, Func<AutomationElement, UiElement> create)
            {
                this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
                this.create = create ?? throw new ArgumentNullException(nameof(create));
            }

            public bool TryCreate(AutomationElement element, out UiElement uiElement)
            {
                if (Conditions.IsMatch(element, this.condition))
                {
                    uiElement = this.create(element);
                    return true;
                }

                uiElement = null;
                return false;
            }
        }
    }
}
