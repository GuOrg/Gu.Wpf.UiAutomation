# Gu.Wpf.UiAutomation

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md)
[![NuGet](https://img.shields.io/nuget/v/Gu.Wpf.UiAutomation.svg)](https://www.nuget.org/packages/Gu.Wpf.UiAutomation/)
[![Build status](https://ci.appveyor.com/api/projects/status/wpxtooew9wicyuqa/branch/master?svg=true)](https://ci.appveyor.com/project/JohanLarsson/gu-wpf-uiautomation/branch/master)


## Introduction
Gu.Wpf.UiAutomation is a .NET library which helps with automated UI testing of WPF applications.<br />
The code is based on [FlaUI](https://github.com/Roemer/FlaUI) that is based on native UI Automation libraries from Microsoft and therefore kind of a wrapper around them.<br />
Gu.Wpf.UiAutomation wraps almost everything from the UI Automation libraries but also provides the native objects in case someone has a special need which is not covered (yet) by Gu.Wpf.UiAutomation.<br />
Some ideas are copied from the UIAComWrapper project or TestStack.White but rewritten from scratch to have a clean codebase.

The reason for this library is to shape the API to match WPF's types and names.

### Usage in Code
The entry point is usually an application or the desktop so you get an automation element (like a the main window of the application).
On this, you can then search sub-elements and interact with them.
There is a helper class to launch, attach or close applications.
Since the application is not related to any UIA library, you need to create the automation you want and use it to get your first element, which then is your entry point.

```csharp
private static readonly string ExeFileName = Application.FindExe("WpfApplication.exe");

[Test]
public void IsChecked()
{
    using (var app = Application.Launch(ExeFileName))
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

```csharp
private static readonly string ExeFileName = Application.FindExe("WpfApplication.exe");

[Test]
public void IsChecked()
{
    using (var app = Application.Launch(ExeFileName))
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
private static readonly string ExeFileName = Application.FindExe("WpfApplication.exe");

[SetUp]
public void SetUp()
{
    if (Application.TryAttach(ExeFileName, "ButtonWindow", out var app))
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
    Application.KillLaunched(ExeFileName);
}

[TestCase("AutomationId", "AutomationProperties.AutomationId")]
[TestCase("XName", "x:Name")]
[TestCase("Content", "Content")]
public void Content(string key, string expected)
{
    using (var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow"))
    {
        var window = app.MainWindow;
        var button = window.FindButton(key);
        Assert.AreEqual(expected, button.Content.AsTextBlock().Text);
    }
}
```

#### Arguments
Launch and AttachOrLaunch has an overload that takes an argument string. It can be used like this:

```cs
private static readonly string ExeFileName = Application.FindExe("WpfApplication.exe");

[OneTimeTearDown]
public void OneTimeTearDown()
{
    Application.KillLaunched(ExeFileName);
}

[Test]
public void SelectByIndex()
{
    using (var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow"))
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
    using (var app = Application.Launch(Application.FindExe("Gu.Wpf.Adorners.Demo.exe"), "WatermarkWindow"))
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

### Contribution
Feel free to fork Gu.Wpf.UiAutomation and send pull requests of your modifications.<br />
You can also create issues if you find problems or have ideas on how to further improve Gu.Wpf.UiAutomation.
