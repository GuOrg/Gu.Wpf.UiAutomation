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

        /// <summary>
        /// The width of the rectangle around the location of a first click in a double-click sequence, in pixels.
        /// The second click must occur within the rectangle that is defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK for the system to consider the two clicks a double-click. The two clicks must also occur within a specified time.
        /// To set the width of the double-click rectangle, call SystemParametersInfo with SPI_SETDOUBLECLKWIDTH.
        /// </summary>
        SM_CXDOUBLECLK = 36,

        /// <summary>
        /// The height of the rectangle around the location of a first click in a double-click sequence, in pixels.
        /// The second click must occur within the rectangle defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK for the system to consider the two clicks a double-click. The two clicks must also occur within a specified time.
        /// To set the height of the double-click rectangle, call SystemParametersInfo with SPI_SETDOUBLECLKHEIGHT.
        /// </summary>
        SM_CYDOUBLECLK = 37,

        SM_XVIRTUALSCREEN = 76,
        SM_YVIRTUALSCREEN = 77,
        SM_CXVIRTUALSCREEN = 78,
        SM_CYVIRTUALSCREEN = 79,
    }
}
