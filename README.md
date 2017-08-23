# Gu.Wpf.UiAutomation

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md)
[![Build status](https://ci.appveyor.com/api/projects/status/wpxtooew9wicyuqa/branch/master?svg=true)](https://ci.appveyor.com/project/JohanLarsson/gu-wpf-uiautomation/branch/master)


### Introduction
Gu.Wpf.UiAutomation is a .NET library which helps with automated UI testing of WPF applications.<br />
The code is based on [FlaUI](https://github.com/Roemer/FlaUI) that is based on native UI Automation libraries from Microsoft and therefore kind of a wrapper around them.<br />
Gu.Wpf.UiAutomation wraps almost everything from the UI Automation libraries but also provides the native objects in case someone has a special need which is not covered (yet) by Gu.Wpf.UiAutomation.<br />
Some ideas are copied from the UIAComWrapper project or TestStack.White but rewritten from scratch to have a clean codebase.

The reason for this library is to shape the API to match WPF's types and names.

##### Usage in Code
The entry point is usually an application or the desktop so you get an automation element (like a the main window of the application).
On this, you can then search sub-elements and interact with them.
There is a helper class to launch, attach or close applications.
Since the application is not related to any UIA library, you need to create the automation you want and use it to get your first element, which then is your entry point.
```csharp
private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

[Test]
public void IsChecked()
{
    using (var app = Application.Launch(ExeFileName))
    {
        var window = app.MainWindow();
        var checkBox = window.FindCheckBox("Test Checkbox");
        checkBox.IsChecked = true;
        Assert.AreEqual(true, checkBox.IsChecked);
    }
}
```
```csharp
var app = Application.Launch("calc.exe");
using (var automation = new UIA3Automation())
{
    var window = app.GetMainWindow(automation);
    var button1 = window.FindFirstDescentant(cf => cf.ByText("1"))?.AsButton();
    button1?.Invoke();
    ...
}
```

### Contribution
Feel free to fork Gu.Wpf.UiAutomation and send pull requests of your modifications.<br />
You can also create issues if you find problems or have ideas on how to further improve Gu.Wpf.UiAutomation.
