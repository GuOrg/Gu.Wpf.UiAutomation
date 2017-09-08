namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Threading;

    /// <summary>
    /// Represents an Action-based disposable.
    /// </summary>
    internal sealed class ActionDisposable : IDisposable
    {
        private volatile Action dispose;

        /// <summary>
        /// Constructs a new disposable with the given action used for disposal.
        /// </summary>
        /// <param name="dispose">Disposal action which will be run upon calling Dispose.</param>
        public ActionDisposable(Action dispose)
        {
            this.dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
        }

        /// <summary>
        /// Gets a value that indicates whether the object is disposed.
        /// </summary>
        public bool IsDisposed => this.dispose == null;

        /// <summary>
        /// Calls the disposal action if and only if the current instance hasn't been disposed yet.
        /// </summary>
        public void Dispose()
        {
            Interlocked.Exchange(ref this.dispose, null)?.Invoke();
        }
    }
}