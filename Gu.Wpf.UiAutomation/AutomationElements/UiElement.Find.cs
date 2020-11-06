namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows;
    using System.Windows.Automation;

    public partial class UiElement
    {
        public static UiElement FromPoint(Point point)
        {
            return FromAutomationElement(AutomationElement.FromPoint(point));
        }

        /// <summary>
        /// Find the first <see cref="Button"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public Button FindButton(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.Button, name),
            x => new Button(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="Calendar"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public Calendar FindCalendar(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.Calendar, name),
            x => new Calendar(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="CalendarDayButton"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public CalendarDayButton FindCalendarDayButton(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.CalendarDayButton, name),
            x => new CalendarDayButton(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="CheckBox"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public CheckBox FindCheckBox(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.CheckBox, name),
            x => new CheckBox(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="ComboBox"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public ComboBox FindComboBox(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ComboBox, name),
            x => new ComboBox(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="DataGrid"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public DataGrid FindDataGrid(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.DataGrid, name),
            x => new DataGrid(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="DatePicker"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public DatePicker FindDatePicker(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.DatePicker, name),
            x => new DatePicker(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="Frame"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public Frame FindFrame(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.Frame, name),
            x => new Frame(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="Label"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public Label FindLabel(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.Label, name),
            x => new Label(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="GridSplitter"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public GridSplitter FindGridSplitter(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.GridSplitter, name),
            x => new GridSplitter(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="GroupBox"/> box by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public GroupBox FindGroupBox(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.GroupBox, name),
            x => new GroupBox(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="Expander"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public Expander FindExpander(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.Expander, name),
            x => new Expander(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="Menu"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public Menu FindMenu(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.Menu, name),
            x => new Menu(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="MenuItem"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public MenuItem FindMenuItem(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.MenuItem, name),
            x => new MenuItem(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="HorizontalScrollBar"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public HorizontalScrollBar FindHorizontalScrollBar(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.HorizontalScrollBar, name),
            x => new HorizontalScrollBar(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="VerticalScrollBar"/> bar by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public VerticalScrollBar FindVerticalScrollBar(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.VerticalScrollBar, name),
            x => new VerticalScrollBar(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="ListBox"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public ListBox FindListBox(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ListBox, name),
            x => new ListBox(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="ListView"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public ListView FindListView(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ListView, name),
            x => new ListView(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="OpenFileDialog"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public OpenFileDialog FindOpenFileDialog(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.OpenFileDialog, name),
            x => new OpenFileDialog(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="PasswordBox"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public PasswordBox FindPasswordBox(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.PasswordBox, name),
            x => new PasswordBox(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="RichTextBox"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public RichTextBox FindRichTextBox(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.RichTextBox, name),
            x => new RichTextBox(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="ProgressBar"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public ProgressBar FindProgressBar(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ProgressBar, name),
            x => new ProgressBar(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="RadioButton"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public RadioButton FindRadioButton(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.RadioButton, name),
            x => new RadioButton(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="RepeatButton"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public RepeatButton FindRepeatButton(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.RepeatButton, name),
            x => new RepeatButton(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="SaveFileDialog"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public SaveFileDialog FindSaveFileDialog(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.SaveFileDialog, name),
            x => new SaveFileDialog(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="Separator"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public Separator FindSeparator(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.Separator, name),
            x => new Separator(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="Slider"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public Slider FindSlider(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.Slider, name),
            x => new Slider(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="ScrollViewer"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public ScrollViewer FindScrollViewer(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ScrollViewer, name),
            x => new ScrollViewer(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="TextBlock"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public TextBlock FindTextBlock(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.TextBlock, name),
            x => new TextBlock(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="TextBox"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public TextBox FindTextBox(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.TextBox, name),
            x => new TextBox(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="TextBoxBase"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public TextBoxBase FindTextBoxBase(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.TextBoxBase, name),
            x => new TextBoxBase(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="ToggleButton"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public ToggleButton FindToggleButton(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ToggleButton, name),
            x => new ToggleButton(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="ToolTip"/>.
        /// </summary>
        public ToolTip FindToolTip() => this.Window
                                            .FindPopup()
                                            .FindFirst(
                                                TreeScope.Descendants,
                                                this.CreateCondition(Conditions.ToolTip, this.HelpText),
                                                x => new ToolTip(x),
                                                Retry.Time);

        /// <summary>
        /// Find the first <see cref="TabControl"/> by x:Name, Content or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public TabControl FindTabControl(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.TabControl, name),
            x => new TabControl(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="TitleBar"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public TitleBar FindTitleBar(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.TitleBar, name),
            x => new TitleBar(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="ToolBar"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public ToolBar FindToolBar(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ToolBar, name),
            x => new ToolBar(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="TreeView"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public TreeView FindTreeView(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.TreeView, name),
            x => new TreeView(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="UserControl"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public UserControl FindUserControl(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.UserControl, name),
            x => new UserControl(x),
            Retry.Time);

        /// <summary>
        /// Find the first <see cref="StatusBar"/> by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        public StatusBar FindStatusBar(string? name = null) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.StatusBar, name),
            x => new StatusBar(x),
            Retry.Time);

        /// <summary>
        /// Find the first descendant by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        /// <param name="controlType">The control type.</param>
        public UiElement FindDescendant(string name, ControlType controlType) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ByControlType(controlType), name),
            Retry.Time);

        /// <summary>
        /// Find the first element by x:Name, Header or AutomationID.
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID.</param>
        /// <param name="controlType">The control type.</param>
        /// <param name="wrap">The function to produce a T from the match. Normally x => new Foo(x).</param>
        public T FindDescendant<T>(string name, ControlType controlType, Func<AutomationElement, T> wrap)
            where T : UiElement => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ByControlType(controlType), name),
            wrap,
            Retry.Time);

        public UiElement FindDescendant(ControlType controlType) => this.FindFirst(
            TreeScope.Descendants,
            Conditions.ByControlType(controlType),
            Retry.Time);

        public T FindDescendant<T>(ControlType controlType, Func<AutomationElement, T> wrap)
            where T : UiElement => this.FindFirst(
            TreeScope.Descendants,
            Conditions.ByControlType(controlType),
            wrap,
            Retry.Time);

        public UiElement FindDescendant(ControlType controlType, int index) => this.FindAt(
            TreeScope.Descendants,
            Conditions.ByControlType(controlType),
            index,
            Retry.Time);

        public T FindDescendant<T>(ControlType controlType, int index, Func<AutomationElement, T> wrap)
            where T : UiElement => this.FindAt(
            TreeScope.Descendants,
            Conditions.ByControlType(controlType),
            index,
            wrap,
            Retry.Time);

        public UiElement FindDescendant(ControlType controlType, string name) => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ByControlType(controlType), name),
            Retry.Time);

        public T FindDescendant<T>(ControlType controlType, string name, Func<AutomationElement, T> wrap)
            where T : UiElement => this.FindFirst(
            TreeScope.Descendants,
            this.CreateCondition(Conditions.ByControlType(controlType), name),
            wrap,
            Retry.Time);

        public UiElement FindDescendant(string name) => this.FindFirst(
            TreeScope.Descendants,
            Conditions.ByNameOrAutomationId(name),
            Retry.Time);

        /// <summary>
        /// Finds all elements in the given tree scope and with the given condition.
        /// </summary>
        public IReadOnlyList<UiElement> FindAll(TreeScope treeScope, System.Windows.Automation.Condition condition)
        {
            return this.AutomationElement.FindAll(treeScope, condition, FromAutomationElement);
        }

        /// <summary>
        /// Finds all elements in the given tree scope and with the given condition.
        /// </summary>
        public IReadOnlyList<T> FindAll<T>(TreeScope treeScope, System.Windows.Automation.Condition condition, Func<AutomationElement, T> wrap)
            where T : UiElement
        {
            return this.AutomationElement.FindAll(treeScope, condition, wrap);
        }

        /// <summary>
        /// Finds all elements in the given tree scope and with the given condition within the given timeout.
        /// </summary>
        public IReadOnlyList<UiElement> FindAll(TreeScope treeScope, System.Windows.Automation.Condition condition, TimeSpan timeOut)
        {
            if (this.TryFindAll(treeScope, condition, timeOut, out var result))
            {
                return result;
            }

            throw new InvalidOperationException($"Did not find an element matching {condition}.");
        }

        /// <summary>
        /// Finds all elements in the given tree scope and with the given condition within the given timeout.
        /// </summary>
        public bool TryFindAll(TreeScope treeScope, System.Windows.Automation.Condition condition, TimeSpan timeOut, [NotNullWhen(true)]out IReadOnlyList<UiElement>? result)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return this.TryFindAll(treeScope, condition, FromAutomationElement, timeOut, out result);
        }

        /// <summary>
        /// Finds all elements in the given tree scope and with the given condition within the given timeout.
        /// </summary>
        public bool TryFindAll<T>(TreeScope treeScope, System.Windows.Automation.Condition condition, Func<AutomationElement, T> wrap, TimeSpan timeOut, [NotNullWhen(true)]out IReadOnlyList<T>? result)
            where T : UiElement
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            result = null;
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, timeOut))
            {
                result = this.AutomationElement.FindAll(treeScope, condition, wrap);
                if (result != null &&
                    result.Count > 0)
                {
                    return true;
                }

                Wait.For(Retry.PollInterval);
            }

            return result != null;
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition.
        /// </summary>
        public UiElement FindFirst(TreeScope treeScope, System.Windows.Automation.Condition condition)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return this.FindFirst(treeScope, condition, Retry.Time);
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition.
        /// </summary>
        public T FindFirst<T>(TreeScope treeScope, System.Windows.Automation.Condition condition, Func<AutomationElement, T> wrap)
            where T : UiElement
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            return this.FindFirst(treeScope, condition, wrap, Retry.Time);
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition within the given timeout period.
        /// </summary>
        public UiElement FindFirst(TreeScope treeScope, System.Windows.Automation.Condition condition, TimeSpan timeOut)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (this.TryFindFirst(treeScope, condition, timeOut, out var result))
            {
                return result;
            }

            throw new InvalidOperationException($"Did not find an element matching {condition.Description()}.");
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition within the given timeout period.
        /// </summary>
        public bool TryFindFirst(TreeScope treeScope, System.Windows.Automation.Condition condition, TimeSpan timeOut, [NotNullWhen(true)]out UiElement? result)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            result = null;
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, timeOut))
            {
                result = this.AutomationElement.FindFirst(treeScope, condition, FromAutomationElement);
                if (result != null)
                {
                    return true;
                }

                Wait.For(Retry.PollInterval);
            }

            return result != null;
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition within the given timeout period.
        /// </summary>
        public T FindFirst<T>(TreeScope treeScope, System.Windows.Automation.Condition condition, Func<AutomationElement, T> wrap, TimeSpan timeOut)
            where T : UiElement
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            if (this.TryFindFirst(treeScope, condition, wrap, timeOut, out var result))
            {
                return result;
            }

            throw new InvalidOperationException($"Did not find a {typeof(T).Name} matching {condition.Description()}.");
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition within the given timeout period.
        /// </summary>
        public bool TryFindFirst<T>(TreeScope treeScope, System.Windows.Automation.Condition condition, Func<AutomationElement, T> wrap, TimeSpan timeOut, [NotNullWhen(true)]out T? result)
            where T : UiElement
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            result = null;
            var start = DateTime.Now;
            do
            {
                if (this.AutomationElement.TryFindFirst(treeScope, condition, out var element))
                {
                    result = wrap(element);
                    return true;
                }

                Wait.For(Retry.PollInterval);
            }
            while (!Retry.IsTimeouted(start, timeOut));

            return result != null;
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition within the given timeout period.
        /// </summary>
        public UiElement FindAt(TreeScope treeScope, System.Windows.Automation.Condition condition, int index, TimeSpan timeOut)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (this.TryFindAt(treeScope, condition, index, timeOut, out var result))
            {
                return result;
            }

            throw new InvalidOperationException($"Did not find an element matching {condition.Description()} at index {index}.");
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition within the given timeout period.
        /// </summary>
        public bool TryFindAt(TreeScope treeScope, System.Windows.Automation.Condition condition, int index, TimeSpan timeOut, [NotNullWhen(true)]out UiElement? result)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            result = null;
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, timeOut))
            {
                result = this.AutomationElement.FindIndexed(treeScope, condition, index, FromAutomationElement);
                if (result != null)
                {
                    return true;
                }

                Wait.For(Retry.PollInterval);
            }

            return result != null;
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition within the given timeout period.
        /// </summary>
        public T FindAt<T>(TreeScope treeScope, System.Windows.Automation.Condition condition, int index, Func<AutomationElement, T> wrap, TimeSpan timeOut)
            where T : UiElement
        {
            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (this.TryFindAt(treeScope, condition, index, wrap, timeOut, out var result))
            {
                return result;
            }

            throw new InvalidOperationException($"Did not find an element matching {condition.Description()} at index {index}.");
        }

        /// <summary>
        /// Finds the first element which is in the given tree scope with the given condition within the given timeout period.
        /// </summary>
        public bool TryFindAt<T>(TreeScope treeScope, System.Windows.Automation.Condition condition, int index, Func<AutomationElement, T> wrap, TimeSpan timeOut, [NotNullWhen(true)] out T? result)
            where T : UiElement
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (wrap is null)
            {
                throw new ArgumentNullException(nameof(wrap));
            }

            result = null;
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, timeOut))
            {
                if (this.AutomationElement.TryFindIndexed(treeScope, condition, index, out var element))
                {
                    result = wrap(element);
                    return true;
                }

                Wait.For(Retry.PollInterval);
            }

            return result != null;
        }

        public UiElement FindFirstChild()
        {
            return this.FindFirst(TreeScope.Children, System.Windows.Automation.Condition.TrueCondition, Retry.Time);
        }

        public T FindFirstChild<T>(Func<AutomationElement, T> wrap)
            where T : UiElement
        {
            return this.FindFirst(TreeScope.Children, System.Windows.Automation.Condition.TrueCondition, wrap, Retry.Time);
        }

        public UiElement FindChildAt(int index)
        {
            return this.FindAt(TreeScope.Children, System.Windows.Automation.Condition.TrueCondition, index, Retry.Time);
        }

        public UiElement FindFirstChild(string automationId)
        {
            return this.FindFirst(TreeScope.Children, Conditions.ByAutomationId(automationId));
        }

        public UiElement FindFirstChild(System.Windows.Automation.Condition condition)
        {
            return this.FindFirst(TreeScope.Children, condition);
        }

        public T FindFirstChild<T>(System.Windows.Automation.Condition condition, Func<AutomationElement, T> wrap)
             where T : UiElement
        {
            return this.FindFirst(TreeScope.Children, condition, wrap);
        }

        public IReadOnlyList<UiElement> FindAllChildren()
        {
            return this.FindAll(TreeScope.Children, System.Windows.Automation.Condition.TrueCondition);
        }

        public IReadOnlyList<T> FindAllChildren<T>(Func<AutomationElement, T> wrap)
            where T : UiElement
        {
            return this.AutomationElement.FindAll(TreeScope.Children, System.Windows.Automation.Condition.TrueCondition, wrap);
        }

        public IReadOnlyList<T> FindAllChildren<T>(System.Windows.Automation.Condition condition, Func<AutomationElement, T> wrap)
            where T : UiElement
        {
            return this.AutomationElement.FindAll(TreeScope.Children, condition, wrap);
        }

        public IReadOnlyList<UiElement> FindAllChildren(System.Windows.Automation.Condition condition)
        {
            return this.AutomationElement.FindAll(TreeScope.Children, condition, FromAutomationElement);
        }

        public UiElement FindFirstDescendant()
        {
            return this.FindFirst(TreeScope.Descendants, System.Windows.Automation.Condition.TrueCondition);
        }

        public T FindFirstDescendant<T>(Func<AutomationElement, T> wrap)
            where T : UiElement
        {
            return this.FindFirst(TreeScope.Descendants, System.Windows.Automation.Condition.TrueCondition, wrap);
        }

        public UiElement FindFirstDescendant(string automationId)
        {
            return this.FindFirst(TreeScope.Descendants, Conditions.ByAutomationId(automationId));
        }

        public T FindFirstDescendant<T>(string automationId, Func<AutomationElement, T> wrap)
            where T : UiElement
        {
            return this.FindFirst(TreeScope.Descendants, Conditions.ByAutomationId(automationId), wrap);
        }

        public UiElement FindFirstDescendant(ControlType controlType)
        {
            return this.FindFirst(TreeScope.Descendants, Conditions.ByControlType(controlType));
        }

        public T FindFirstDescendant<T>(ControlType controlType, Func<AutomationElement, T> wrap)
            where T : UiElement
        {
            return this.FindFirst(TreeScope.Descendants, Conditions.ByControlType(controlType), wrap);
        }

        public UiElement FindFirstDescendant(System.Windows.Automation.Condition condition)
        {
            return this.FindFirst(TreeScope.Descendants, condition);
        }

        public IReadOnlyList<UiElement> FindAllDescendants()
        {
            return this.FindAll(TreeScope.Descendants, System.Windows.Automation.Condition.TrueCondition);
        }

        public IReadOnlyList<UiElement> FindAllDescendants(System.Windows.Automation.Condition condition)
        {
            return this.FindAll(TreeScope.Descendants, condition);
        }

        /// <summary>
        /// Finds the first element by looping thru all conditions.
        /// </summary>
        public UiElement? FindFirstNested(params System.Windows.Automation.Condition[] nestedConditions)
        {
            var currentElement = this;
            foreach (var condition in nestedConditions)
            {
                currentElement = currentElement.FindFirstChild(condition);
                if (currentElement is null)
                {
                    return null;
                }
            }

            return currentElement;
        }

        /// <summary>
        /// Finds all elements by looping thru all conditions.
        /// </summary>
        public IReadOnlyList<UiElement>? FindAllNested(params System.Windows.Automation.Condition[] nestedConditions)
        {
            var currentElement = this;
            for (var i = 0; i < nestedConditions.Length - 1; i++)
            {
                var condition = nestedConditions[i];
                currentElement = currentElement.FindFirstChild(condition);
                if (currentElement is null)
                {
                    return null;
                }
            }

            return currentElement.FindAllChildren(nestedConditions.Last());
        }

        /// <summary>
        /// Finds for the first item which matches the given xpath.
        /// </summary>
        public UiElement? FindFirstByXPath(string xPath)
        {
            var xPathNavigator = new AutomationElementXPathNavigator(this);
            var nodeItem = xPathNavigator.SelectSingleNode(xPath);
            return (UiElement?)nodeItem?.UnderlyingObject;
        }

        /// <summary>
        /// Finds all items which match the given xpath.
        /// </summary>
        public IReadOnlyList<UiElement> FindAllByXPath(string xPath)
        {
            var xPathNavigator = new AutomationElementXPathNavigator(this);
            var itemNodeIterator = xPathNavigator.Select(xPath);
            var itemList = new List<UiElement>();
            while (itemNodeIterator.MoveNext())
            {
                var automationItem = (UiElement)itemNodeIterator.Current.UnderlyingObject;
                itemList.Add(automationItem);
            }

            return itemList.ToArray();
        }

#pragma warning disable CA1822
        public System.Windows.Automation.Condition CreateCondition(System.Windows.Automation.Condition controlTypeCondition, string? name)
#pragma warning restore CA1822
        {
            if (name is null)
            {
                return controlTypeCondition;
            }

            return new AndCondition(
                controlTypeCondition,
                Conditions.ByNameOrAutomationId(name));
        }
    }
}
