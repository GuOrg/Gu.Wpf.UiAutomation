namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using System.Linq;
    using System.Windows.Automation;
    using NUnit.Framework;

    [Explicit("Script")]
    public class SandboxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void DumpTypes()
        {
            foreach (var type in typeof(UiElement).Assembly
                                                  .GetTypes()
                                                  .Where(x => typeof(UiElement).IsAssignableFrom(x)))
            {
                Console.WriteLine($"- {type.Name}");
            }
        }

        [Test]
        public void DumpButton()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var toggleButton = window.FindButton();
                DumpRecursive(toggleButton.AutomationElement);
            }
        }

        [Test]
        public void DumpDataGrid()
        {
            using (var app = Application.Launch(ExeFileName, "SingleDataGridWindow"))
            {
                var window = app.MainWindow;
                DumpRecursive(window.FindDataGrid().AutomationElement);
            }
        }

        [Test]
        public void DumpDataGrid10()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                DumpRecursive(window.FindDataGrid("DataGrid10").AutomationElement);
            }
        }

        [Test]
        public void DumpTabControl()
        {
            using (var app = Application.Launch(ExeFileName, "TabControlWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindTabControl().AutomationElement;
                DumpRecursive(element);
            }
        }

        [Test]
        public void DumpTabItem()
        {
            using (var app = Application.Launch(ExeFileName, "TabControlWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindTabControl().Items[0].AutomationElement;
                DumpRecursive(element);
            }
        }

        [Test]
        public void DumpGroupBox()
        {
            using (var app = Application.Launch(ExeFileName, "GroupBoxWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindGroupBox().AutomationElement;
                DumpRecursive(element);
            }
        }

        [Test]
        public void DumpExpander()
        {
            using (var app = Application.Launch(ExeFileName, "ExpanderWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindExpander().AutomationElement;
                DumpRecursive(element);
            }
        }

        [Test]
        public void DumpWindow()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                var element = window.AutomationElement;
                DumpRecursive(element);
            }
        }

        [Test]
        public void DumpDataGridItem()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                DumpRecursive(window.FindDataGrid()[0, 0].AutomationElement);
            }
        }

        [Test]
        public void DumpListBox()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                DumpRecursive(window.FindListBox().AutomationElement);
            }
        }

        [Test]
        public void DumpListBox10()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                DumpRecursive(window.FindListBox("ListBox10").AutomationElement);
            }
        }

        [Test]
        public void DumpListView()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow;
                DumpRecursive(window.FindListView().AutomationElement);
            }
        }

        [Test]
        public void DumpMessageBox()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show MessageBox OKCancel").Click();
                var messageBox = window.FindMessageBox();
                var element = messageBox.AutomationElement;
                DumpRecursive(element);
                messageBox.Close();
            }
        }

        [Test]
        public void DumpScrollBarWindow()
        {
            using (var app = Application.Launch(ExeFileName, "ScrollBarWindow"))
            {
                var window = app.MainWindow;
                DumpRecursive(window.AutomationElement, allPropertiesAndPatterns: true);
            }
        }

        [Test]
        public void DumpToggleButton()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow;
                var toggleButton = window.FindToggleButton();
                DumpRecursive(toggleButton.AutomationElement, allPropertiesAndPatterns: true);
            }
        }

        private static void DumpRecursive(AutomationElement element, bool allPropertiesAndPatterns = false, string padding = "")
        {
            DumpPropertiesAndPatterns(element, allPropertiesAndPatterns, padding);
            foreach (var child in element.Children())
            {
                DumpRecursive(child, allPropertiesAndPatterns, padding + "  ");
            }
        }

        private static void DumpPropertiesAndPatterns(AutomationElement element, bool all = true, string padding = "")
        {
            if (all)
            {
                foreach (var pattern in element.GetSupportedPatterns())
                {
                    Console.WriteLine($"{padding}{pattern.ProgrammaticName}");
                    var currentPattern = element.GetCurrentPattern(pattern);
                    Console.WriteLine($"{padding}{currentPattern}");
                    var currentProperty = currentPattern.GetType().GetProperty("Current");
                    if (currentProperty != null)
                    {
                        var value = currentProperty.GetValue(currentPattern);
                        foreach (var property in value.GetType().GetProperties())
                        {
                            Console.WriteLine($"{padding}{property.Name} {property.GetValue(value)}");
                        }
                    }
                }

                foreach (var property in element.GetSupportedProperties().OrderBy(x => x.ProgrammaticName))
                {
                    Console.WriteLine($"{padding}{property.ProgrammaticName.TrimStart("AutomationElementIdentifiers.").TrimEnd("Property")} {element.GetCurrentPropertyValue(property)}");
                }

                Console.WriteLine();
            }
            else
            {
                var info = element.Current;
                Console.WriteLine($"{padding}ControlType: {info.ControlType.ProgrammaticName} (LocalizedControlType: {info.LocalizedControlType})");
                Console.WriteLine($"{padding}ClassName: {info.ClassName}");
                Console.WriteLine($"{padding}Name: {info.Name}");
                Console.WriteLine($"{padding}AutomationId: {info.AutomationId}");
                Console.WriteLine($"{padding}IsContentElement: {info.IsContentElement} IsControlElement: {info.IsControlElement}");
                Console.WriteLine($"{padding}Properties: {string.Join(", ", element.GetSupportedProperties().Select(x => x.ProgrammaticName.TrimStart("AutomationElementIdentifiers.").TrimEnd("Property")).OrderBy(x => x))}");
                Console.WriteLine($"{padding}Patterns: {string.Join(", ", element.GetSupportedPatterns().Select(x => x.ProgrammaticName.TrimEnd("Identifiers.Pattern").TrimEnd("Pattern")).OrderBy(x => x))}");
                Console.WriteLine();
            }
        }
    }
}
