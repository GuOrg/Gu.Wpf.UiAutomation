namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System.Runtime.InteropServices;
    using System.Text;

#pragma warning disable CA1060 // Move pinvokes to native methods class
    public static class Oleacc
#pragma warning restore CA1060 // Move pinvokes to native methods class
    {
        [DllImport("oleacc.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetRoleText(AccessibilityRole dwRole, [Out] StringBuilder lpszRole, uint cchRoleMax);

        [DllImport("oleacc.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetStateText(AccessibilityState dwStateBit, [Out] StringBuilder lpszStateBit, uint cchStateBitMax);
    }
}
