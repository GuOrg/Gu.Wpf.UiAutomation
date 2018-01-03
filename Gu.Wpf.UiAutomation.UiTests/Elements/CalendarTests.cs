namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
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
                var window = app.MainWindow;
                var calendar = window.FindCalendar();
                CollectionAssert.AllItemsAreInstancesOfType(calendar.Items, typeof(CalendarDayButton));
                calendar.Items[3].Select();
            }
        }

        [Test]
        public void SelectDate()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "CalendarWindow"))
            {
                var window = app.MainWindow;
                var calendar = window.FindCalendar();
                var date = DateTime.Today.AddDays(1);
                Assert.Null(calendar.SelectedItem);
                calendar.Select(date);

                // Can't figure out a nice way to assert here
                // Tricky with culture
                Assert.NotNull(calendar.SelectedItem);
            }
        }
    }
}
