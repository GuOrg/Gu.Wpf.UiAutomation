namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using System.Windows.Automation;
    using NUnit.Framework;

    [Explicit("Script")]
    public class SandboxTests
    {
        [Test]
        public void Dump()
        {
            using (var app = Application.Launch("WpfApplication.exe", "DatePickerWindow"))
            {
                var window = app.MainWindow;
                var element = window.AutomationElement.FindIndexed(TreeScope.Children, Condition.TrueCondition, 1);
                Console.WriteLine($"ControlType: {element.Current.ControlType.ProgrammaticName}");
                Console.WriteLine($"LocalizedControlType: {element.Current.LocalizedControlType}");
                Console.WriteLine($"ClassName: {element.Current.ClassName}");
                Console.WriteLine($"IsContentElement: {element.Current.IsContentElement}");
                Console.WriteLine($"IsControlElement: {element.Current.IsControlElement}");

                foreach (var pattern in element.GetSupportedPatterns())
                {
                    Console.WriteLine(pattern.ProgrammaticName);
                }

                foreach (var property in element.GetSupportedProperties())
                {
                    Console.WriteLine(property.ProgrammaticName);
                }
            }
        }
    }
}
