namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;

    public partial class UiElement
    {
        private static readonly IReadOnlyList<ConditionAndCreate> ConditionAndCreates = new[]
        {
            new ConditionAndCreate(Condition.Button, e => new Button(e)),
            //// Calendar
            new ConditionAndCreate(Condition.CheckBox, e => new CheckBox(e)),
            new ConditionAndCreate(Condition.ComboBox, e => new ComboBox(e)),
            //// Custom
            new ConditionAndCreate(Condition.DataGrid, e => new DataGrid(e)),
            new ConditionAndCreate(Condition.DataGridCell, e => new DataGridCell(e)),
            new ConditionAndCreate(Condition.DataGridColumnHeader, e => new DataGridColumnHeader(e)),
            new ConditionAndCreate(Condition.DataGridRowHeader, e => new DataGridRowHeader(e)),
            new ConditionAndCreate(Condition.DatePicker, e => new DatePicker(e)),
            //// DataItem
            //// ControlTypeDocument
            new ConditionAndCreate(Condition.Expander, e => new Expander(e)),
            new ConditionAndCreate(Condition.GridViewColumnHeader, e => new GridViewColumnHeader(e)),
            new ConditionAndCreate(Condition.GridViewRowHeader, e => new GridViewRowHeader(e)),
            new ConditionAndCreate(Condition.GroupBox, e => new GroupBox(e)),
            ////new ConditionAndCreate(Condition.Header, e => new Header(e)),
            ////new ConditionAndCreate(Condition.HeaderItem, e => new HeaderItem(e)),
            ////new ConditionAndCreate(Condition.Hyperlink, e => new Hyperlink(e)),
            ////new ConditionAndCreate(Condition.Image, e => new Image(e)),
            new ConditionAndCreate(Condition.Label, e => new Label(e)),
            new ConditionAndCreate(Condition.ListBox, e => new ListBox(e)),
            new ConditionAndCreate(Condition.ListBoxItem, e => new ListBoxItem(e)),
            new ConditionAndCreate(Condition.ListView, e => new ListView(e)),
            new ConditionAndCreate(Condition.Menu, e => new Menu(e)),
            ////new ConditionAndCreate(Condition.MenuBar, e => new MenuBar(e)),
            new ConditionAndCreate(Condition.MenuItem, e => new MenuItem(e)),
            new ConditionAndCreate(Condition.MessageBox, e => new MessageBox(e)),
            new ConditionAndCreate(Condition.ModalWindow, e => new Window(e, isMainWindow: false)),
            ////new ConditionAndCreate(Condition.Pane, e => new Pane(e)),
            new ConditionAndCreate(Condition.ProgressBar, e => new ProgressBar(e)),
            new ConditionAndCreate(Condition.RadioButton, e => new RadioButton(e)),
            ////new ConditionAndCreate(Condition.ScrollBar, e => new ScrollBar(e)),
            ////new ConditionAndCreate(Condition.Separator, e => new Separator(e)),
            new ConditionAndCreate(Condition.Slider, e => new Slider(e)),
            ////new ConditionAndCreate(Condition.Spinner, e => new Spinner(e)),
            ////new ConditionAndCreate(Condition.SplitButton, e => new SplitButton(e)),
            new ConditionAndCreate(Condition.StatusBar, e => new StatusBar(e)),
            new ConditionAndCreate(Condition.TabControl, e => new TabControl(e)),
            new ConditionAndCreate(Condition.TabItem, e => new TabItem(e)),
            new ConditionAndCreate(Condition.TextBlock, e => new TextBlock(e)),
            new ConditionAndCreate(Condition.TextBox, e => new TextBox(e)),
            new ConditionAndCreate(Condition.Thumb, e => new Thumb(e)),
            new ConditionAndCreate(Condition.TitleBar, e => new TitleBar(e)),
            new ConditionAndCreate(Condition.ToolBar, e => new ToolBar(e)),
            ////new ConditionAndCreate(Condition.ToolTip, e => new ToolTip(e)),
            new ConditionAndCreate(Condition.TreeView, e => new TreeView(e)),
            new ConditionAndCreate(Condition.TreeViewItem, e => new TreeViewItem(e)),
            new ConditionAndCreate(Condition.UserControl, e => new UserControl(e)),
            new ConditionAndCreate(Condition.Window, e => new Window(e)),
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
                    return uiElement;
                }
            }

            return new UiElement(element);
        }

        private class ConditionAndCreate
        {
            private readonly System.Windows.Automation.Condition condition;
            private readonly Func<AutomationElement, UiElement> create;

            public ConditionAndCreate(System.Windows.Automation.Condition condition, Func<AutomationElement, UiElement> create)
            {
                this.condition = condition;
                this.create = create;
            }

            public bool TryCreate(AutomationElement element, out UiElement uiElement)
            {
                if (Condition.IsMatch(element, this.condition))
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