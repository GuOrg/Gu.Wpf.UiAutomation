namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Windows.Automation;
    using NUnit.Framework;
    using Condition = Gu.Wpf.UiAutomation.Condition;

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
            using (var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow"))
            {
                var window = app.MainWindow;
                var presenter = (DataGridColumnHeadersPresenter)window.FindFirst(TreeScope.Descendants, Condition.DataGridColumnHeadersPresenter);
                Assert.IsInstanceOf<DataGridColumnHeadersPresenter>(UiElement.FromAutomationElement(presenter.AutomationElement));
            }
        }
    }
}
