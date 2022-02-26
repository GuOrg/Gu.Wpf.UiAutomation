namespace Gu.Wpf.UiAutomation
{
    public partial class UiElement
    {
        public Button AsButton() => new(this.AutomationElement);

        public CheckBox AsCheckBox() => new(this.AutomationElement);

        public ToggleButton AsToggleButton() => new(this.AutomationElement);

        public ComboBox AsComboBox() => new(this.AutomationElement);

        public Label AsLabel() => new(this.AutomationElement);

        public TextBlock AsTextBlock() => new(this.AutomationElement);

        public GroupBox AsGroupBox() => new(this.AutomationElement);

        public Expander AsExpander() => new(this.AutomationElement);

        public ListBox AsListBox() => new(this.AutomationElement);

        public ListView AsListView() => new(this.AutomationElement);

        public DataGrid AsDataGrid() => new(this.AutomationElement);

        public ListBoxItem AsListBoxItem() => new(this.AutomationElement);

        public GridViewCell AsGridCell() => new(FromAutomationElement(this.AutomationElement));

        public Menu AsMenu() => new(this.AutomationElement);

        public ContextMenu AsContextMenu() => new(this.AutomationElement);

        public MenuItem AsMenuItem() => new(this.AutomationElement);

        public ProgressBar AsProgressBar() => new(this.AutomationElement);

        public RadioButton AsRadioButton() => new(this.AutomationElement);

        public Slider AsSlider() => new(this.AutomationElement);

        public TabControl AsTabControl() => new(this.AutomationElement);

        public TabItem AsTabItem() => new(this.AutomationElement);

        public TextBox AsTextBox() => new(this.AutomationElement);

        public Thumb AsThumb() => new(this.AutomationElement);

        public TitleBar AsTitleBar() => new(this.AutomationElement);

        public TreeView AsTreeView() => new(this.AutomationElement);

        public TreeViewItem AsTreeViewItem() => new(this.AutomationElement);

        public HorizontalScrollBar AsHorizontalScrollBar() => new(this.AutomationElement);

        public VerticalScrollBar AsVerticalScrollBar() => new(this.AutomationElement);

        public Window AsWindow(bool isMainWindow) => new(this.AutomationElement, isMainWindow);

        public MessageBox AsMessageBox() => new(this.AutomationElement);
    }
}
