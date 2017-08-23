namespace Gu.Wpf.UiAutomation
{
    using System;

    [Flags]
    public enum ActivateOptions
    {
        None = 0x00000000,
        DesignMode = 0x00000001,
        NoErrorUI = 0x00000002,
        NoSplashScreen = 0x00000004
    }
}