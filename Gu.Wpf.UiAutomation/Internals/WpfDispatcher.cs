namespace Gu.Wpf.UiAutomation.Internals
{
    using System.Threading;
    using System.Windows.Threading;

    internal static class WpfDispatcher
    {
        internal static Dispatcher Create()
        {
            using var startedEvent = new ManualResetEventSlim(initialState: false);
            System.Windows.Threading.Dispatcher? dispatcher = null;
            var uiThread = new Thread(() =>
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));
                dispatcher = Dispatcher.CurrentDispatcher;
                //// ReSharper disable once AccessToDisposedClosure
                startedEvent.Set();
                Dispatcher.Run();
            });

            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = true;

            uiThread.Start();
            startedEvent.Wait();
            return dispatcher!;
        }
    }
}
