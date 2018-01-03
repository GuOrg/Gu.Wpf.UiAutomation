namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class CalendarTests
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
            using (var app = Application.AttachOrLaunch(ExeFileName, "CalendarWindow"))
            {
                var window = app.MainWindow;
                var calendar = window.FindCalendar();
                Assert.IsInstanceOf<Calendar>(UiElement.FromAutomationElement(calendar.AutomationElement));
                Assert.IsInstanceOf<CalendarDayButton>(UiElement.FromAutomationElement(calendar.Items[0].AutomationElement));
            }
        }

        [Test]
        public void SelectDay()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "CalendarWindow"))
            {
                Assert.Fail("Need to figure out how to get items for current view.");
                var window = app.MainWindow;
                var calendar = window.FindCalendar();
                CollectionAssert.AllItemsAreInstancesOfType(calendar.Items, typeof(CalendarDayButton));
                calendar.Items[3].Select();
            }
        }
    }
}
