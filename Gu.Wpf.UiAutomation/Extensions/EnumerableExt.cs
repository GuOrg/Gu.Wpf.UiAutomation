namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExt
    {
        public static bool TryGetSingle<T>(this IEnumerable<T> source, out T result)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using (var enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    result = enumerator.Current;
                    if (enumerator.MoveNext())
                    {
                        result = default;
                        return false;
                    }

                    return true;
                }
            }

            result = default;
            return false;
        }

        public static bool TryGetSingle<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T result)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    result = e.Current;
                    if (predicate(result))
                    {
                        while (e.MoveNext())
                        {
                            if (predicate(e.Current))
                            {
                                result = default;
                                return false;
                            }
                        }

                        return true;
                    }
                }
            }

            result = default;
            return false;
        }
    }
}
