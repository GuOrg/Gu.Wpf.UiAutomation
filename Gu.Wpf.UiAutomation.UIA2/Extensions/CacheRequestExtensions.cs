using Gu.Wpf.UiAutomation;
using Gu.Wpf.UiAutomation.UIA2.Converters;
using UIA = System.Windows.Automation;

namespace Gu.Wpf.UiAutomation.UIA2.Extensions
{
    public static class CacheRequestExtensions
    {
        public static UIA.CacheRequest ToNative(this CacheRequest cacheRequest)
        {
            var nativeCacheRequest = new UIA.CacheRequest();
            nativeCacheRequest.AutomationElementMode = (UIA.AutomationElementMode)cacheRequest.AutomationElementMode;
            nativeCacheRequest.TreeFilter = ConditionConverter.ToNative(cacheRequest.TreeFilter);
            nativeCacheRequest.TreeScope = (UIA.TreeScope)cacheRequest.TreeScope;
            foreach (var pattern in cacheRequest.Patterns)
            {
                nativeCacheRequest.Add(UIA.AutomationPattern.LookupById(pattern.Id));
            }
            foreach (var property in cacheRequest.Properties)
            {
                nativeCacheRequest.Add(UIA.AutomationProperty.LookupById(property.Id));
            }
            return nativeCacheRequest;
        }
    }
}
