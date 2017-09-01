namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class AutomationElementTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void Parent()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox();
                Assert.AreEqual("Window", checkBox.Parent.ClassName);
            }
        }
    }
}