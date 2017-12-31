namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using System.Windows.Automation;
    using NUnit.Framework;
    using Condition = Gu.Wpf.UiAutomation.Condition;

    public class SandboxTests
    {
        [Test]
        public void Dump()
        {
            using (var app = Application.Launch("WpfApplication.exe", "ToolBarWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindFirst(TreeScope.Children, Condition.ToolBar);
                Console.WriteLine($"ControlType: {element.ControlType.ProgrammaticName}");
                Console.WriteLine($"LocalizedControlType: {element.LocalizedControlType}");
                Console.WriteLine($"ClassName: {element.ClassName}");
                Console.WriteLine($"IsContentElement: {element.AutomationElement.Current.IsContentElement}");
                Console.WriteLine($"IsControlElement: {element.AutomationElement.Current.IsControlElement}");
            }
        }
    }
}
