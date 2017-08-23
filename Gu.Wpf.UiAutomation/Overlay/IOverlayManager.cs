namespace Gu.Wpf.UiAutomation.Overlay
{
    using System;
    using System.Windows.Media;

    public interface IOverlayManager : IDisposable
    {
        /// <summary>
        /// Border size of the overlay
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// Margin of the overlay (use negative to move it inside)
        /// </summary>
        int Margin { get; set; }

        void Show(Rectangle rectangle, Color color, int durationInMs);

        void ShowBlocking(Rectangle rectangle, Color color, int durationInMs);
    }
}
