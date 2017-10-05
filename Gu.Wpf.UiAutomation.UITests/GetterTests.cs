namespace Gu.Wpf.UiAutomation.UiTests
{
    using NUnit.Framework;

    public sealed class GetterTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void CorrectPattern()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                var pattern = window.Automation.PatternLibrary.WindowPattern;
                var windowPattern = window.BasicAutomationElement.GetNativePattern<object>(pattern);
                Assert.NotNull(windowPattern);
            }
        }

        [Test]
        public void CorrectPatternCached()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var cacheRequest = new CacheRequest
                {
                    AutomationElementMode = AutomationElementMode.None,
                    TreeScope = TreeScope.Element
                };

                cacheRequest.Patterns.Add(app.Automation.PatternLibrary.WindowPattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    var pattern = window.Automation.PatternLibrary.WindowPattern;
                    var windowPattern = window.BasicAutomationElement.GetNativePattern<object>(pattern);
                    Assert.NotNull(windowPattern);
                }
            }
        }

        [Test]
        public void UnsupportedPattern()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var mainWindow = app.MainWindow;
                var pattern = mainWindow.Automation.PatternLibrary.ExpandCollapsePattern;
                var exception = Assert.Throws<PatternNotSupportedException>(() => mainWindow.BasicAutomationElement.GetNativePattern<object>(pattern));
                StringAssert.Contains("ExpandCollapse", exception.Message);
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
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                cacheRequest.Patterns.Add(app.Automation.PatternLibrary.ExpandCollapsePattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    var pattern = window.Automation.PatternLibrary.ExpandCollapsePattern;
                    var exception = Assert.Throws<PatternNotSupportedException>(
                        () =>
                            window.BasicAutomationElement.GetNativePattern<object>(pattern));
                    StringAssert.Contains("ExpandCollapse", exception.Message);
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
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                cacheRequest.Patterns.Add(app.Automation.PatternLibrary.ExpandCollapsePattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    var pattern = app.Automation.PatternLibrary.WindowPattern;
                    var exception = Assert.Throws<PatternNotCachedException>(
                        () =>
                            window.BasicAutomationElement.GetNativePattern<object>(pattern));
                    StringAssert.Contains("Window", exception.Message);
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
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                cacheRequest.Patterns.Add(app.Automation.PatternLibrary.WindowPattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    var pattern = window.Automation.PatternLibrary.ExpandCollapsePattern;
                    var exception = Assert.Throws<PatternNotCachedException>(
                        () =>
                            window.BasicAutomationElement.GetNativePattern<object>(pattern));
                    StringAssert.Contains("ExpandCollapse", exception.Message);
                }
            }
        }

        [Test]
        public void CorrectProperty()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                var property = window.Automation.PropertyLibrary.Window.CanMaximize;
                var windowProperty = window.BasicAutomationElement.GetPropertyValue(property);
                Assert.NotNull(windowProperty);
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
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                cacheRequest.Properties.Add(app.Automation.PropertyLibrary.Window.CanMaximize);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    var property = window.Automation.PropertyLibrary.Window.CanMaximize;
                    var windowProperty = window.BasicAutomationElement.GetPropertyValue(property);
                    Assert.NotNull(windowProperty);
                }
            }
        }

        [Test]
        public void UnsupportedProperty()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                var property = window.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState;
                var exception = Assert.Throws<PropertyNotSupportedException>(() => window.BasicAutomationElement.GetPropertyValue(property));
                StringAssert.Contains("ExpandCollapseState", exception.Message);
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
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                cacheRequest.Properties.Add(app.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
                using (cacheRequest.Activate())
                {
                    var mainWindow = app.MainWindow;
                    var property = mainWindow.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState;
                    var exception = Assert.Throws<PropertyNotSupportedException>(() => mainWindow.BasicAutomationElement.GetPropertyValue(property));
                    StringAssert.Contains("ExpandCollapseState", exception.Message);
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
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                cacheRequest.Properties.Add(app.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    var property = window.Automation.PropertyLibrary.Window.CanMaximize;
                    var exception = Assert.Throws<PropertyNotCachedException>(() => window.BasicAutomationElement.GetPropertyValue(property));
                    StringAssert.Contains("CanMaximize", exception.Message);
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
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                cacheRequest.Properties.Add(app.Automation.PropertyLibrary.Window.CanMaximize);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    var property = window.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState;
                    var exception =
                        Assert.Throws<PropertyNotCachedException>(
                            () =>
                                window.BasicAutomationElement.GetPropertyValue(property));
                    StringAssert.Contains("ExpandCollapseState", exception.Message);
                }
            }
        }
    }
}
