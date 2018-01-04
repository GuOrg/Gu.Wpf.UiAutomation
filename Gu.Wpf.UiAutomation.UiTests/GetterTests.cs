namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using System.Windows.Automation;
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
                var windowPattern = window.AutomationElement.GetCurrentPattern(WindowPattern.Pattern);
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

                cacheRequest.Add(WindowPattern.Pattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    var pattern = window.AutomationElement.GetCachedPattern(WindowPattern.Pattern);
                    Assert.NotNull(pattern);
                }
            }
        }

        [Test]
        public void UnsupportedPatternThrows()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                Assert.Throws<InvalidOperationException>(() => window.AutomationElement.GetCurrentPattern(ExpandCollapsePattern.Pattern));
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
                cacheRequest.Add(ExpandCollapsePattern.Pattern);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    Assert.Throws<InvalidOperationException>(() => window.AutomationElement.GetCurrentPattern(ExpandCollapsePattern.Pattern));
                }
            }
        }

        [Test]
        public void CorrectProperty()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                var windowProperty = window.AutomationElement.GetCurrentPropertyValue(WindowPatternIdentifiers.CanMaximizeProperty);
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
                cacheRequest.Add(WindowPatternIdentifiers.CanMaximizeProperty);
                using (cacheRequest.Activate())
                {
                    var window = app.MainWindow;
                    var windowProperty = window.AutomationElement.GetCachedPropertyValue(WindowPatternIdentifiers.CanMaximizeProperty);
                    Assert.NotNull(windowProperty);
                }
            }
        }
    }
}
