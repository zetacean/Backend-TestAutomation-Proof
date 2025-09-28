namespace Zetacean.BETEAP.Students.Helpers
{
    public static class StringHelper
    {
        public static string UppercaseFirstWord(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            value = value.ToLower();
            return char.ToUpper(value[0]) + value[1..];
        }
    }
}
