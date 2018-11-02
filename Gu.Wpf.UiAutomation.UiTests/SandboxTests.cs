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
                Dump.Recursive(window.FindButton().AutomationElement);
            }
        }

        [Test]
        public void DumpCalendar()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "CalendarWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindFirst(TreeScope.Descendants, Conditions.Calendar).AutomationElement);
            }
        }

        [Test]
        public void DumpComboBox()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindComboBox().AutomationElement);
            }
        }

        [Test]
        public void DumpComboBoxExpanded()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow"))
            {
                var window = app.MainWindow;
                var comboBox = window.FindComboBox();
                comboBox.Expand();
                Dump.Recursive(comboBox.AutomationElement);
            }
        }

        [Test]
        public void DumpFrame()
        {
            using (var app = Application.Launch(ExeFileName, "FrameWindow"))
            {
                Dump.Recursive(app.MainWindow.FindFrame().AutomationElement);
            }
        }

        [Test]
        public void DumpDataGrid()
        {
            using (var app = Application.Launch(ExeFileName, "SingleDataGridWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindDataGrid().AutomationElement);
            }
        }

        [Test]
        public void DumpDataGrid10()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindDataGrid("DataGrid10").AutomationElement);
            }
        }

        [Test]
        public void DumpDialog()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show Dialog").Click();
                var dialog = window.FindDialog();
                var element = dialog.AutomationElement;
                Dump.Recursive(element);
                dialog.Close();
            }
        }

        [Test]
        public void DumpExpander()
        {
            using (var app = Application.Launch(ExeFileName, "ExpanderWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindExpander().AutomationElement;
                Dump.Recursive(element);
            }
        }

        [Test]
        public void DumpGridSplitterWindow()
        {
            using (var app = Application.Launch(ExeFileName, "GridSplitterWindow"))
            {
                Dump.Recursive(app.MainWindow.AutomationElement);
            }
        }

        [Test]
        public void DumpOpenFileDialog()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show OpenFileDialog").Click();
                var dialog = window.FindOpenFileDialog();
                Dump.Recursive(dialog.AutomationElement);
                dialog.AutomationElement.WindowPattern().Close();
            }
        }

        [Test]
        public void DumpSaveFileDialog()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show SaveFileDialog").Click();
                var dialog = window.FindSaveFileDialog();
                Dump.Recursive(dialog.AutomationElement);
                dialog.AutomationElement.WindowPattern().Close();
            }
        }

        [Test]
        public void DumpMenu()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "MenuWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindMenu().AutomationElement);
            }
        }

        [Test]
        public void DumpTabControl()
        {
            using (var app = Application.Launch(ExeFileName, "TabControlWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindTabControl().AutomationElement;
                Dump.Recursive(element);
            }
        }

        [Test]
        public void DumpTabItem()
        {
            using (var app = Application.Launch(ExeFileName, "TabControlWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindTabControl().Items[0].AutomationElement;
                Dump.Recursive(element);
            }
        }

        [Test]
        public void DumpGroupBox()
        {
            using (var app = Application.Launch(ExeFileName, "GroupBoxWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindGroupBox().AutomationElement;
                Dump.Recursive(element);
            }
        }

        [Test]
        public void DumpRichTextBox()
        {
            using (var app = Application.Launch(ExeFileName, "RichTextBoxWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindFirstDescendant(Conditions.ByClassName("RichTextBox")).AutomationElement;
                Dump.Recursive(element);
            }
        }

        [Test]
        public void DumpPasswordBox()
        {
            using (var app = Application.Launch(ExeFileName, "PasswordBoxWindow"))
            {
                var window = app.MainWindow;
                var element = window.FindFirstDescendant(new PropertyCondition(AutomationElement.IsPasswordProperty, true)).AutomationElement;
                Dump.Recursive(element);
            }
        }

        [Test]
        public void DumpSeparatorWindow()
        {
            using (var app = Application.Launch(ExeFileName, "SeparatorWindow"))
            {
                Dump.Recursive(app.MainWindow.AutomationElement);
            }
        }

        [Test]
        public void DumpStatusBar()
        {
            using (var app = Application.Launch(ExeFileName, "StatusBarWindow"))
            {
                Dump.Recursive(app.MainWindow.FindStatusBar().AutomationElement);
            }
        }

        [Test]
        public void DumpWindow()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                var element = window.AutomationElement;
                Dump.Recursive(element);
            }
        }

        [Test]
        public void DumpDataGridItem()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindDataGrid()[0, 0].AutomationElement);
            }
        }

        [Test]
        public void DumpListBox()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindListBox().AutomationElement);
            }
        }

        [Test]
        public void DumpListBox10()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindListBox("ListBox10").AutomationElement);
            }
        }

        [Test]
        public void DumpListView()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindListView().AutomationElement);
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
                Dump.Recursive(element);
                messageBox.Close();
            }
        }

        [Test]
        public void DumpScrollViewer()
        {
            using (var app = Application.Launch(ExeFileName, "ScrollBarWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindScrollViewer().AutomationElement);
            }
        }

        [Test]
        public void DumpTextBox()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "TextBoxWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindTextBox().AutomationElement);
            }
        }

        [Test]
        public void DumpToggleButton()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow;
                var toggleButton = window.FindToggleButton();
                Dump.Recursive(toggleButton.AutomationElement, allPropertiesAndPatterns: true);
            }
        }

        [Test]
        public void DumpTreeView()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "TreeViewWindow"))
            {
                var window = app.MainWindow;
                Dump.Recursive(window.FindTreeView().AutomationElement);
            }
        }
    }
}
