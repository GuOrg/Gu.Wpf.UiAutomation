namespace Gu.Wpf.UiAutomation.UITests.TestFramework
{
    using Gu.Wpf.UiAutomation.AutomationElements;
    using Gu.Wpf.UiAutomation.Input;
    using Gu.Wpf.UiAutomation.Shapes;
    using NUnit.Framework;

    /// <summary>
    /// Various helpful methods
    /// </summary>
    public static class TestUtilities
    {
        /// <summary>
        /// Closes the given window and invokes the "Don't save" button
        /// </summary>
        public static void CloseWindowWithDontSave(Window window)
        {
            window.Close();
            Helpers.WaitUntilInputIsProcessed();
            var modal = window.ModalWindows;
            var dontSaveButton = modal[0].FindFirstDescendant(cf => cf.ByAutomationId("CommandButton_7")).AsButton();
            dontSaveButton.Invoke();
        }

        public static void AssertPointsAreSame(Point p1, Point p2, double variance = 0)
        {
            Assert.That(p1.X, Is.EqualTo(p2.X).Within(variance));
            Assert.That(p1.Y, Is.EqualTo(p2.Y).Within(variance));
        }
    }
}
