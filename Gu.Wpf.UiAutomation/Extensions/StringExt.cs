namespace Gu.Wpf.UiAutomation
{
    internal static class StringExt
    {
        internal static string TrimStart(this string text, string toTrim)
        {
            if (text.StartsWith(toTrim))
            {
                return text.Substring(toTrim.Length);
            }

            return text;
        }

        internal static string TrimEnd(this string text, string toTrim)
        {
            if (text.EndsWith(toTrim))
            {
                return text.Substring(0, text.Length - toTrim.Length);
            }

            return text;
        }
    }
}