namespace Gu.Wpf.UiAutomation.UIA3.Extensions
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;

    public static class CacheRequestExtensions
    {
        public static Interop.UIAutomationClient.IUIAutomationCacheRequest ToNative(this CacheRequest cacheRequest, UIA3Automation automation)
        {
            var nativeCacheRequest = automation.NativeAutomation.CreateCacheRequest();
            nativeCacheRequest.AutomationElementMode = (Interop.UIAutomationClient.AutomationElementMode)cacheRequest.AutomationElementMode;
            nativeCacheRequest.TreeFilter = ConditionConverter.ToNative(automation, cacheRequest.TreeFilter);
            nativeCacheRequest.TreeScope = (Interop.UIAutomationClient.TreeScope)cacheRequest.TreeScope;
            foreach (var pattern in cacheRequest.Patterns)
            {
                nativeCacheRequest.AddPattern(pattern.Id);
            }

            foreach (var property in cacheRequest.Properties)
            {
                nativeCacheRequest.AddProperty(property.Id);
            }

            return nativeCacheRequest;
        }
    }
}
