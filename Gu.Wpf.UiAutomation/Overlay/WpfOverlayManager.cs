namespace Gu.Wpf.UiAutomation.Overlay
{
    using System;
    using System.Threading;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Gu.Wpf.UiAutomation.Shapes;

    public class WpfOverlayManager : IOverlayManager
    {
        private readonly Thread uiThread;
#if NET35
        private readonly ManualResetEvent _startedEvent = new ManualResetEvent(false);
#else
        private readonly ManualResetEventSlim startedEvent = new ManualResetEventSlim(false);
#endif
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
#if NET35
            _startedEvent.WaitOne();
#else
            this.startedEvent.Wait();
#endif
        }

        public int Size { get; set; }

        public int Margin { get; set; }

        public void Show(Rectangle rectangle, Color color, int durationInMs)
        {
            if (rectangle.IsValid)
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

        public void ShowBlocking(Rectangle rectangle, Color color, int durationInMs)
        {
            if (rectangle.IsValid)
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

        public void Dispose()
        {
            this.dispatcher.InvokeShutdown();
            this.uiThread.Join(1000);
        }
    }
}
