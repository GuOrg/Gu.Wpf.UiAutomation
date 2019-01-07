// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/input_touchinjection/constants.
    /// </summary>
    public enum TOUCH_FEEDBACK
    {
        /// <summary>
        /// Specifies default touch visualizations.
        /// </summary>
        DEFAULT = 0x1,

        /// <summary>
        /// Specifies indirect touch visualizations.
        /// </summary>
        INDIRECT = 0x2,

        /// <summary>
        /// Specifies no touch visualizations.
        /// </summary>
        NONE = 0x3,
    }
}
