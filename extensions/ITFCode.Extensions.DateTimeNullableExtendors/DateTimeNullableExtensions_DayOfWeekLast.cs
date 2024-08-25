namespace ITFCode.Extensions.DateTimeNullableExtendors
{
    public static partial class DateTimeNullableExtensions
    {
        #region  Day Last

        public static DateTime? MondayLast(this DateTime? self, bool throwIfNull = true)
            => GetLastDayOfWeekForMonth(self, DayOfWeek.Monday, throwIfNull);

        public static DateTime? TuesdayLast(this DateTime? self, bool throwIfNull = true)
            => GetLastDayOfWeekForMonth(self, DayOfWeek.Tuesday, throwIfNull);

        public static DateTime? WednesdayLast(this DateTime? self, bool throwIfNull = true)
            => GetLastDayOfWeekForMonth(self, DayOfWeek.Wednesday, throwIfNull);

        public static DateTime? ThursdayLast(this DateTime? self, bool throwIfNull = true)
            => GetLastDayOfWeekForMonth(self, DayOfWeek.Thursday, throwIfNull);

        public static DateTime? FridayLast(this DateTime? self, bool throwIfNull = true)
            => GetLastDayOfWeekForMonth(self, DayOfWeek.Friday, throwIfNull);

        public static DateTime? SaturdayLast(this DateTime? self, bool throwIfNull = true)
            => GetLastDayOfWeekForMonth(self, DayOfWeek.Saturday, throwIfNull);

        public static DateTime? SundayLast(this DateTime? self, bool throwIfNull = true)
            => GetLastDayOfWeekForMonth(self, DayOfWeek.Sunday, throwIfNull);

        #endregion

        #region Private Methods 

        private static DateTime? GetLastDayOfWeekForMonth(DateTime? date, DayOfWeek dayOfWeek, bool throwIfNull = true)
        {
            if (!date.HasValue && throwIfNull)
                throw new ArgumentNullException(nameof(date), $"Method {GetFirstDayOfWeekForMonth}()");
            else if (!date.HasValue)
                return date;

            var value = date.Value;
            var last = new DateTime(value.Year, value.Month, 1, 0, 0, 0, value.Kind).AddMonths(1).AddDays(-1);

            while (last.DayOfWeek != dayOfWeek)
                last = last.AddDays(-1);

            return last.Date;
        }

        #endregion
    }
}