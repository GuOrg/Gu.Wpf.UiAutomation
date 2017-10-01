namespace Gu.Wpf.UiAutomation
{
    public enum OnFail
    {
        /// <summary>
        /// This is the default meaning just an assert exception is thrown if images are not equal.
        /// </summary>
        DoNothing,

        /// <summary>
        /// Saves the actual image to %TEMP%/filename
        /// </summary>
        SaveImageToTemp,
    }
}