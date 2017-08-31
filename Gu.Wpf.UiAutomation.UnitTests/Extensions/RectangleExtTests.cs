namespace Gu.Wpf.UiAutomation.UnitTests.Extensions
{
    using System.Windows;
    using NUnit.Framework;

    [TestFixture]
    public class RectangleExtTests
    {
        [Test]
        public void EmptyTest()
        {
            var rectangle = new Rect(0, 0, 0, 0);
            var rectangle2 = new Rect(0, 0, 1, 0);
            var rectangle3 = new Rect(0, 0, 0, 1);
            Assert.AreEqual(true, rectangle.IsZeroes());
            Assert.AreEqual(false, rectangle2.IsZeroes());
            Assert.AreEqual(false, rectangle3.IsZeroes());
        }

        [Test]
        public void CenterTest()
        {
            var rectangle = new Rect(10, 20, 30, 40);
            this.AssertPointsAreSame(rectangle.Center(), new Point(25, 40));
        }

        private void AssertPointsAreSame(Point p1, Point p2)
        {
            Assert.AreEqual(p1.X, p2.X);
            Assert.AreEqual(p1.Y, p2.Y);
        }
    }
}
