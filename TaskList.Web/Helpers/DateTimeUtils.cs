using System;

namespace TaskList.Web.Helpers
{
    public static class DateTimeUtils
    {
        public static Func<DateTime> Today = () => DateTime.Today;
    }
}