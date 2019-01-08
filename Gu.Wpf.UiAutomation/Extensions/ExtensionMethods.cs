namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ExtensionMethods
    {
        /// <summary>
        /// Makes sure a comparable object is between a given range.
        /// </summary>
        public static T Clamp<T>(this T source, T min, T max)
            where T : IComparable
        {
            var isReversed = min.CompareTo(max) > 0;
            var smallest = isReversed ? max : min;
            var biggest = isReversed ? min : max;

            return source.CompareTo(smallest) < 0 ? smallest :
                source.CompareTo(biggest) > 0 ? biggest : source;
        }

        /// <summary>
        /// Checks if a double is not NaN and not Infinity.
        /// </summary>
        public static bool HasValue(this double value)
        {
            return !double.IsNaN(value) && !double.IsInfinity(value);
        }

        public static IEnumerable<Enum> GetFlags(this Enum variable)
        {
            return Enum.GetValues(variable.GetType()).Cast<Enum>().Where(variable.HasFlag);
        }
    }
}
