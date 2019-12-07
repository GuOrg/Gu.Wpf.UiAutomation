namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
    using System.Windows.Automation;
    using NUnit.Framework;

    public class DataGridColumnHeadersPresenterTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Find()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow");
            var window = app.MainWindow;
            var presenter = (DataGridColumnHeadersPresenter)window.FindFirst(TreeScope.Descendants, Conditions.DataGridColumnHeadersPresenter);
            Assert.IsInstanceOf<DataGridColumnHeadersPresenter>(UiElement.FromAutomationElement(presenter.AutomationElement));
        }

        [Test]
        public void Headers()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow");
            var window = app.MainWindow;
            var presenter = (DataGridColumnHeadersPresenter)window.FindFirst(TreeScope.Descendants, Conditions.DataGridColumnHeadersPresenter);
            CollectionAssert.AllItemsAreInstancesOfType(presenter.Headers, typeof(DataGridColumnHeader));
            CollectionAssert.AreEqual(new[] { "IntValue", "StringValue" }, presenter.Headers.Select(x => x.Text));
        }
    }
}
