namespace Gu.Wpf.UiAutomation.UITests
{
    using System.Threading;
    using Gu.Wpf.UiAutomation.Input;
    using Gu.Wpf.UiAutomation.UIA3;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    [TestFixture]
    public class KeyboardTests
    {
        [Test]
        public void KeyboardTest()
        {
            var app = Application.Launch("notepad.exe");
            using (var automation = new UIA3Automation())
            {
                var mainWindow = app.GetMainWindow(automation);

                Keyboard.Type("ééééééööööö aaa | ");

                Keyboard.Type(VirtualKeyShort.KEY_Z);
                Keyboard.Type(VirtualKeyShort.LEFT);
                Keyboard.Type(VirtualKeyShort.DELETE);
                Keyboard.Type(VirtualKeyShort.KEY_Y);
                Keyboard.Type(VirtualKeyShort.BACK);
                Keyboard.Type(VirtualKeyShort.KEY_X);

                Keyboard.Type(" | ");

                Keyboard.Type("ঋ ঌ এ ঐ ও ঔ ক খ গ ঘ ঙ চ ছ জ ঝ ঞ ট ঠ ড ঢ");

                Thread.Sleep(500);

                TestUtilities.CloseWindowWithDontSave(mainWindow);
            }
            app.Dispose();
        }
    }
}
