namespace Gu.Wpf.UiAutomation.UnitTests.Extensions
{
    using System;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class EnumeranbleExtTests
    {
        [TestCase("", false, 0)]
        [TestCase("1, 2", false, 0)]
        [TestCase("1", true, 1)]
        public void TryGetSingle(string text, bool expected, int match)
        {
            var ints = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(int.Parse)
                           .ToArray();
            Assert.AreEqual(expected, ints.TryGetSingle(out var result));
            Assert.AreEqual(match, result);
        }

        [TestCase("", false, 0)]
        [TestCase("2, 2, 3", false, 0)]
        [TestCase("1, 1", false, 0)]
        [TestCase("1, 1, 3", false, 0)]
        [TestCase("1, 2, 2", true, 1)]
        [TestCase("1, 2", true, 1)]
        [TestCase("1", true, 1)]
        public void TryGetSingleWithSelector(string text, bool expected, int match)
        {
            var ints = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();
            Assert.AreEqual(expected, ints.TryGetSingle(x => x == 1, out var result));
            Assert.AreEqual(match, result);
        }
    }
}