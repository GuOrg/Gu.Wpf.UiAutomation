namespace Gu.Wpf.UiAutomation.Converters
{
    public static class FrameworkIds
    {
        public static FrameworkType ConvertToFrameworkType(string frameworkId)
        {
            return frameworkId switch
            {
#pragma warning disable SA1122 // Use string.Empty for empty strings
                "" => FrameworkType.None,
#pragma warning restore SA1122 // Use string.Empty for empty strings
                "WPF" => FrameworkType.Wpf,
                "WinForm" => FrameworkType.WinForms,
                "Win32" => FrameworkType.Win32,
                "XAML" => FrameworkType.Xaml,
                _ => FrameworkType.Unknown,
            };
        }
    }
}
