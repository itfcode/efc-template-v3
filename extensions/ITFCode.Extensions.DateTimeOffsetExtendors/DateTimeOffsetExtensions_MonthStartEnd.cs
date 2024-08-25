namespace ITFCode.Extensions.DateTimeOffsetExtendors
{
    public static partial class DateTimeOffsetExtensions
    {
        #region Publlic methods: 'MonthStart'

        public static DateTimeOffset MonthStartAt(this DateTimeOffset self, int months = 0) => GetMonthStart(self).AddMonths(months);
        public static DateTimeOffset MonthStart(this DateTimeOffset self) => self.MonthStartAt(0);
        public static DateTimeOffset MonthStartPrev(this DateTimeOffset self) => self.MonthStartAt(-1);
        public static DateTimeOffset MonthStartNext(this DateTimeOffset self) => self.MonthStartAt(1);

        #endregion

        #region Public methods: 'MonthEnd'

        public static DateTimeOffset MonthEndAt(this DateTimeOffset self, int months = 0) => self.MonthStartAt(months + 1).AddTicks(-1);
        public static DateTimeOffset MonthEnd(this DateTimeOffset self) => self.MonthEndAt(0);
        public static DateTimeOffset MonthEndPrev(this DateTimeOffset self) => self.MonthEndAt(-1);
        public static DateTimeOffset MonthEndNext(this DateTimeOffset self) => self.MonthEndAt(1);

        #endregion

        #region Private Mehtods 

        private static DateTimeOffset GetMonthStart(DateTimeOffset date) => new DateTimeOffset(date.Year, date.Month, 1, 0, 0, 0, date.Offset);

        #endregion
    }
}