namespace Gu.Wpf.UiAutomation.UIA3
{
    using System.Collections.Generic;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Extensions;

    public class UIA3TreeWalker : ITreeWalker
    {
        public UIA3TreeWalker(UIA3Automation automation, Interop.UIAutomationClient.IUIAutomationTreeWalker nativeTreeWalker)
        {
            this.Automation = automation;
            this.NativeTreeWalker = nativeTreeWalker;
        }

        public UIA3Automation Automation { get; }

        public Interop.UIAutomationClient.IUIAutomationTreeWalker NativeTreeWalker { get; }

        public AutomationElement GetParent(AutomationElement element)
        {
            var native = element.ToNative();
            if (native == null)
            {
                return null;
            }

            var parent = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetParentElement(native) :
                this.NativeTreeWalker.GetParentElementBuildCache(native, CacheRequest.Current.ToNative(this.Automation));
            return this.Automation.WrapNativeElement(parent);
        }

        public IReadOnlyList<AutomationElement> GetChildren(AutomationElement element)
        {
            var children = new List<AutomationElement>();
            var child = this.GetFirstChild(element);
            while (child != null)
            {
                children.Add(child);
                child = this.GetNextSibling(child);
            }

            return children;
        }

        public AutomationElement GetFirstChild(AutomationElement element)
        {
            var native = element.ToNative();
            if (native == null)
            {
                return null;
            }

            var child = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetFirstChildElement(native) :
                this.NativeTreeWalker.GetFirstChildElementBuildCache(native, CacheRequest.Current.ToNative(this.Automation));
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
