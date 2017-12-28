namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using System;
    using System.Threading;
    using System.Windows.Automation;
    using NUnit.Framework;

    public class InvokePatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void InvokeWithEventTest()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                var tabItem = tab.Items[0];
                var button = tabItem.FindButton("InvokableButton");
                Assert.NotNull(button);
                var invokePattern = button.AutomationElement.InvokePattern();
                Assert.NotNull(invokePattern);
                var invokeFired = false;
                using (var waitHandle = new ManualResetEventSlim(initialState: false))
                {
                    using (button.SubscribeToEvent(
                        InvokePatternIdentifiers.InvokedEvent,
                        TreeScope.Element,
                        (element, id) =>
                        {
                            invokeFired = true;
                            waitHandle.Set();
                        }))
                    {
                        invokePattern.Invoke();
                        var waitResult = waitHandle.Wait(TimeSpan.FromSeconds(1));
                        Assert.AreEqual(true, waitResult);
                        Assert.AreEqual("Invoked!", button.Text);
                        Assert.AreEqual(true, invokeFired);
                    }
                }
            }
        }
    }
}
