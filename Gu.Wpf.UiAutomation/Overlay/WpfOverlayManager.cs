namespace Gu.Wpf.UiAutomation.Overlay
{
    using System;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class WpfOverlayManager : IOverlayManager
    {
        private readonly Thread uiThread;
        private readonly ManualResetEventSlim startedEvent = new ManualResetEventSlim(initialState: false);

        private Dispatcher dispatcher;
        private OverlayRectangleWindow currWin;

        public WpfOverlayManager()
        {
            this.uiThread = new Thread(() =>
            {
                // Create and install a new dispatcher context
                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(
                        Dispatcher.CurrentDispatcher));

                this.dispatcher = Dispatcher.CurrentDispatcher;

                // Signal that it is initialized
                this.startedEvent.Set();

                // Start the dispatcher processing
                Dispatcher.Run();
            });

            // Set the apartment state
            this.uiThread.SetApartmentState(ApartmentState.STA);

            // Make the thread a background thread
            this.uiThread.IsBackground = true;

            // Start the thread
            this.uiThread.Start();
            this.startedEvent.Wait();
        }

        /// <inheritdoc/>
        public int Size { get; set; }

        /// <inheritdoc/>
        public int Margin { get; set; }

        /// <inheritdoc/>
        public void Show(Rect rectangle, Color color, int durationInMs)
        {
            if (rectangle.IsValid())
            {
                // ReSharper disable once RedundantDelegateCreation Used for older .Net versions
                this.dispatcher.Invoke(new Action(() =>
                {
                    this.currWin?.Close();
                    var win = new OverlayRectangleWindow(rectangle, color, durationInMs);
                    win.Show();
                    this.currWin = win;
                }));
            }
        }

        /// <inheritdoc/>
        public void ShowBlocking(Rect rectangle, Color color, int durationInMs)
        {
            if (rectangle.IsValid())
            {
                // ReSharper disable once RedundantDelegateCreation Used for older .Net versions
                this.dispatcher.Invoke(new Action(() =>
                {
                    this.currWin?.Close();
                    var win = new OverlayRectangleWindow(rectangle, color, durationInMs);
                    win.ShowDialog();
                    this.currWin = win;
                }));
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.dispatcher.InvokeShutdown();
            this.uiThread.Join(1000);
            this.startedEvent.Dispose();
        }
    }
}
