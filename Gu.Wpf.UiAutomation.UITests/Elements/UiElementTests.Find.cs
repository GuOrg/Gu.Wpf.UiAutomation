namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;
    using NUnit.Framework;
    using Condition = Gu.Wpf.UiAutomation.Condition;

    public partial class UiElementTests
    {
        public class Find
        {
            private static readonly IReadOnlyList<TestCaseData> FindAtCases = new[]
            {
                new TestCaseData(0, ControlType.TitleBar),
                new TestCaseData(1, ControlType.MenuBar),
                new TestCaseData(2, ControlType.MenuItem),
            };

            [OneTimeTearDown]
            public void OneTimeTearDown()
            {
                Application.KillLaunched(ExeFileName);
                Retry.ResetTime();
            }

            [Test]
            public void FromPoint()
            {
                using (var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow"))
                {
                    var window = app.MainWindow;
                    var button = window.FindButton();
                    var fromPoint = UiElement.FromPoint(button.GetClickablePoint()).AsButton();
                    Assert.AreEqual(button.Text, fromPoint.Text);
                }
            }

            [Test]
            public void FocusedElement()
            {
                using (var app = Application.AttachOrLaunch(ExeFileName, "TextBoxWindow"))
                {
                    var window = app.MainWindow;
                    var textBox = window.FindTextBox();
                    textBox.Text = "focused";
                    textBox.Focus();
                    var fromPoint = window.FocusedElement().AsTextBox();
                    Assert.AreEqual("focused", fromPoint.Text);
                }
            }

            [Test]
            public void FindCheckBox()
            {
                using (var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    var checkBox = window.FindCheckBox();
                    Assert.IsInstanceOf<CheckBox>(checkBox);
                }
            }

            //// [TestCase("CheckBoxXName")]
            [TestCase("CheckBox1AutomationId")]
            [TestCase("CheckBox1Content")]
            public void FindCheckBoxWithXNameAndAutomationId(string key)
            {
                using (var app = Application.AttachOrLaunch(ExeFileName, "FindWindow"))
                {
                    var window = app.MainWindow;
                    var checkBox = window.FindCheckBox(key);
                    Assert.NotNull(checkBox);
                }
            }

            [TestCase(null)]
            [TestCase("AutomationId")]
            [TestCase("XName")]
            [TestCase("Content")]
            public void FindCheckBoxThrowsWhenNotFound(string key)
            {
                Retry.Time = TimeSpan.FromMilliseconds(0);
                using (var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow"))
                {
                    var window = app.MainWindow;
                    var exception = Assert.Throws<InvalidOperationException>(() => window.FindCheckBox(key));
                    var expected = key == null
                        ? $"Did not find a CheckBox matching ControlType == CheckBox."
                        : $"Did not find a CheckBox matching (ControlType == CheckBox && (Name == {key} || AutomationId == {key})).";
                    Assert.AreEqual(expected, exception.Message);
                }

                Retry.ResetTime();
            }

            [Test]
            public void FindFirstChild()
            {
                using (var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    var child = window.FindFirstChild();
                    Assert.AreEqual("title bar", child.LocalizedControlType);
                }
            }

            [Test]
            public void FindFirstChildWithWrap()
            {
                using (var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    var child = window.FindFirstChild(x => new CheckBox(x));
                    Assert.AreEqual(ControlType.TitleBar, child.ControlType);
                }
            }

            [TestCaseSource(nameof(FindAtCases))]
            public void FindAt(int index, ControlType expected)
            {
                using (var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    var child = window.FindAt(TreeScope.Descendants, Condition.TrueCondition, index, TimeSpan.FromMilliseconds(100));
                    Assert.AreEqual(expected, child.ControlType);
                }
            }

            [Test]
            public void FindAtWithWrap()
            {
                using (var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    var child = window.FindAt(
                        TreeScope.Descendants,
                        Condition.CheckBox,
                        1,
                        x => new CheckBox(x),
                        TimeSpan.FromMilliseconds(100));
                    Assert.IsInstanceOf<CheckBox>(child);
                    Assert.AreEqual("XName", child.AutomationId);
                }
            }

            [Test]
            public void TryFindAtWithWrap()
            {
                using (var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    Assert.AreEqual(true, window.TryFindAt(
                        TreeScope.Descendants,
                        Condition.CheckBox,
                        1,
                        x => new CheckBox(x),
                        TimeSpan.FromMilliseconds(100),
                        out var child));
                    Assert.IsInstanceOf<CheckBox>(child);
                    Assert.AreEqual("XName", child.AutomationId);

                    Assert.AreEqual(false, window.TryFindAt(
                        TreeScope.Descendants,
                        Condition.CheckBox,
                        100,
                        x => new CheckBox(x),
                        TimeSpan.FromMilliseconds(100),
                        out child));
                    Assert.IsNull(child);
                }
            }
        }
    }
}