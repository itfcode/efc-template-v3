using System;
using System.Collections.Generic;
using System.Linq;

namespace ITFCode.Extensions.NumberExtendors
{
    public static class DecimalNullableExtensions
    {
        public static bool InRange(this decimal? self, bool throwIfNull = false, IEnumerable<decimal> values = null) =>
            Helper.NullChecking(self, throwIfNull, values).HasValue && self.Value.InRange(values);

        public static bool InRange(this decimal? self, bool throwIfNull = false, params decimal[] values) =>
            Helper.NullChecking(self, throwIfNull, values).HasValue && self.Value.InRange(values);

        public static bool InRanges(this decimal? self, bool throwIfNull = false, IEnumerable<IEnumerable<decimal>> values = null) =>
            Helper.NullChecking(self, throwIfNull, values).HasValue && values.Any(x => self.Value.InRange(x));

        public static bool OutRange(this decimal? self, bool throwIfNull = false, IEnumerable<decimal> values = null) =>
            Helper.NullChecking(self, throwIfNull, values).HasValue && !self.Value.InRange(values);

        public static bool OutRange(this decimal? self, bool throwIfNull = false, params decimal[] values) =>
            Helper.NullChecking(self, throwIfNull, values).HasValue && !self.Value.InRange(values);

        public static bool OutRanges(this decimal? self, bool throwIfNull = false, IEnumerable<IEnumerable<decimal>> values = null) =>
            Helper.NullChecking(self, throwIfNull, values).HasValue ? !self.Value.InRanges(values) : false;

        public static bool InInterval(this decimal? self, bool throwIfNull = false, decimal min = decimal.MinValue, decimal max = decimal.MaxValue) =>
            Helper.NullChecking(self, throwIfNull).HasValue ? self.Value.InInterval(min, max) : false;

        //public static bool InInterval(this decimal? self, bool throwIfNull = false, (decimal, decimal)? interval = null) =>
        //    Helper.NullChecking(self, throwIfNull).HasValue ? self.Value.InInterval(interval.Item1, interval.Item1) : false;

        public static bool OutInterval(this decimal? self, bool throwIfNull = false, decimal min = decimal.MinValue, decimal max = decimal.MaxValue) =>
            Helper.NullChecking(self, throwIfNull).HasValue ? !self.Value.InInterval(min, max) : false;

        public static bool InIntervals(this decimal? self, bool throwIfNull = false, params (decimal, decimal)[] intervals) =>
            Helper.NullChecking(self, throwIfNull).HasValue ? self.Value.InIntervals(intervals) : false;

        public static bool OutIntervals(this decimal? self, bool throwIfNull = false, params (decimal, decimal)[] intervals) =>
            Helper.NullChecking(self, throwIfNull).HasValue ? !self.Value.InIntervals(intervals) : false;
    }
}
