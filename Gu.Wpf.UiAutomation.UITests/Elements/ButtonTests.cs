namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class ButtonTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
            Retry.ResetTime();
        }

        [TestCase(null)]
        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindButton(string key)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton(key);
                Assert.NotNull(button);
            }
        }

        [TestCase("AutomationId", "AutomationProperties.AutomationId")]
        [TestCase("XName", "x:Name")]
        [TestCase("Content", "Content")]
        public void Text(string key, string expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton(key);
                Assert.AreEqual(expected, button.Text);
            }
        }

        [TestCase("AutomationId", "AutomationProperties.AutomationId")]
        [TestCase("XName", "x:Name")]
        [TestCase("Content", "Content")]
        public void Content(string key, string expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton(key);
                Assert.AreEqual(expected, button.Content.AsTextBlock().Text);
            }
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindButtonThrowsWhenNotFound(string key)
        {
            Retry.Time = TimeSpan.FromMilliseconds(10);
            using (var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                var exception = Assert.Throws<InvalidOperationException>(() => window.FindButton(key));
                Assert.AreEqual($"Did not find a Button matching (ControlType: Button AND (Name: {key} OR AutomationId: {key})).", exception.Message);
            }
        }

        [Test]
        public void Click()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Click();
                Assert.AreEqual("1", textBlock.Text);
            }
        }

        [Test]
        public void ClickThrice()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Click();
                Assert.AreEqual("1", textBlock.Text);

                button.Click();
                Assert.AreEqual("2", textBlock.Text);

                button.Click();
                Assert.AreEqual("3", textBlock.Text);
            }
        }

        [Test]
        public void Invoke()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("1", textBlock.Text);
            }
        }

        [Test]
        public void InvokeThrice()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("1", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("2", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("3", textBlock.Text);
            }
        }

        [Test]
        public void ClickSleep()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Sleep Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Click();
                Assert.AreEqual("1", textBlock.Text);
            }
        }

        [Test]
        public void ClickSleepThrice()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Sleep Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Click();
                Assert.AreEqual("1", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("2", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("3", textBlock.Text);
            }
        }

        [Test]
        public void InvokeSleep()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Sleep Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("1", textBlock.Text);
            }
        }

        [Test]
        public void InvokeSleepThrice()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Sleep Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("1", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("2", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("3", textBlock.Text);
            }
        }

        [Test]
        public void ClickThenInvoke()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Click();
                Assert.AreEqual("1", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("2", textBlock.Text);
            }
        }

        [Test]
        public void LaunchClickCloseLaunchInvoke()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Click();
                Assert.AreEqual("1", textBlock.Text);
            }

            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Invoke();
                Assert.AreEqual("1", textBlock.Text);
            }
        }
    }
}