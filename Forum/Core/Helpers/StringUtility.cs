using System.Linq;

namespace Forum.Core.Helpers
{
    public static class StringUtility
    {
        private static readonly string SpecialChars = $"\\/:*?<>|!@#$%^&()\"'.,";

        public static string SummarizeText(string text, int wordCount = 30)
        {
            if (wordCount <= 0 || wordCount > text.Length)
            {
                return text;
            }

            var words = text.Split().Take(wordCount);

            var summary = string.Join(" ", words).TrimEnd(SpecialChars.ToCharArray());

            return $"{summary}....";
        }
    }
}