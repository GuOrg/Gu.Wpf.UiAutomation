// ReSharper disable AssignmentIsFullyDiscarded
namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class ExpanderTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        private static readonly string WindowName = "ExpanderWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId", "1")]
        [TestCase("xName", "2")]
        [TestCase("Header", "Header")]
        public void FindExpander(string key, string header)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander(key);
                Assert.AreEqual(header, expander.HeaderText);
                Assert.NotNull(expander.FindTextBlock());
                Assert.IsInstanceOf<Expander>(UiElement.FromAutomationElement(expander.AutomationElement));
            }
        }

        [TestCase("AutomationId", "1")]
        [TestCase("xName", "2")]
        [TestCase("Header", "Header")]
        public void Header(string key, string expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander(key);
                Assert.AreEqual(expected, expander.HeaderText);
                var header = expander.Header;
                Assert.AreEqual(expected, ((ToggleButton)header).Text);
            }
        }

        [TestCase("AutomationId", "1")]
        [TestCase("xName", "2")]
        [TestCase("Header", "3")]
        public void Content(string key, string content)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander(key);
                Assert.AreEqual(content, ((TextBlock)expander.Content).Text);
                Assert.AreEqual(content, ((TextBlock)expander.ContentCollection[0]).Text);
            }
        }

        [Test]
        public void ContentCollection()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander("WithItemsControl");
                expander.IsExpanded = true;
                Assert.AreEqual("WithItemsControl", expander.HeaderText);
                Assert.AreEqual("WithItemsControl", ((ToggleButton)expander.Header).Text);
                Assert.Throws<InvalidOperationException>(() => _ = expander.Content);
                var content = expander.ContentCollection;
                Assert.AreEqual(2, content.Count);
                Assert.AreEqual("1", ((TextBlock)content[0]).Text);
                Assert.AreEqual("2", ((TextBlock)content[1]).Text);
            }
        }

        [Test]
        public void ContentElements()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander("WithItemsControl");
                expander.IsExpanded = true;
                Assert.AreEqual("WithItemsControl", expander.HeaderText);
                Assert.AreEqual("WithItemsControl", ((ToggleButton)expander.Header).Text);
                Assert.Throws<InvalidOperationException>(() => _ = expander.Content);
                var content = expander.ContentElements(x => new TextBlock(x));
                Assert.AreEqual(2, content.Count);
                Assert.AreEqual("1", content[0].Text);
                Assert.AreEqual("2", content[1].Text);
            }
        }

        [Test]
        public void IsExpanded()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander("AutomationId");
                expander.IsExpanded = true;
                Assert.AreEqual(true, expander.IsExpanded);

                expander.IsExpanded = false;
                Assert.AreEqual(false, expander.IsExpanded);

                expander.IsExpanded = true;
                Assert.AreEqual(true, expander.IsExpanded);
            }
        }

        [Test]
        public void ExpandCollapse()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander("AutomationId");
                expander.IsExpanded = true;
                Assert.AreEqual(true, expander.IsExpanded);

                expander.Collapse();
                Assert.AreEqual(false, expander.IsExpanded);

                expander.Expand();
                Assert.AreEqual(true, expander.IsExpanded);
            }
        }
    }
}
