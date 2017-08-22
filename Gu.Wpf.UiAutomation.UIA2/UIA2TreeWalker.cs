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
            Automation = automation;
            NativeTreeWalker = nativeTreeWalker;
        }

        public AutomationElement GetParent(AutomationElement element)
        {
            var parent = CacheRequest.Current == null ?
                NativeTreeWalker.GetParent(element.ToNative()) :
                NativeTreeWalker.GetParent(element.ToNative(), CacheRequest.Current.ToNative());
            return Automation.WrapNativeElement(parent);
        }

        public AutomationElement GetFirstChild(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                NativeTreeWalker.GetFirstChild(element.ToNative()) :
                NativeTreeWalker.GetFirstChild(element.ToNative(), CacheRequest.Current.ToNative());
            return Automation.WrapNativeElement(child);
        }

        public AutomationElement GetLastChild(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                NativeTreeWalker.GetLastChild(element.ToNative()) :
                NativeTreeWalker.GetLastChild(element.ToNative(), CacheRequest.Current.ToNative());
            return Automation.WrapNativeElement(child);
        }

        public AutomationElement GetNextSibling(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                NativeTreeWalker.GetNextSibling(element.ToNative()) :
                NativeTreeWalker.GetNextSibling(element.ToNative(), CacheRequest.Current.ToNative());
            return Automation.WrapNativeElement(child);
        }

        public AutomationElement GetPreviousSibling(AutomationElement element)
        {
            var child = CacheRequest.Current == null ?
                NativeTreeWalker.GetPreviousSibling(element.ToNative()) :
                NativeTreeWalker.GetPreviousSibling(element.ToNative(), CacheRequest.Current.ToNative());
            return Automation.WrapNativeElement(child);
        }
    }
}
