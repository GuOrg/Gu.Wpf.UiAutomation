namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExt
    {
        public static bool TryGetSingle<T>(this IEnumerable<T> self, out T result)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }

            using (var enumerator = self.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    result = enumerator.Current;
                    if (enumerator.MoveNext())
                    {
                        result = default(T);
                        return false;
                    }

                    return true;
                }
            }

            result = default(T);
            return false;
        }

        public static bool TryGetSingle<T>(this IEnumerable<T> self, Func<T, bool> selector, out T result)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            result = default(T);
            var found = false;
            foreach (var item in self)
            {
                if (selector(item))
                {
                    if (found)
                    {
                        result = default(T);
                        return false;
                    }

                    result = item;
                    found = true;
                }
            }

            return found;
        }
    }
}