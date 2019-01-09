// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    public enum SystemMetric
    {
        /// <summary>
        /// The width of the screen of the primary display monitor, in pixels. This is the same value obtained by calling GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor, HORZRES)
        /// </summary>
        SM_CXSCREEN = 0,

        /// <summary>
        /// The height of the screen of the primary display monitor, in pixels. This is the same value obtained by calling GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor, VERTRES)
        /// </summary>
        SM_CYSCREEN = 1,

        /// <summary>
        /// Nonzero if the meanings of the left and right mouse buttons are swapped; otherwise, 0.
        /// </summary>
        SM_SWAPBUTTON = 23,
        SM_XVIRTUALSCREEN = 76,
        SM_YVIRTUALSCREEN = 77,
        SM_CXVIRTUALSCREEN = 78,
        SM_CYVIRTUALSCREEN = 79,
    }
}
