namespace ITFCode.Extensions.DateTimeExtendors
{
    using System;

    public static partial class DateTimeExtensions
    {
        #region Month Start

        public static DateTime MonthStartAt(this DateTime self, int months) => GetMonthStart(self).AddMonths(months);
        public static DateTime MonthStart(this DateTime self) => self.MonthStartAt(0);
        public static DateTime MonthStartPrev(this DateTime self) => self.MonthStartAt(-1);
        public static DateTime MonthStartNext(this DateTime self) => self.MonthStartAt(1);

        #endregion

        #region Month End

        public static DateTime MonthEndAt(this DateTime self, int months) => self.MonthStartAt(months + 1).AddTicks(-1);
        public static DateTime MonthEnd(this DateTime self) => self.MonthEndAt(0);
        public static DateTime MonthEndPrev(this DateTime self) => self.MonthEndAt(-1);
        public static DateTime MonthEndNext(this DateTime self) => self.MonthEndAt(1);

        #endregion

        #region Private Methods 

        private static DateTime GetMonthStart(DateTime date) => new(date.Year, date.Month, 1, 0, 0, 0, date.Kind);

        #endregion
    }
}