namespace Gu.Wpf.UiAutomation.UITests.Patterns
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class ExpandCollapsePatternTests : UITestBase
    {
        private AutomationElement expander;

        public ExpandCollapsePatternTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [OneTimeSetUp]
        public void SelectTab()
        {
            var mainWindow = this.App.MainWindow();
            var tab = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tab)).AsTab();
            var tabItem = tab.SelectTabItem(1);
            this.expander = tabItem.FindFirstNested(cf => new ConditionBase[] { cf.ByControlType(ControlType.Pane), cf.ByAutomationId("Expander") });
        }

        [Test]
        public void ExpanderTest()
        {
            var expander = this.expander;
            Assert.That(expander, Is.Not.Null);
            var ecp = expander.Patterns.ExpandCollapse.Pattern;
            Assert.That(ecp, Is.Not.Null);
            Assert.That(ecp.ExpandCollapseState.Value, Is.EqualTo(ExpandCollapseState.Collapsed));
            ecp.Expand();
            Assert.That(ecp.ExpandCollapseState.Value, Is.EqualTo(ExpandCollapseState.Expanded));
            ecp.Collapse();
            Assert.That(ecp.ExpandCollapseState.Value, Is.EqualTo(ExpandCollapseState.Collapsed));
        }
    }
}
