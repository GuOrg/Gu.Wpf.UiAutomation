namespace Gu.Wpf.UiAutomation
{
    using System.Globalization;

    public static class SystemLanguageRetreiver
    {
        public static CultureInfo GetCurrentOsCulture()
        {
            var currentOsCulture = CultureInfo.InstalledUICulture;
            return currentOsCulture;
        }
    }
}
