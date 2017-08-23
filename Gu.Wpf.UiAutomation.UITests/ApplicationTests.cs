namespace Gu.Wpf.UiAutomation.UITests
{
    using Gu.Wpf.UiAutomation.UIA3;
    using NUnit.Framework;

    public class ApplicationTests
    {
        [Test]
        public void DisposeWhenClosed()
        {
            using (var automation = new UIA3Automation())
            {
                using (var app = Application.Launch("notepad.exe"))
                {
                    var window = app.GetMainWindow(automation);
                    app.Close();
                }
            }
        }
    }
}