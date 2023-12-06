namespace CallplusUtil.Extensions
{
    public static class StringExtension
    {
        public static bool IsNumericInt(this string s)
        {
            int output;
            return int.TryParse(s, out output);
        }

    }
}
