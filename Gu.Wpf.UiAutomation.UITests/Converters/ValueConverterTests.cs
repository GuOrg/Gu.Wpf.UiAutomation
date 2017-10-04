namespace Gu.Wpf.UiAutomation.UITests.Converters
{
    using System.Windows;
    using NUnit.Framework;

    public class ValueConverterTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void CheckBoxControlType()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox("Test Checkbox");
                Assert.AreEqual(ControlType.CheckBox, checkBox.ControlType);
            }
        }

        [Test]
        public void CheckBoxIsChecked()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox("Test Checkbox");
                Assert.AreEqual(false, checkBox.IsChecked);
            }
        }

        [Test]
        public void CheckBoxBounds()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox("Test Checkbox");
                Assert.IsInstanceOf<Rect>(checkBox.Bounds);
            }
        }
    }
}
