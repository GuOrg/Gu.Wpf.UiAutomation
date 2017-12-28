namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using NUnit.Framework;

    public sealed class GetterTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void CorrectPattern()
        {
            throw new NotImplementedException();
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    var window = app.MainWindow;
            //    var pattern = window.Automation.PatternLibrary.WindowPattern;
            //    var windowPattern = window.AutomationElement.GetNativePattern<object>(pattern);
            //    Assert.NotNull(windowPattern);
            //}
        }

        [Test]
        public void CorrectPatternCached()
        {
            throw new NotImplementedException();
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    var cacheRequest = new CacheRequest
            //    {
            //        AutomationElementMode = AutomationElementMode.None,
            //        TreeScope = TreeScope.Element
            //    };

            //    cacheRequest.Patterns.Add(app.Automation.PatternLibrary.WindowPattern);
            //    using (cacheRequest.Activate())
            //    {
            //        var window = app.MainWindow;
            //        var pattern = window.Automation.PatternLibrary.WindowPattern;
            //        var windowPattern = window.AutomationElement.GetNativePattern<object>(pattern);
            //        Assert.NotNull(windowPattern);
            //    }
            //}
        }

        [Test]
        public void UnsupportedPattern()
        {
            throw new NotImplementedException();
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    var mainWindow = app.MainWindow;
            //    var pattern = mainWindow.Automation.PatternLibrary.ExpandCollapsePattern;
            //    var exception = Assert.Throws<PatternNotSupportedException>(() => mainWindow.AutomationElement.GetNativePattern<object>(pattern));
            //    StringAssert.Contains("ExpandCollapse", exception.Message);
            //}
        }

        [Test]
        public void UnsupportedPatternCached()
        {
            throw new NotImplementedException();
            //var cacheRequest = new CacheRequest
            //{
            //    AutomationElementMode = AutomationElementMode.None,
            //    TreeScope = TreeScope.Element
            //};
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    cacheRequest.Patterns.Add(app.Automation.PatternLibrary.ExpandCollapsePattern);
            //    using (cacheRequest.Activate())
            //    {
            //        var window = app.MainWindow;
            //        var pattern = window.Automation.PatternLibrary.ExpandCollapsePattern;
            //        var exception = Assert.Throws<PatternNotSupportedException>(
            //            () =>
            //                window.AutomationElement.GetNativePattern<object>(pattern));
            //        StringAssert.Contains("ExpandCollapse", exception.Message);
            //    }
            //}
        }

        [Test]
        public void CorrectPatternUncached()
        {
            throw new NotImplementedException();
            //var cacheRequest = new CacheRequest
            //{
            //    AutomationElementMode = AutomationElementMode.None,
            //    TreeScope = TreeScope.Element
            //};
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    cacheRequest.Patterns.Add(app.Automation.PatternLibrary.ExpandCollapsePattern);
            //    using (cacheRequest.Activate())
            //    {
            //        var window = app.MainWindow;
            //        var pattern = app.Automation.PatternLibrary.WindowPattern;
            //        var exception = Assert.Throws<PatternNotCachedException>(
            //            () =>
            //                window.AutomationElement.GetNativePattern<object>(pattern));
            //        StringAssert.Contains("Window", exception.Message);
            //    }
            //}
        }

        [Test]
        public void UnsupportedPatternUnCached()
        {
            throw new NotImplementedException();
            //var cacheRequest = new CacheRequest
            //{
            //    AutomationElementMode = AutomationElementMode.None,
            //    TreeScope = TreeScope.Element
            //};
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    cacheRequest.Patterns.Add(app.Automation.PatternLibrary.WindowPattern);
            //    using (cacheRequest.Activate())
            //    {
            //        var window = app.MainWindow;
            //        var pattern = window.Automation.PatternLibrary.ExpandCollapsePattern;
            //        var exception = Assert.Throws<PatternNotCachedException>(
            //            () =>
            //                window.AutomationElement.GetNativePattern<object>(pattern));
            //        StringAssert.Contains("ExpandCollapse", exception.Message);
            //    }
            //}
        }

        [Test]
        public void CorrectProperty()
        {
            throw new NotImplementedException();
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    var window = app.MainWindow;
            //    var property = window.Automation.PropertyLibrary.Window.CanMaximize;
            //    var windowProperty = window.AutomationElement.GetPropertyValue(property);
            //    Assert.NotNull(windowProperty);
            //}
        }

        [Test]
        public void CorrectPropertyCached()
        {
            throw new NotImplementedException();
            //var cacheRequest = new CacheRequest
            //{
            //    AutomationElementMode = AutomationElementMode.None,
            //    TreeScope = TreeScope.Element
            //};
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    cacheRequest.Properties.Add(app.Automation.PropertyLibrary.Window.CanMaximize);
            //    using (cacheRequest.Activate())
            //    {
            //        var window = app.MainWindow;
            //        var property = window.Automation.PropertyLibrary.Window.CanMaximize;
            //        var windowProperty = window.AutomationElement.GetPropertyValue(property);
            //        Assert.NotNull(windowProperty);
            //    }
            //}
        }

        [Test]
        public void UnsupportedProperty()
        {
            throw new NotImplementedException();
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    var window = app.MainWindow;
            //    var property = window.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState;
            //    var exception = Assert.Throws<PropertyNotSupportedException>(() => window.AutomationElement.GetPropertyValue(property));
            //    StringAssert.Contains("ExpandCollapseState", exception.Message);
            //}
        }

        [Test]
        public void UnsupportedPropertyCached()
        {
            throw new NotImplementedException();
            //var cacheRequest = new CacheRequest
            //{
            //    AutomationElementMode = AutomationElementMode.None,
            //    TreeScope = TreeScope.Element
            //};
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    cacheRequest.Properties.Add(app.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
            //    using (cacheRequest.Activate())
            //    {
            //        var mainWindow = app.MainWindow;
            //        var property = mainWindow.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState;
            //        var exception = Assert.Throws<PropertyNotSupportedException>(() => mainWindow.AutomationElement.GetPropertyValue(property));
            //        StringAssert.Contains("ExpandCollapseState", exception.Message);
            //    }
            //}
        }

        [Test]
        public void CorrectPropertyUncached()
        {
            throw new NotImplementedException();
            //var cacheRequest = new CacheRequest
            //{
            //    AutomationElementMode = AutomationElementMode.None,
            //    TreeScope = TreeScope.Element
            //};
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    cacheRequest.Properties.Add(app.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState);
            //    using (cacheRequest.Activate())
            //    {
            //        var window = app.MainWindow;
            //        var property = window.Automation.PropertyLibrary.Window.CanMaximize;
            //        var exception = Assert.Throws<PropertyNotCachedException>(() => window.AutomationElement.GetPropertyValue(property));
            //        StringAssert.Contains("CanMaximize", exception.Message);
            //    }
            //}
        }

        [Test]
        public void UnsupportedPropertyUnCached()
        {
            throw new NotImplementedException();
            //var cacheRequest = new CacheRequest
            //{
            //    AutomationElementMode = AutomationElementMode.None,
            //    TreeScope = TreeScope.Element
            //};
            //using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            //{
            //    cacheRequest.Properties.Add(app.Automation.PropertyLibrary.Window.CanMaximize);
            //    using (cacheRequest.Activate())
            //    {
            //        var window = app.MainWindow;
            //        var property = window.Automation.PropertyLibrary.ExpandCollapse.ExpandCollapseState;
            //        var exception =
            //            Assert.Throws<PropertyNotCachedException>(
            //                () =>
            //                    window.AutomationElement.GetPropertyValue(property));
            //        StringAssert.Contains("ExpandCollapseState", exception.Message);
            //    }
            //}
        }
    }
}
