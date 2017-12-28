namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Threading;

    public static class Retry
    {
        /// <summary>
        /// The time to retry when searching for elements. Default is one second.
        /// </summary>
        public static TimeSpan Time { get; set; } = TimeSpan.FromMilliseconds(1000);

        /// <summary>
        /// The poll interval, default 100 ms.
        /// </summary>
        public static TimeSpan PollInterval { get; set; } = TimeSpan.FromMilliseconds(100);

        public static void WhileException(Action retryAction, TimeSpan timeout, TimeSpan? retryInterval = null)
        {
            var startTime = DateTime.Now;
            while (true)
            {
                try
                {
                    retryAction();
                    return;
                }
                catch (Exception ex)
                {
                    if (IsTimeouted(startTime, timeout))
                    {
                        throw new Exception("Timeout occurred in retry", ex);
                    }

                    Thread.Sleep(retryInterval ?? PollInterval);
                }
            }
        }

        public static T WhileException<T>(Func<T> retryMethod, TimeSpan timeout, TimeSpan? retryInterval = null)
        {
            var startTime = DateTime.Now;
            while (true)
            {
                try
                {
                    return retryMethod();
                }
                catch (Exception ex)
                {
                    if (IsTimeouted(startTime, timeout))
                    {
                        throw new Exception("Timeout occurred in retry", ex);
                    }

                    Thread.Sleep(retryInterval ?? PollInterval);
                }
            }
        }

        [Obsolete("Refactor away from this. Saves so little duplication.")]
        public static T While<T>(Func<T> retryMethod, Predicate<T> whilePredicate, TimeSpan timeout, TimeSpan? retryInterval = null)
        {
            var startTime = DateTime.Now;
            while (true)
            {
                var obj = retryMethod();
                if (!whilePredicate(obj))
                {
                    return obj;
                }

                if (IsTimeouted(startTime, timeout))
                {
                    throw new TimeoutException();
                }

                Wait.For(retryInterval ?? PollInterval);
            }
        }

        [Obsolete("Refactor away from this. Saves so little duplication.")]
        public static void While(Func<bool> whilePredicate, TimeSpan timeout, TimeSpan? retryInterval = null)
        {
            var startTime = DateTime.Now;
            while (true)
            {
                if (!whilePredicate())
                {
                    return;
                }

                if (IsTimeouted(startTime, timeout))
                {
                    return;
                }

                Thread.Sleep(retryInterval ?? PollInterval);
            }
        }

        public static bool IsTimeouted(DateTime startTime, TimeSpan timeout)
        {
            // Check for infinite timeout
            if (timeout.TotalMilliseconds < 0)
            {
                return false;
            }

            return DateTime.Now.Subtract(startTime) >= timeout;
        }

        public static void ResetTime()
        {
            Time = TimeSpan.FromMilliseconds(1000);
        }
    }
}
