namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class ListBoxTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void Items()
        {
            using (var app = Application.Launch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                var itemsControl = window.FindListBox();
                Assert.AreEqual(2, itemsControl.Items.Count);
            }
        }
    }
}