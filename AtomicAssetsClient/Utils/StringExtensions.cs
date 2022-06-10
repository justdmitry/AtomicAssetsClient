namespace System
{
    public static class StringExtensions
    {
        public static string SubstringSafe(this string source, int maxCount)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            if (source.Length > maxCount)
            {
                return source[..maxCount];
            }

            return source;
        }
    }
}
