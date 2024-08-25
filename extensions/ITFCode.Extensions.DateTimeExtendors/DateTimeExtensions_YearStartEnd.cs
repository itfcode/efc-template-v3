namespace ITFCode.Extensions.DateTimeExtendors
{
    using System;

    public static partial class DateTimeExtensions
    {
        #region Year Start 

        public static DateTime YearStartAt(this DateTime self, int years) => new DateTime(self.Year, 1, 1, 0, 0, 0, self.Kind).AddYears(years);
        public static DateTime YearStart(this DateTime self) => self.YearStartAt(0);
        public static DateTime YearStartPrev(this DateTime self) => self.YearStartAt(-1);
        public static DateTime YearStartNext(this DateTime self) => self.YearStartAt(1);

        #endregion

        #region Year End

        public static DateTime YearEndAt(this DateTime self, int years) => self.YearStartAt(years + 1).AddTicks(-1);
        public static DateTime YearEnd(this DateTime self) => self.YearEndAt(0);
        public static DateTime YearEndPrev(this DateTime self) => self.YearEndAt(-1);
        public static DateTime YearEndNext(this DateTime self) => self.YearEndAt(1);

        #endregion
    }
}
