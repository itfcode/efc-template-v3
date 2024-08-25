namespace ITFCode.Extensions.DateTimeExtendors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static partial class DateTimeExtensions
    {
        public static DateTime ResetHours(this DateTime self)
            => self.Date;

        public static DateTime ResetMinutes(this DateTime self)
            => self.ResetHours().AddHours(self.Hour);

        public static DateTime ResetSeconds(this DateTime self)
            => self.ResetMinutes().AddMinutes(self.Minute);

        public static DateTime ResetMilliseconds(this DateTime self)
            => self.ResetSeconds().AddSeconds(self.Second);
    }
}
