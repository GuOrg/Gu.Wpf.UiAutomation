namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a set of static methods for creating <see cref="IDisposable"/> objects.
    /// </summary>
    public static class Disposable
    {
        /// <summary>
        /// Gets the disposable that does nothing when disposed.
        /// </summary>
        public static IDisposable Empty { get; } = new NopDisposable();

        /// <summary>
        /// Creates a disposable object that invokes the specified action when disposed.
        /// </summary>
        /// <param name="dispose">Action to run during the first call to <see cref="IDisposable.Dispose"/>. The action is guaranteed to be run at most once.</param>
        /// <returns>The disposable object that runs the given action upon disposal.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dispose"/> is <c>null</c>.</exception>
        public static IDisposable Create(Action dispose)
        {
            if (dispose == null)
            {
                throw new ArgumentNullException(nameof(dispose));
            }

            return new ActionDisposable(dispose);
        }

        public static IDisposable Create(IEnumerable<IDisposable> disposables)
        {
            return Disposable.Create(
                () =>
                {
                    foreach (var disposable in disposables)
                    {
                        disposable.Dispose();
                    }
                });
        }

        private sealed class NopDisposable : IDisposable
        {
            public void Dispose()
            {
                // NOP
            }
        }
    }
}