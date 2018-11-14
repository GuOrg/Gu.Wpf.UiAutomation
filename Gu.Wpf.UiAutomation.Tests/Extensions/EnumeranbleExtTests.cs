namespace Gu.Wpf.UiAutomation.Tests.Extensions
{
    using System;
    using System.Collections;
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
        public void TryGetSingleWithPredicate(string text, bool expected, int match)
        {
            var ints = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();
            Assert.AreEqual(expected, ints.TryGetSingle(x => x == 1, out var result));
            Assert.AreEqual(match, result);
        }

        public IEnumerable Get(int[] xs)
        {
            foreach (var x in xs)
            {
                yield return x;
            }
        }
    }
}
