namespace ITFCode.Extensions.DateTimeExtendors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static partial class DateTimeExtensions
    {
        public static bool IsHoliday(this DateTime self, IEnumerable<DateTime> holidays)
            => holidays != null && holidays.Any(x => x == self);

        public static DateTime CopyTime(this DateTime self, DateTime source)
            => new(self.Year, self.Month, self.Day, source.Hour, source.Minute, source.Second);

    }
}
