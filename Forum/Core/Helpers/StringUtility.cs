namespace Forum.Core.Helpers
{
    public static class StringUtility
    {
        public const int DefaultSummaryLength = 25;
        public const string SummaryEnd = "...";

        public static string SummarizeText(string text, int maxLength)
        {
            if (maxLength <= 0 || maxLength > text.Length)
                maxLength = DefaultSummaryLength;

            return $"{text.Substring(0, maxLength - 1)}{SummaryEnd}";
        }
    }
}