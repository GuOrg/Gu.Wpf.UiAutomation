namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using System.Globalization;
    using NUnit.Framework;

    public class DatePickerTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using var app = Application.Launch(ExeFileName, "DatePickerWindow");
            var window = app.MainWindow;
            var datePicker = window.FindDatePicker();
            Assert.IsInstanceOf<DatePicker>(datePicker);
            Assert.IsInstanceOf<DatePicker>(UiElement.FromAutomationElement(datePicker.AutomationElement));
        }

        [Test]
        public void Value()
        {
            using var app = Application.Launch(ExeFileName, "DatePickerWindow");
            var window = app.MainWindow;
            var datePicker = window.FindDatePicker();
            Assert.AreEqual(string.Empty, datePicker.Value);
            datePicker.Value = "2017-12-31";
            Assert.AreEqual(DateTime.Parse("2017-12-31 00:00:00", CultureInfo.CurrentCulture), DateTime.Parse(datePicker.Value, CultureInfo.CurrentCulture));
        }
    }
}
