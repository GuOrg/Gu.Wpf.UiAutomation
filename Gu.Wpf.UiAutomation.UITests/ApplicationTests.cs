namespace Gu.Wpf.UiAutomation.UITests
{
    using NUnit.Framework;

    public class ApplicationTests
    {
        [Test]
        public void DisposeWhenClosed()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                app.Close();
            }
        }
    }
}