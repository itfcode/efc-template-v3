namespace ITFCode.Extensions.DateTimeNullableExtendors
{
    public static partial class DateTimeNullableExtensions
    {
        #region Year Start 

        public static DateTime? YearStartAt(this DateTime? self, int years, bool throwIfNull = true)
            => Exec(self, nameof(YearStartAt), d => GetYearStart(d).Value.AddYears(years), throwIfNull);

        public static DateTime? YearStart(this DateTime? self, bool throwIfNull = true)
            => Exec(self, nameof(YearStart), d => d.YearStartAt(0), throwIfNull);

        public static DateTime? YearStartPrev(this DateTime? self, bool throwIfNull = true)
            => Exec(self, nameof(YearStart), d => d.YearStartAt(-1), throwIfNull);

        public static DateTime? YearStartNext(this DateTime? self, bool throwIfNull = true)
            => Exec(self, nameof(YearStart), d => d.YearStartAt(1), throwIfNull);

        #endregion

        #region Year End

        public static DateTime? YearEndAt(this DateTime? self, int years, bool throwIfNull = true)
            => Exec(self, nameof(YearEndAt), d => d.YearEndAt(years + 1).Value.AddTicks(-1), throwIfNull);

        public static DateTime? YearEnd(this DateTime? self, bool throwIfNull = true)
            => Exec(self, nameof(YearEnd), d => d.YearEndAt(0), throwIfNull);

        public static DateTime? YearEndPrev(this DateTime? self, bool throwIfNull = true)
            => Exec(self, nameof(YearEndPrev), d => d.YearEndAt(-1), throwIfNull);

        public static DateTime? YearEndNext(this DateTime? self, bool throwIfNull = true)
            => Exec(self, nameof(YearEndNext), d => d.YearEndAt(1), throwIfNull);

        #endregion

        #region Private Methods 

        private static DateTime? GetYearStart(DateTime? date, bool throwIfNull = true)
        {
            if (!date.HasValue && throwIfNull)
                throw new ArgumentNullException(nameof(date), $"Method {nameof(GetYearStart)}()");
            else if (!date.HasValue)
                return date;

            var value = date.Value;
            return new DateTime(value.Year, 1, 1, 0, 0, 0, value.Kind);
        }

        #endregion
    }
}