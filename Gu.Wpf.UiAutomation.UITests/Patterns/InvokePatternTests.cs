namespace Gu.Wpf.UiAutomation.UITests.Patterns
{
    using System;
    using System.IO;
    using System.Threading;
    using NUnit.Framework;

    public class InvokePatternTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

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
                var invokePattern = button.Patterns.Invoke.Pattern;
                Assert.NotNull(invokePattern);
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
                    Assert.AreEqual(true, waitResult);
                    Assert.AreEqual("Invoked!", button.Text);
                    Assert.AreEqual(true, invokeFired);
                    button.RemoveAutomationEventHandler(invokePattern.Events.InvokedEvent, registeredEvent);
                }
            }
        }
    }
}
