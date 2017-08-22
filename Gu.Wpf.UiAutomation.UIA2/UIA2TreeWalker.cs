namespace Gu.Wpf.UiAutomation.UIA2
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.UIA2.Converters;
    using Gu.Wpf.UiAutomation.UIA2.Extensions;
    using UIA = System.Windows.Automation;

    public class UIA2TreeWalker : ITreeWalker
    {
        public UIA2Automation Automation { get; }

        public UIA.TreeWalker NativeTreeWalker { get; }

        public UIA2TreeWalker(UIA2Automation automation, UIA.TreeWalker nativeTreeWalker)
        {
            this.Automation = automation;
            this.NativeTreeWalker = nativeTreeWalker;
        }

        public AutomationElement GetParent(AutomationElement element)
        {
            var parent = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetParent(element.ToNative()) :
                this.NativeTreeWalker.GetParent(element.ToNative(), CacheRequest.Current.ToNative());
            return this.Automation.WrapNativeElement(parent);
        }

        public AutomationElement GetFirstChild(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetFirstChild(element.ToNative()) :
                this.NativeTreeWalker.GetFirstChild(element.ToNative(), CacheRequest.Current.ToNative());
            return this.Automation.WrapNativeElement(child);
        }

        public AutomationElement GetLastChild(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetLastChild(element.ToNative()) :
                this.NativeTreeWalker.GetLastChild(element.ToNative(), CacheRequest.Current.ToNative());
            return this.Automation.WrapNativeElement(child);
        }

        public AutomationElement GetNextSibling(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetNextSibling(element.ToNative()) :
                this.NativeTreeWalker.GetNextSibling(element.ToNative(), CacheRequest.Current.ToNative());
            return this.Automation.WrapNativeElement(child);
        }

        public AutomationElement GetPreviousSibling(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                this.NativeTreeWalker.GetPreviousSibling(element.ToNative()) :
                this.NativeTreeWalker.GetPreviousSibling(element.ToNative(), CacheRequest.Current.ToNative());
            return this.Automation.WrapNativeElement(child);
        }
    }
}
