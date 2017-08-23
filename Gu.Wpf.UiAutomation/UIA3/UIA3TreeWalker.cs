namespace Gu.Wpf.UiAutomation.UIA3
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Extensions;
    using UIA = Interop.UIAutomationClient;

    public class UIA3TreeWalker : ITreeWalker
    {
        public UIA3TreeWalker(UIA3Automation automation, UIA.IUIAutomationTreeWalker nativeTreeWalker)
        {
            this.Automation = automation;
            this.NativeTreeWalker = nativeTreeWalker;
        }

        public UIA3Automation Automation { get; }

        public UIA.IUIAutomationTreeWalker NativeTreeWalker { get; }

        public AutomationElement GetParent(AutomationElement element)
        {
            var parent = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetParentElement(element.ToNative()) :
                this.NativeTreeWalker.GetParentElementBuildCache(element.ToNative(), CacheRequest.Current.ToNative(this.Automation));
            return this.Automation.WrapNativeElement(parent);
        }

        public AutomationElement GetFirstChild(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetFirstChildElement(element.ToNative()) :
                this.NativeTreeWalker.GetFirstChildElementBuildCache(element.ToNative(), CacheRequest.Current.ToNative(this.Automation));
            return this.Automation.WrapNativeElement(child);
        }

        public AutomationElement GetLastChild(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetLastChildElement(element.ToNative()) :
                this.NativeTreeWalker.GetLastChildElementBuildCache(element.ToNative(), CacheRequest.Current.ToNative(this.Automation));
            return this.Automation.WrapNativeElement(child);
        }

        public AutomationElement GetNextSibling(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetNextSiblingElement(element.ToNative()) :
                this.NativeTreeWalker.GetNextSiblingElementBuildCache(element.ToNative(), CacheRequest.Current.ToNative(this.Automation));
            return this.Automation.WrapNativeElement(child);
        }

        public AutomationElement GetPreviousSibling(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetPreviousSiblingElement(element.ToNative()) :
                this.NativeTreeWalker.GetPreviousSiblingElementBuildCache(element.ToNative(), CacheRequest.Current.ToNative(this.Automation));
            return this.Automation.WrapNativeElement(child);
        }
    }
}
