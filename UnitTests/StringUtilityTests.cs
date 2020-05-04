using System.Linq;
using Forum.Core.Helpers;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        private readonly string _text = "This is gonna be a really really really really long text";

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void SummarizeText_WordCountInRange_ReturnSummarizedText(int wordCount)
        {
            var result = StringUtility.SummarizeText(_text, wordCount).Split();

            Assert.That(result.Length, Is.EqualTo(wordCount));
            Assert.That(result.Last(), Does.EndWith("..."));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public void SummarizeText_WordCountIsOutOfRange_ReturnWholeText(int wordCount)
        {
            var result = StringUtility.SummarizeText(_text, wordCount).Split();
            var expectedLength = _text.Split().Length;

            Assert.That(result.Length, Is.EqualTo(expectedLength));
        }
    }
}