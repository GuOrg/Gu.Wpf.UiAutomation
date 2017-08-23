namespace Gu.Wpf.UiAutomation.UITests.Patterns
{
    using System;
    using System.Threading;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class InvokePatternTests : UITestBase
    {
        public InvokePatternTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void InvokeWithEventTest()
        {
            var mainWindow = this.App.GetMainWindow(this.Automation);
            var tab = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tab)).AsTab();
            var tabItem = tab.TabItems[0];
            var button = tabItem.FindFirstDescendant(cf => cf.ByAutomationId("InvokableButton"));
            Assert.That(button, Is.Not.Null);
            var origButtonText = button.Properties.Name.Value;
            var invokePattern = button.Patterns.Invoke.Pattern;
            Assert.That(invokePattern, Is.Not.Null);
            var invokeFired = false;
            using (var waitHandle = new ManualResetEventSlim(initialState: false))
            {
                var registeredEvent = button.RegisterEvent(
                    invokePattern.Events.InvokedEvent,
                    TreeScope.Element,
                    (element, id) =>
                    {
                        invokeFired = true;
                        waitHandle.Set();
                    });
                invokePattern.Invoke();
                var waitResult = waitHandle.Wait(TimeSpan.FromSeconds(1));
                Assert.That(waitResult, Is.True);
                Assert.That(button.Properties.Name, Is.Not.EqualTo(origButtonText));
                Assert.That(invokeFired, Is.True);
                button.RemoveAutomationEventHandler(invokePattern.Events.InvokedEvent, registeredEvent);
            }
        }
    }
}
