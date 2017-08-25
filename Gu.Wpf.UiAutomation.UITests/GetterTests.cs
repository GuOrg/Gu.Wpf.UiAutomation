namespace Gu.Wpf.UiAutomation.UITests
{
    using System.IO;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    public class GetterTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void CorrectPattern()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                Assert.That(window, Is.Not.Null);
                var windowPattern = window.BasicAutomationElement.GetNativePattern<object>(window.Automation.PatternLibrary.WindowPattern);
                Assert.That(windowPattern, Is.Not.Null);
            }
        }

        [Test]
        public void CorrectPatternCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            using (var app = Application.Launch(ExeFileName))
            {
                cacheRequest.Patterns.Add(app.Automation.PatternLibrary.WindowPattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    var windowPattern =
                        window.BasicAutomationElement.GetNativePattern<object>(window
                            .Automation.PatternLibrary.WindowPattern);
                    Assert.That(windowPattern, Is.Not.Null);
                }
            }
        }

        [Test]
        public void UnsupportedPattern()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var mainWindow = app.MainWindow();
                Assert.That(mainWindow, Is.Not.Null);
                ActualValueDelegate<object> testDelegate = () =>
                    mainWindow.BasicAutomationElement.GetNativePattern<object>(
                        mainWindow.Automation.PatternLibrary.ExpandCollapsePattern);
                Assert.That(testDelegate, Throws.TypeOf<PatternNotSupportedException>().With.Message.Contains("ExpandCollapse"));
            }
        }

        [Test]
        public void UnsupportedPatternCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            using (var app = Application.Launch(ExeFileName))
            {
                cacheRequest.Patterns.Add(app.Automation.PatternLibrary.ExpandCollapsePattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    ActualValueDelegate<object> testDelegate = () =>
                        window.BasicAutomationElement.GetNativePattern<object>(window
                            .Automation.PatternLibrary.ExpandCollapsePattern);
                    Assert.That(testDelegate, Throws.TypeOf<PatternNotSupportedException>().With.Message.Contains("ExpandCollapse"));
                }
            }
        }

        [Test]
        public void CorrectPatternUncached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            using (var app = Application.Launch(ExeFileName))
            {
                cacheRequest.Patterns.Add(app.Automation.PatternLibrary.ExpandCollapsePattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    ActualValueDelegate<object> testDelegate = () =>
                        window.BasicAutomationElement.GetNativePattern<object>(
                            app.Automation.PatternLibrary.WindowPattern);
                    Assert.That(testDelegate,
                        Throws.TypeOf<PatternNotCachedException>().With.Message.Contains("Window"));
                }
            }
        }

        [Test]
        public void UnsupportedPatternUnCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            using (var app = Application.Launch(ExeFileName))
            {
                cacheRequest.Patterns.Add(app.Automation.PatternLibrary.WindowPattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    ActualValueDelegate<object> testDelegate = () =>
                        window.BasicAutomationElement.GetNativePattern<object>(window
                            .Automation.PatternLibrary.ExpandCollapsePattern);
                    Assert.That(testDelegate,
                        Throws.TypeOf<PatternNotCachedException>().With.Message.Contains("ExpandCollapse"));
                }
            }
        }

        [Test]
        public void CorrectProperty()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                Assert.That(window, Is.Not.Null);
                var windowProperty = window.BasicAutomationElement.GetPropertyValue(window.Automation.PropertyLibrary.Window.CanMaximize);
                Assert.That(windowProperty, Is.Not.Null);
            }
        }

        [Test]
        public void CorrectPropertyCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            using (var app = Application.Launch(ExeFileName))
            {
                cacheRequest.Properties.Add(app.Automation.PropertyLibrary.Window.CanMaximize);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    var windowProperty =
                        window.BasicAutomationElement.GetPropertyValue(window
                            .Automation.PropertyLibrary.Window.CanMaximize);
                    Assert.That(windowProperty, Is.Not.Null);
                }
            }
        }

        [Test]
        public void UnsupportedProperty()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                Assert.That(window, Is.Not.Null);
                ActualValueDelegate<object> testDelegate = () =>
                    window.BasicAutomationElement.GetPropertyValue(window
                        .Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
                Assert.That(testDelegate,
                    Throws.TypeOf<PropertyNotSupportedException>().With.Message.Contains("ExpandCollapseState"));
            }
        }

        [Test]
        public void UnsupportedPropertyCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            using (var app = Application.Launch(ExeFileName))
            {
                cacheRequest.Properties.Add(app.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
                using (cacheRequest.Activate())
                {
                    var mainWindow = app.MainWindow();
                    Assert.That(mainWindow, Is.Not.Null);
                    ActualValueDelegate<object> testDelegate = () =>
                        mainWindow.BasicAutomationElement.GetPropertyValue(mainWindow
                            .Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
                    Assert.That(testDelegate,
                        Throws.TypeOf<PropertyNotSupportedException>().With.Message.Contains("ExpandCollapseState"));
                }
            }
        }

        [Test]
        public void CorrectPropertyUncached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            using (var app = Application.Launch(ExeFileName))
            {
                cacheRequest.Properties.Add(app.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    ActualValueDelegate<object> testDelegate = () =>
                        window.BasicAutomationElement.GetPropertyValue(window
                            .Automation.PropertyLibrary.Window.CanMaximize);
                    Assert.That(testDelegate,
                        Throws.TypeOf<PropertyNotCachedException>().With.Message.Contains("CanMaximize"));
                }
            }
        }

        [Test]
        public void UnsupportedPropertyUnCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            using (var app = Application.Launch(ExeFileName))
            {
                cacheRequest.Properties.Add(app.Automation.PropertyLibrary.Window.CanMaximize);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow();
                    var exception = Assert.Throws<PropertyNotCachedException>(() =>
                    {
                        var property = window.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState;
                        window.BasicAutomationElement.GetPropertyValue(property);
                    });

                    StringAssert.Contains("ExpandCollapseState", exception.Message);
                }
            }
        }
    }
}
