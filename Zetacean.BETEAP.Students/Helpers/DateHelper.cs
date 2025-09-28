using System.Globalization;

namespace Zetacean.BETEAP.Students.Helpers
{
    public static class DateHelper
    {
        public static bool TryParseDate(string dateString, out DateTime date)
        {
            return DateTime.TryParseExact(
                dateString,
                Constants.DateFormat,
                Constants.Culture,
                DateTimeStyles.None,
                out date
            );
        }

        public static DateTime ParseDate(string dateString) =>
            DateTime.ParseExact(dateString, Constants.DateFormat, Constants.Culture);
    }
}
