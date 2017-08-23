namespace Gu.Wpf.UiAutomation
{
    using System.Linq;
    using System.Text;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public static class AccessibilityTextResolver
    {
        public static string GetRoleText(AccessibilityRole role)
        {
            var sb = new StringBuilder(1024);
            Oleacc.GetRoleText(role, sb, 1024);
            return sb.ToString();
        }

        public static string GetStateBitText(AccessibilityState state)
        {
            var sb = new StringBuilder(1024);
            Oleacc.GetStateText(state, sb, 1024);
            return sb.ToString();
        }

        public static string GetStateText(AccessibilityState state)
        {
            var allStates = state.GetFlags();
            return string.Join(", ", allStates.Select(s => GetStateBitText((AccessibilityState)s)).ToArray());
        }
    }
}
