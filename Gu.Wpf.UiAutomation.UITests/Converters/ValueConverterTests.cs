namespace Gu.Wpf.UiAutomation.UITests.Converters
{
    using System.IO;
    using NUnit.Framework;

    public class ValueConverterTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void GetControlType()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var checkBox = window.FindCheckBox("Test Checkbox");
                Assert.AreEqual(ControlType.CheckBox, checkBox.Properties.ControlType);
            }
        }
    }
}
