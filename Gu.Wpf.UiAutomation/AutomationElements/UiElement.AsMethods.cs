namespace Gu.Wpf.UiAutomation
{
    public partial class UiElement
    {
        public Button AsButton() => new Button(this.AutomationElement);

        public CheckBox AsCheckBox() => new CheckBox(this.AutomationElement);

        public ToggleButton AsToggleButton() => new ToggleButton(this.AutomationElement);

        public ComboBox AsComboBox() => new ComboBox(this.AutomationElement);

        public Label AsLabel() => new Label(this.AutomationElement);

        public TextBlock AsTextBlock() => new TextBlock(this.AutomationElement);

        public GroupBox AsGroupBox() => new GroupBox(this.AutomationElement);

        public Expander AsExpander() => new Expander(this.AutomationElement);

        public GridRow AsGridRow() => new GridRow(this.AutomationElement);

        public ListBox AsListBox() => new ListBox(this.AutomationElement);

        public ListView AsListView() => new ListView(this.AutomationElement);

        public DataGrid AsDataGrid() => new DataGrid(this.AutomationElement);

        public ListBoxItem AsListBoxItem() => new ListBoxItem(this.AutomationElement);

        public GridCell AsGridCell() => new GridCell(this.AutomationElement);

        public Menu AsMenu() => new Menu(this.AutomationElement);

        public ContextMenu AsContextMenu() => new ContextMenu(this.AutomationElement);

        public MenuItem AsMenuItem() => new MenuItem(this.AutomationElement);

        public ProgressBar AsProgressBar() => new ProgressBar(this.AutomationElement);

        public RadioButton AsRadioButton() => new RadioButton(this.AutomationElement);

        public Slider AsSlider() => new Slider(this.AutomationElement);

        public TabControl AsTabControl() => new TabControl(this.AutomationElement);

        public TabItem AsTabItem() => new TabItem(this.AutomationElement);

        public TextBox AsTextBox() => new TextBox(this.AutomationElement);

        public Thumb AsThumb() => new Thumb(this.AutomationElement);

        public TitleBar AsTitleBar() => new TitleBar(this.AutomationElement);

        public TreeView AsTreeView() => new TreeView(this.AutomationElement);

        public TreeViewItem AsTreeViewItem() => new TreeViewItem(this.AutomationElement);

        public HorizontalScrollBar AsHorizontalScrollBar() => new HorizontalScrollBar(this.AutomationElement);

        public VerticalScrollBar AsVerticalScrollBar() => new VerticalScrollBar(this.AutomationElement);

        public Window AsWindow(bool isMainWindow) => new Window(this.AutomationElement, isMainWindow);

        public MessageBox AsMessageBox() => new MessageBox(this.AutomationElement);

    }
}