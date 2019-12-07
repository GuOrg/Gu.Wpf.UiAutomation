namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using System.Windows.Automation;
    using NUnit.Framework;

    public class ExpandCollapsePatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void ExpanderTest()
        {
            using var app = Application.Launch(ExeFileName, "ExpanderWindow");
            var window = app.MainWindow;
            var expander = window.FindExpander();
            Assert.NotNull(expander);
            var ecp = expander.AutomationElement.ExpandCollapsePattern();
            Assert.AreEqual(ExpandCollapseState.Expanded, ecp.Current.ExpandCollapseState);

            ecp.Collapse();
            Assert.AreEqual(ExpandCollapseState.Collapsed, ecp.Current.ExpandCollapseState);

            ecp.Expand();
            Assert.AreEqual(ExpandCollapseState.Expanded, ecp.Current.ExpandCollapseState);
        }
    }
}
