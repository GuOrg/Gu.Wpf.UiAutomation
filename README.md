# Gu.Wpf.UiAutomation

[![Join the chat at https://gitter.im/JohanLarsson/Gu.Wpf.UiAutomation](https://badges.gitter.im/JohanLarsson/Gu.Wpf.UiAutomation.svg)](https://gitter.im/JohanLarsson/Gu.Wpf.UiAutomation?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md)
[![NuGet](https://img.shields.io/nuget/v/Gu.Wpf.UiAutomation.svg)](https://www.nuget.org/packages/Gu.Wpf.UiAutomation/)
[![Build status](https://ci.appveyor.com/api/projects/status/wpxtooew9wicyuqa/branch/master?svg=true)](https://ci.appveyor.com/project/Gu.Wpf.UiAutomation/gu-wpf-uiautomation/branch/master)


## Introduction
Gu.Wpf.UiAutomation is a .NET library which helps with automated UI testing of WPF applications.
The library wraps `UIAutomationClient` and tries to provide an API that is nice for WPF.

The code inspired by [FlaUI](https://github.com/Roemer/FlaUI) and [White](https://github.com/TestStack/White).
Tested on Windows 7, Windows 10, and the default AppVeyor image.

## Supported types


- Button
- Calendar
- CalendarDayButton
- CheckBox
- ColumnHeader
- ComboBox
- ComboBoxItem
- ContentControl
- ContextMenu
- Control
- DataGrid
- DataGridCell
- DataGridColumnHeader
- DataGridColumnHeadersPresenter
- DataGridDetailsPresenter
- DataGridRow
- DataGridRowHeader
- DatePicker
- Dialog
- Expander
- Frame
- GridHeader
- GridSplitter
- GridViewCell
- GridViewColumnHeader
- GridViewHeaderRowPresenter
- GridViewRowHeader
- GroupBox
- HeaderedContentControl
- HorizontalScrollBar
- Label
- ListBox
- ListBoxItem
- ListView
- ListViewItem
- Menu
- MenuBar
- MenuItem
- MessageBox
- MultiSelector`1
- OpenFileDialog
- PasswordBox
- ExpandCollapseControl
- InvokeControl
- SelectionItemControl
- Popup
- ProgressBar
- RadioButton
- RepeatButton
- RichTextBox
- RowHeader
- SaveFileDialog
- ScrollBar
- ScrollViewer
- Selector
- Selector`1
- Separator
- Slider
- StatusBar
- TabControl
- TabItem
- TextBlock
- TextBox
- TextBoxBase
- Thumb
- TitleBar
- ToggleButton
- ToolBar
- ToolTip
- TreeView
- TreeViewItem
- UiElement
- UserControl
- VerticalScrollBar
- Window


### Usage in Code
The entry point is usually an application or the desktop so you get an automation element (like a the main window of the application).
On this, you can then search sub-elements and interact with them.
The `Application` class is a helper for launching, attaching and closing applications.
Since the application is not related to any UIA library, you need to create the automation you want and use it to get your first element, which then is your entry point.

```cs
[Test]
public void CheckBoxIsChecked()
{
    using (var app = Application.Launch("WpfApplication.exe"))
    {
        var window = app.MainWindow;
        var checkBox = window.FindCheckBox("Test Checkbox");
        checkBox.IsChecked = true;
        Assert.AreEqual(true, checkBox.IsChecked);
    }
}
```

### Application
The application class iss the way to start an application to test. There are a couple of factory methods.

#### Launch
Starts a new instance of the application and closes it on dispose. There is a flag to leave the app open but the default is close on dispose.
Launch is useful for tests that mutate state where resetting can be slow and painful.

```cs
[Test]
public void IsChecked()
{
    using (var app = Application.Launch("WpfApplication.exe"))
    {
        var window = app.MainWindow;
        var checkBox = window.FindCheckBox("Test Checkbox");
        checkBox.IsChecked = true;
        Assert.AreEqual(true, checkBox.IsChecked);
    }
}
```

#### Attach
Attaches to a running process and leaves it open when disposing disposing by default.

#### AttachOrLaunch
Attaches to a running process or launches a new if not found and leaves it open when disposing by default.

```cs
[SetUp]
public void SetUp()
{
    if (Application.TryAttach("WpfApplication.exe", "ButtonWindow", out var app))
    {
        using (app)
        {
            app.MainWindow.FindButton("Reset").Invoke();
        }
    }
}

[OneTimeTearDown]
public void OneTimeTearDown()
{
    Application.KillLaunched("WpfApplication.exe");
}

[TestCase("AutomationId", "AutomationProperties.AutomationId")]
[TestCase("XName", "x:Name")]
[TestCase("Content", "Content")]
public void Content(string key, string expected)
{
    using (var app = Application.AttachOrLaunch("WpfApplication.exe", "ButtonWindow"))
    {
        var window = app.MainWindow;
        var button = window.FindButton(key);
        Assert.AreEqual(expected, ((TextBlock)button.Content).Text);
    }
}
```

#### Arguments
Launch and AttachOrLaunch has an overload that takes an argument string. It can be used like this:

```cs
[OneTimeTearDown]
public void OneTimeTearDown()
{
    Application.KillLaunched("WpfApplication.exe");
}

[Test]
public void SelectByIndex()
{
    using (var app = Application.AttachOrLaunch("WpfApplication.exe", "ListBoxWindow"))
    {
        var window = app.MainWindow;
        var listBox = window.FindListBox("BoundListBox");
        Assert.AreEqual(2, listBox.Items.Count);
        Assert.IsInstanceOf<ListBoxItem>(listBox.Items[0]);
        Assert.IsInstanceOf<ListBoxItem>(listBox.Items[1]);
        Assert.IsNull(listBox.SelectedItem);

        var item = listBox.Select(0);
        Assert.AreEqual("Johan", item.FindTextBlock().Text);
        Assert.AreEqual("Johan", listBox.SelectedItem.FindTextBlock().Text);

        item = listBox.Select(1);
        Assert.AreEqual("Erik", item.FindTextBlock().Text);
        Assert.AreEqual("Erik", listBox.SelectedItem.FindTextBlock().Text);
    }
}
```

```cs
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        if (e.Args.Length == 1)
        {
            var window = e.Args[0];
            this.StartupUri = new Uri($"Windows/{window}.xaml", UriKind.Relative);
        }

        base.OnStartup(e);
    }
}
```

## ImageAssert
For asserting using an expected image of how the control will render.

```cs
[Test]
public void DefaultAdornerWhenNotFocused()
{
    using (var app = Application.Launch("Gu.Wpf.Adorners.Demo.exe", "WatermarkWindow"))
    {
        var window = app.MainWindow;
        var textBox = window.FindTextBox("WithDefaultAdorner");
        ImageAssert.AreEqual(".\\Images\\WithDefaultAdorner_not_focused.png", textBox);
    }
}
```

For image asserts to work on build servers forcing a theme may be needed:

```xml
<Window.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/PresentationFramework.Classic;V4.0.0.0;31bf3856ad364e35;component/themes/Classic.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Window.Resources>
```
### OnFail
Convenience property for saving the actual image to %Temp%

```cs
[OneTimeSetUp]
public void OneTimeSetUp()
{
    ImageAssert.OnFail = OnFail.SaveImageToTemp;
}
```

And in `appveyor.yml`
```
on_failure:
  - ps: Get-ChildItem $env:temp\*.png | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }
```

## AppVeyor
Troubleshooting failing UI-tests on AppVeyor is tricky. Here is a snippet that can be used for getting a screenshot of what things look like.

```cs
[Test]
public void SomeTest()
{
    try
    {
        using (var app = Application.AttachOrLaunch("SomeApp.exe"))
        {
            ...
        }
    }
    catch (TimeoutException)
    {
        Capture.ScreenToFile(Path.Combine(Path.GetTempPath(), "SomeTest.png"));
        throw;
    }
}
```

And in `appveyor.yml`
```
on_failure:
  - ps: Get-ChildItem $env:temp\*.png | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }
```

### Contribution
Feel free to fork Gu.Wpf.UiAutomation and send pull requests of your modifications.<br />
You can also create issues if you find problems or have ideas on how to further improve Gu.Wpf.UiAutomation.
