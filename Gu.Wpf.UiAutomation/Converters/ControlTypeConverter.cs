namespace Gu.Wpf.UiAutomation.UIA3.Converters
{
    using System;

    public static class ControlTypeConverter
    {
        public static object ToControlType(object nativeControlType)
        {
            switch ((int)nativeControlType)
            {
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_AppBarControlTypeId:
                    return ControlType.AppBar;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ButtonControlTypeId:
                    return ControlType.Button;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_CalendarControlTypeId:
                    return ControlType.Calendar;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_CheckBoxControlTypeId:
                    return ControlType.CheckBox;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ComboBoxControlTypeId:
                    return ControlType.ComboBox;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_CustomControlTypeId:
                    return ControlType.Custom;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_DataGridControlTypeId:
                    return ControlType.DataGrid;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_DataItemControlTypeId:
                    return ControlType.DataItem;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_DocumentControlTypeId:
                    return ControlType.Document;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_EditControlTypeId:
                    return ControlType.Edit;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_GroupControlTypeId:
                    return ControlType.Group;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_HeaderControlTypeId:
                    return ControlType.Header;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_HeaderItemControlTypeId:
                    return ControlType.HeaderItem;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_HyperlinkControlTypeId:
                    return ControlType.Hyperlink;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ImageControlTypeId:
                    return ControlType.Image;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ListControlTypeId:
                    return ControlType.List;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ListItemControlTypeId:
                    return ControlType.ListItem;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_MenuBarControlTypeId:
                    return ControlType.MenuBar;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_MenuControlTypeId:
                    return ControlType.Menu;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_MenuItemControlTypeId:
                    return ControlType.MenuItem;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_PaneControlTypeId:
                    return ControlType.Pane;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ProgressBarControlTypeId:
                    return ControlType.ProgressBar;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_RadioButtonControlTypeId:
                    return ControlType.RadioButton;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ScrollBarControlTypeId:
                    return ControlType.ScrollBar;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SemanticZoomControlTypeId:
                    return ControlType.SemanticZoom;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SeparatorControlTypeId:
                    return ControlType.Separator;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SliderControlTypeId:
                    return ControlType.Slider;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SpinnerControlTypeId:
                    return ControlType.Spinner;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SplitButtonControlTypeId:
                    return ControlType.SplitButton;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_StatusBarControlTypeId:
                    return ControlType.StatusBar;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TabControlTypeId:
                    return ControlType.Tab;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TabItemControlTypeId:
                    return ControlType.TabItem;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TableControlTypeId:
                    return ControlType.Table;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TextControlTypeId:
                    return ControlType.Text;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ThumbControlTypeId:
                    return ControlType.Thumb;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TitleBarControlTypeId:
                    return ControlType.TitleBar;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ToolBarControlTypeId:
                    return ControlType.ToolBar;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ToolTipControlTypeId:
                    return ControlType.ToolTip;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TreeControlTypeId:
                    return ControlType.Tree;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TreeItemControlTypeId:
                    return ControlType.TreeItem;
                case Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_WindowControlTypeId:
                    return ControlType.Window;
                default:
                    throw new NotSupportedException();
            }
        }

        public static object ToControlTypeNative(ControlType controlType)
        {
            switch (controlType)
            {
                case ControlType.AppBar:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_AppBarControlTypeId;
                case ControlType.Button:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ButtonControlTypeId;
                case ControlType.Calendar:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_CalendarControlTypeId;
                case ControlType.CheckBox:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_CheckBoxControlTypeId;
                case ControlType.ComboBox:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ComboBoxControlTypeId;
                case ControlType.Custom:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_CustomControlTypeId;
                case ControlType.DataGrid:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_DataGridControlTypeId;
                case ControlType.DataItem:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_DataItemControlTypeId;
                case ControlType.Document:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_DocumentControlTypeId;
                case ControlType.Edit:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_EditControlTypeId;
                case ControlType.Group:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_GroupControlTypeId;
                case ControlType.Header:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_HeaderControlTypeId;
                case ControlType.HeaderItem:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_HeaderItemControlTypeId;
                case ControlType.Hyperlink:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_HyperlinkControlTypeId;
                case ControlType.Image:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ImageControlTypeId;
                case ControlType.List:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ListControlTypeId;
                case ControlType.ListItem:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ListItemControlTypeId;
                case ControlType.MenuBar:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_MenuBarControlTypeId;
                case ControlType.Menu:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_MenuControlTypeId;
                case ControlType.MenuItem:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_MenuItemControlTypeId;
                case ControlType.Pane:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_PaneControlTypeId;
                case ControlType.ProgressBar:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ProgressBarControlTypeId;
                case ControlType.RadioButton:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_RadioButtonControlTypeId;
                case ControlType.ScrollBar:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ScrollBarControlTypeId;
                case ControlType.SemanticZoom:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SemanticZoomControlTypeId;
                case ControlType.Separator:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SeparatorControlTypeId;
                case ControlType.Slider:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SliderControlTypeId;
                case ControlType.Spinner:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SpinnerControlTypeId;
                case ControlType.SplitButton:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_SplitButtonControlTypeId;
                case ControlType.StatusBar:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_StatusBarControlTypeId;
                case ControlType.Tab:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TabControlTypeId;
                case ControlType.TabItem:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TabItemControlTypeId;
                case ControlType.Table:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TableControlTypeId;
                case ControlType.Text:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TextControlTypeId;
                case ControlType.Thumb:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ThumbControlTypeId;
                case ControlType.TitleBar:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TitleBarControlTypeId;
                case ControlType.ToolBar:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ToolBarControlTypeId;
                case ControlType.ToolTip:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_ToolTipControlTypeId;
                case ControlType.Tree:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TreeControlTypeId;
                case ControlType.TreeItem:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_TreeItemControlTypeId;
                case ControlType.Window:
                    return Interop.UIAutomationClient.UIA_ControlTypeIds.UIA_WindowControlTypeId;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
