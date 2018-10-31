namespace Gu.Wpf.UiAutomation
{
    using System;

    internal static class StringExt
    {
        internal static string TrimStart(this string text, string toTrim)
        {
            if (text.StartsWith(toTrim, StringComparison.CurrentCulture))
            {
                return text.Substring(toTrim.Length);
            }

            return text;
        }

        internal static string TrimEnd(this string text, string toTrim)
        {
            if (text.EndsWith(toTrim, StringComparison.CurrentCulture))
            {
                return text.Substring(0, text.Length - toTrim.Length);
            }

            return text;
        }
    }
}
