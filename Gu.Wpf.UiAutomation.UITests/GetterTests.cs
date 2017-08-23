namespace Gu.Wpf.UiAutomation.UITests
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    public class GetterTests : UITestBase
    {
        public GetterTests()
            : base(TestApplicationType.Wpf)
        {
        }

        protected override Application StartApplication()
        {
            return Application.Launch("notepad.exe");
        }

        [Test]
        public void CorrectPattern()
        {
            var mainWindow = this.App.GetMainWindow();
            Assert.That(mainWindow, Is.Not.Null);
            var windowPattern = mainWindow.BasicAutomationElement.GetNativePattern<object>(mainWindow.Automation.PatternLibrary.WindowPattern);
            Assert.That(windowPattern, Is.Not.Null);
        }

        [Test]
        public void CorrectPatternCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            cacheRequest.Patterns.Add(this.App.Automation.PatternLibrary.WindowPattern);
            using (cacheRequest.Activate())
            {
                var mainWindow = this.App.GetMainWindow();
                Assert.That(mainWindow, Is.Not.Null);
                var windowPattern = mainWindow.BasicAutomationElement.GetNativePattern<object>(mainWindow.Automation.PatternLibrary.WindowPattern);
                Assert.That(windowPattern, Is.Not.Null);
            }
        }

        [Test]
        public void UnsupportedPattern()
        {
            var mainWindow = this.App.GetMainWindow();
            Assert.That(mainWindow, Is.Not.Null);
            ActualValueDelegate<object> testDelegate = () => mainWindow.BasicAutomationElement.GetNativePattern<object>(mainWindow.Automation.PatternLibrary.ExpandCollapsePattern);
            Assert.That(testDelegate, Throws.TypeOf<PatternNotSupportedException>().With.Message.Contains("ExpandCollapse"));
        }

        [Test]
        public void UnsupportedPatternCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            cacheRequest.Patterns.Add(this.App.Automation.PatternLibrary.ExpandCollapsePattern);
            using (cacheRequest.Activate())
            {
                var mainWindow = this.App.GetMainWindow();
                Assert.That(mainWindow, Is.Not.Null);
                ActualValueDelegate<object> testDelegate = () => mainWindow.BasicAutomationElement.GetNativePattern<object>(mainWindow.Automation.PatternLibrary.ExpandCollapsePattern);
                Assert.That(testDelegate, Throws.TypeOf<PatternNotSupportedException>().With.Message.Contains("ExpandCollapse"));
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
            cacheRequest.Patterns.Add(this.App.Automation.PatternLibrary.ExpandCollapsePattern);
            using (cacheRequest.Activate())
            {
                var mainWindow = this.App.GetMainWindow();
                Assert.That(mainWindow, Is.Not.Null);
                ActualValueDelegate<object> testDelegate = () => mainWindow.BasicAutomationElement.GetNativePattern<object>(this.App.Automation.PatternLibrary.WindowPattern);
                Assert.That(testDelegate, Throws.TypeOf<PatternNotCachedException>().With.Message.Contains("Window"));
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
            cacheRequest.Patterns.Add(this.App.Automation.PatternLibrary.WindowPattern);
            using (cacheRequest.Activate())
            {
                var mainWindow = this.App.GetMainWindow();
                Assert.That(mainWindow, Is.Not.Null);
                ActualValueDelegate<object> testDelegate = () => mainWindow.BasicAutomationElement.GetNativePattern<object>(mainWindow.Automation.PatternLibrary.ExpandCollapsePattern);
                Assert.That(testDelegate, Throws.TypeOf<PatternNotCachedException>().With.Message.Contains("ExpandCollapse"));
            }
        }

        [Test]
        public void CorrectProperty()
        {
            var mainWindow = this.App.GetMainWindow();
            Assert.That(mainWindow, Is.Not.Null);
            var windowProperty = mainWindow.BasicAutomationElement.GetPropertyValue(mainWindow.Automation.PropertyLibrary.Window.CanMaximize);
            Assert.That(windowProperty, Is.Not.Null);
        }

        [Test]
        public void CorrectPropertyCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            cacheRequest.Properties.Add(this.App.Automation.PropertyLibrary.Window.CanMaximize);
            using (cacheRequest.Activate())
            {
                var mainWindow = this.App.GetMainWindow();
                Assert.That(mainWindow, Is.Not.Null);
                var windowProperty = mainWindow.BasicAutomationElement.GetPropertyValue(mainWindow.Automation.PropertyLibrary.Window.CanMaximize);
                Assert.That(windowProperty, Is.Not.Null);
            }
        }

        [Test]
        public void UnsupportedProperty()
        {
            var mainWindow = this.App.GetMainWindow();
            Assert.That(mainWindow, Is.Not.Null);
            ActualValueDelegate<object> testDelegate = () => mainWindow.BasicAutomationElement.GetPropertyValue(mainWindow.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
            Assert.That(testDelegate, Throws.TypeOf<PropertyNotSupportedException>().With.Message.Contains("ExpandCollapseState"));
        }

        [Test]
        public void UnsupportedPropertyCached()
        {
            var cacheRequest = new CacheRequest
            {
                AutomationElementMode = AutomationElementMode.None,
                TreeScope = TreeScope.Element
            };
            cacheRequest.Properties.Add(this.App.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
            using (cacheRequest.Activate())
            {
                var mainWindow = this.App.GetMainWindow();
                Assert.That(mainWindow, Is.Not.Null);
                ActualValueDelegate<object> testDelegate = () => mainWindow.BasicAutomationElement.GetPropertyValue(mainWindow.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
                Assert.That(testDelegate, Throws.TypeOf<PropertyNotSupportedException>().With.Message.Contains("ExpandCollapseState"));
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
            cacheRequest.Properties.Add(this.App.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
            using (cacheRequest.Activate())
            {
                var mainWindow = this.App.GetMainWindow();
                Assert.That(mainWindow, Is.Not.Null);
                ActualValueDelegate<object> testDelegate = () => mainWindow.BasicAutomationElement.GetPropertyValue(mainWindow.Automation.PropertyLibrary.Window.CanMaximize);
                Assert.That(testDelegate, Throws.TypeOf<PropertyNotCachedException>().With.Message.Contains("CanMaximize"));
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
            cacheRequest.Properties.Add(this.App.Automation.PropertyLibrary.Window.CanMaximize);
            using (cacheRequest.Activate())
            {
                var mainWindow = this.App.GetMainWindow();
                Assert.That(mainWindow, Is.Not.Null);
                ActualValueDelegate<object> testDelegate = () => mainWindow.BasicAutomationElement.GetPropertyValue(mainWindow.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
                Assert.That(testDelegate, Throws.TypeOf<PropertyNotCachedException>().With.Message.Contains("ExpandCollapseState"));
            }
        }
    }
}
