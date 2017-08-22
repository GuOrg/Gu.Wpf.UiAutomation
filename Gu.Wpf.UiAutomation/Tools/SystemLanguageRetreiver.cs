using System.Globalization;

namespace Gu.Wpf.UiAutomation.Tools
{
    public static class SystemLanguageRetreiver
    {
        public static CultureInfo GetCurrentOsCulture()
        {
            var currentOsCulture = CultureInfo.InstalledUICulture;
            return currentOsCulture;
        }
    }
}
