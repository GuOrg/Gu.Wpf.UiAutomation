namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public static class EnumerableExt
    {
        public static bool TryGetSingle<T>(this IEnumerable<T> source, [MaybeNull]out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using (var enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    result = enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        return true;
                    }
                }
            }

            result = default!;
            return false;
        }

        public static bool TryGetSingle<T>(this IEnumerable<T> source, Func<T, bool> predicate, [MaybeNull]out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
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
