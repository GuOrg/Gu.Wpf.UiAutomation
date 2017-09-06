namespace Gu.Wpf.UiAutomation.UIA3
{
    public class UIA3TextRange2 : UIA3TextRange, ITextRange2
    {
        public UIA3TextRange2(UIA3Automation automation, Interop.UIAutomationClient.IUIAutomationTextRange2 nativeRange)
            : base(automation, nativeRange)
        {
            this.NativeRange2 = nativeRange;
        }

        public Interop.UIAutomationClient.IUIAutomationTextRange2 NativeRange2 { get; }

        public void ShowContextMenu()
        {
            Com.Call(() => this.NativeRange2.ShowContextMenu());
        }
    }
}
