namespace Gu.Wpf.UiAutomation.UIA3
{
    using UIA = Interop.UIAutomationClient;

    public class UIA3TextRange2 : UIA3TextRange, ITextRange2
    {
        public UIA3TextRange2(UIA3Automation automation, UIA.IUIAutomationTextRange2 nativeRange)
            : base(automation, nativeRange)
        {
            this.NativeRange2 = nativeRange;
        }

        public UIA.IUIAutomationTextRange2 NativeRange2 { get; }

        public void ShowContextMenu()
        {
            ComCallWrapper.Call(() => this.NativeRange2.ShowContextMenu());
        }
    }
}
