# 0.2.3
* FEATURE: Retry in ImageAssert

# 0.2.2
* FEATURE: Calendar
* FEATURE: CalendarDayButton
* FEATURE: Frame
* FEATURE: GridSplitter
* FEATURE: OpenFileDialog
* FEATURE: PasswordBox
* FEATURE: SaveFileDialog
* FEATURE: Separator

# 0.2.1.1
* BUGFIX: Selector.Select() virtualized items.

# 0.2.1
* FEATURE: Handle virtualization for ListBox.Items.
* FEATURE: UiElement.Children for displaying trees.
* BUGFIX: Handle empty data grids.
* BREAKING: Remove DataGrid.RowHeaders use the Rows headers.
* BREAKING: Rename Conditions, was Condition, confusing when same name as System.Windows.Automation.Condition
* BREAKING: rename ScrollBar.Minimum & Maximum to match WPF property names.
* BREAKING: Remove GridView, refactor DataGrid & ListView

# 0.2.0
* FEATURE: Added more wrapper types.
* FEATURE: Return better types to enable cast instead of using As-methods.
* BREAKING: Use UIAutomationClient & UIAutomationTypes instead of interop assembly.
* BREAKING: Rename HeaderedContentControl.HeaderText was Text
* BREAKING: Rename UiElement was AutomationElement
* BREAKING: Remove condition types, use built-in conditions.
* BREAKING: Removed some exception types and use built-in instead.
* BREAKING: Remove UIElement.ExecuteInPattern
* BREAKING: Minor changes to inheritance hierarchies.
* BREAKING: event handler signatures, use built-in delegates.

# 0.1.17
* FEATURE: Wait for Aero animation.

# 0.1.16
* BUGFIX: ImageAssert handle bmp.

# 0.1.15
* FEATURE: ImageAssert.OnFail save when not found.
* FEATURE: ImageAssert handle bmp.

# 0.1.14
* FEATURE: ImageAssert.OnFail.

# 0.1.13
* FEATURE: ImageAssert with onerror overloads.

# 0.1.12
* BUGFIX: ImageAssert relative paths and resources.

# 0.1.11
* FEATURE: Application.FindExe

# 0.1.10
* FEATURE: Find TextBlock in template
* FEATURE: GridView.RowHeader(index)
* BUGFIX: GridView.ColumnHeaders on Windows 7.
* FEATURE: Application.TryWithAttahced

# 0.1.9
* FEATURE: HeaderedContentControl
* FEATURE: Expander : HeaderedContentControl
* FEATURE: GroupBox : HeaderedContentControl
* FEATURE: Application.TryAttach

# 0.1.8
* FEATURE: Datagrid handle input in template columns.
* FEATURE: Find overloads.
* FEATURE: ComboBox.IsEditable & IsReadOnly
* FEATURE: UserControl
* FEATURE: TextBox.IsReadOnly
* FEATURE: Application.Kill
* FEATURE: Application.AttachOrLaunch with argument.
* FEATURE: ListBox.Select by text.

# 0.1.7
* BREAKING: Rename capture.
* BREAKING: Refactor overlay API.
* FEATURE: ImageAssert & ImageDiffWindow
* FEATURE: FindChildAt
* FEATURE: FindXx with func overloads.
* FEATURE: TreeViewItem.IsExpanded
* FEATURE: GridCell.Enter handle invalid value.
* FEATURE: Button.Text & Content.

# 0.1.6
* BREAKING: Don't throw in FindAll
* BREAKING: Use TimeSpan in Draw.
* BREAKING: Throw if click or enter when off screen.
* BREAKING: Rename bounds.
* FEATURE: ComboBox.Enter
* FEATURE: GridView cell selection
* FEATURE: ClearFocus
* FEATURE: AutomationElement.Window

# 0.1.5
* BUGFIX: FindListBox
* FEATURE: HelpText property.

# 0.1.4
* FEATURE: MessageBox
* FEATURE: Popup
* FEATURE: ContextMenu
* FEATURE: TabControl
* Bugfixes DataGrid
* FEATURE: ActualWidth & ActualHeight
* FEATURE: IsKeyboardFocusable & HasKeyboardFocus
* FEATURE: Better performance TreeView
* FEATURE: Throw when child is not found.
* FEATURE: Application.MainWindow, cached.

# 0.1.3
* FEATURE: GridCell.Value
* BUGFIX: GridCell.Select on Win 7
* BUGFIX: Handle RowHeader.Text with ContentStringFormat
* Feature GridView.RowHeaders & ColumnHeaders.