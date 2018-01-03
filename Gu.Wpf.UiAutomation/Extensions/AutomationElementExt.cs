namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        public static string Text(this AutomationElement element)
        {
            if (element.TryFindFirst(TreeScope.Children, Conditions.Text, out var text))
            {
                return text.Name();
            }

            if (element.TryGetValuePattern(out var valuePattern))
            {
                return valuePattern.Current.Value;
            }

            return element.Name();
        }
    }
}
