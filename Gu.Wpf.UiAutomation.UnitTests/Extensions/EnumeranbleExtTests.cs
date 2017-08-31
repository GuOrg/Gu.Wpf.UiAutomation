namespace Gu.Wpf.UiAutomation.UnitTests.Extensions
{
    using NUnit.Framework;

    [TestFixture]
    public class EnumeranbleExtTests
    {
        [TestCase(new int[0], false, 0)]
        [TestCase(new[] { 1, 2 }, false, 0)]
        [TestCase(new[] { 1 }, true, 1)]
        public void TryGetSingle(int[] ints, bool expected, int match)
        {
            Assert.AreEqual(expected, ints.TryGetSingle(out var result));
            Assert.AreEqual(match, result);
        }

        [TestCase(new int[0], false, 0)]
        [TestCase(new[] { 2, 2, 3 }, false, 0)]
        [TestCase(new[] { 1, 1 }, false, 0)]
        [TestCase(new[] { 1, 1, 3 }, false, 0)]
        [TestCase(new[] { 1, 2, 2 }, true, 1)]
        [TestCase(new[] { 1, 2 }, true, 1)]
        [TestCase(new[] { 1 }, true, 1)]
        public void TryGetSingleWithSelector(int[] ints, bool expected, int match)
        {
            Assert.AreEqual(expected, ints.TryGetSingle(x => x == 1, out var result));
            Assert.AreEqual(match, result);
        }
    }
}