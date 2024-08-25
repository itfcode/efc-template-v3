namespace ITFCode.Extensions.DateTimeOffsetExtendors
{
    public static partial class DateTimeOffsetExtensions
    {
        #region Public Methods: DayStart

        public static DateTimeOffset DayStartAt(this DateTimeOffset self, int days = 0) => GetDayStart(self).AddDays(days);
        public static DateTimeOffset DayStart(this DateTimeOffset self) => self.DayStartAt(0);
        public static DateTimeOffset DayStartPrev(this DateTimeOffset self) => self.DayStartAt(-1);
        public static DateTimeOffset DayStartNext(this DateTimeOffset self) => self.DayStartAt(1);

        #endregion

        #region Public Methods: DayEnd

        public static DateTimeOffset DayEndAt(this DateTimeOffset self, int days = 0) => self.DayStartAt(days + 1).AddTicks(-1);
        public static DateTimeOffset DayEnd(this DateTimeOffset self) => self.DayEndAt(0);
        public static DateTimeOffset DayEndPrev(this DateTimeOffset self) => self.DayEndAt(-1);
        public static DateTimeOffset DayEndNext(this DateTimeOffset self) => self.DayEndAt(1);

        #endregion

        #region Private methods 

        private static DateTimeOffset GetDayStart(DateTimeOffset date) => new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, date.Offset);

        #endregion
    }
}