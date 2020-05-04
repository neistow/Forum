using Forum.Core.Helpers;
using NUnit.Framework;
    
namespace UnitTests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        private readonly string text = "This is gonna be a really really really really long text";

        [Test]
        [TestCase(15)]
        [TestCase(5)]
        public void SummarizeText_MaxLengthGreaterThanZeroAndLessThanTextLength_ReturnSummaryOfSpecifiedLength(
            int maxLength)
        {
            var result = StringUtility.SummarizeText(text, maxLength);

            Assert.That(result.Length, Is.GreaterThanOrEqualTo(maxLength));
            Assert.That(result, Does.EndWith(StringUtility.SummaryEnd));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(1000)]
        public void SummarizeText_MaxLengthZeroNegativeOrGreaterThanTextLength_ReturnSummaryOfDefaultLength(
            int maxLength)
        {
            var result = StringUtility.SummarizeText(text, maxLength);
                                
            Assert.That(result.Length, Is.GreaterThanOrEqualTo(StringUtility.DefaultSummaryLength));
            Assert.That(result, Does.EndWith(StringUtility.SummaryEnd));
        }
    }
}