namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System;
    using NUnit.Framework;

    public partial class AutomationElementTests
    {
        public class Find
        {
            [Test]
            public void FindCheckBox()
            {
                using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    var checkBox = window.FindCheckBox();
                    Assert.IsInstanceOf<CheckBox>(checkBox);
                }
            }

            [TestCase(null)]
            [TestCase("AutomationId")]
            [TestCase("XName")]
            [TestCase("Content")]
            public void FindCheckBoxThrowsWhenNotFound(string key)
            {
                using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
                {
                    var window = app.MainWindow;
                    var exception = Assert.Throws<InvalidOperationException>(() => window.FindCheckBox(key));
                    var expected = key == null
                        ? $"Did not find a CheckBox matching ControlType: CheckBox."
                        : $"Did not find a CheckBox matching (ControlType: CheckBox AND (Name: {key} OR AutomationId: {key})).";
                    Assert.AreEqual(expected, exception.Message);
                }
            }

            [Test]
            public void FindFirstChild()
            {
                using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    var child = window.FindFirstChild();
                    Assert.AreEqual(ControlType.TitleBar, child.ControlType);
                }
            }

            [Test]
            public void FindFirstChildWithWrap()
            {
                using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    var child = window.FindFirstChild(x => new CheckBox(x));
                    Assert.AreEqual(ControlType.TitleBar, child.ControlType);
                }
            }

            [TestCase(0, ControlType.TitleBar)]
            [TestCase(1, ControlType.MenuBar)]
            [TestCase(2, ControlType.MenuItem)]
            public void FindAt(int index, ControlType expected)
            {
                using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
                {
                    var window = app.MainWindow;
                    var child = window.FindAt(TreeScope.Descendants, TrueCondition.Default, index, TimeSpan.FromMilliseconds(100));
                    Assert.AreEqual(expected, child.ControlType);
                }
            }
        }
    }
}