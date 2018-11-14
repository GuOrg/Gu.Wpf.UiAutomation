namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System.Runtime.InteropServices;
    using System.Text;

    public static class Oleacc
    {
        [DllImport("oleacc.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetRoleText(AccessibilityRole dwRole, [Out] StringBuilder lpszRole, uint cchRoleMax);

        [DllImport("oleacc.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetStateText(AccessibilityState dwStateBit, [Out] StringBuilder lpszStateBit, uint cchStateBitMax);
    }
}
