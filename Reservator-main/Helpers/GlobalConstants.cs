using System.Globalization;

namespace Reservator.Helpers
{
    public static class GlobalConstants
    {
        public static string Currency = "lv.";
        public static string ApplicationName = "Reservator";
        // 15-03-1994 г.
        private static string dateFormat = "dd-MM-yyyy г."; //

        public static string FormatDate(DateTime date)
        {
            return date.ToString(dateFormat);
        }


    }
}
