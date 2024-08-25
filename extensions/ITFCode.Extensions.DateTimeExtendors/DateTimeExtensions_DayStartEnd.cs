namespace ITFCode.Extensions.DateTimeExtendors
{
    using System;

    public static partial class DateTimeExtensions
    {
        #region Public methods 'DayStart'

        public static DateTime DayStartAt(this DateTime self, int days) => self.Date.AddDays(days);
        public static DateTime DayStart(this DateTime self) => self.DayStartAt(0);
        public static DateTime DayStartPrev(this DateTime self) => self.DayStartAt(-1);
        public static DateTime DayStartNext(this DateTime self) => self.DayStartAt(1);

        #endregion

        #region Public methods 'DayEnd'

        public static DateTime DayEndAt(this DateTime self, int days) => self.DayStartAt(days + 1).AddTicks(-1);
        public static DateTime DayEnd(this DateTime self) => self.DayEndAt(0);
        public static DateTime DayEndPrev(this DateTime self) => self.DayEndAt(-1);
        public static DateTime DayEndNext(this DateTime self) => self.DayEndAt(1);

        #endregion
    }
}
