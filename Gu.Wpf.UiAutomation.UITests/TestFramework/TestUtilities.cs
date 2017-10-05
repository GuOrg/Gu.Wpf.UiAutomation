namespace Gu.Wpf.UiAutomation.UiTests.TestFramework
{
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
            Wait.UntilInputIsProcessed();
            var modal = window.ModalWindows;
            var dontSaveButton = modal[0].FindButton("CommandButton_7");
            dontSaveButton.Invoke();
        }
    }
}
