using System.Collections.Generic;
using System.Linq;

namespace ITFCode.Extensions.NumberExtendors
{
    public static class Int64Extensions
    {
        public static bool InRange(this long self, IEnumerable<long> values) => self.InRange<long>(values);
        public static bool InRange(this long self, params long[] values) => self.InRange<long>(values);
        public static bool OutRange(this long self, IEnumerable<long> values) => self.OutRange<long>(values);
        public static bool OutRange(this long self, params long[] values) => self.OutRange<long>(values);

        public static bool InRanges(this long self, IEnumerable<IEnumerable<long>> values) => self.InRanges<long>(values);
        public static bool OutRanges(this long self, IEnumerable<IEnumerable<long>> values) => self.OutRanges<long>(values);

        public static bool InInterval(this long self, long min = long.MinValue, long max = long.MaxValue) => self.InInterval<long>(min, max);
        public static bool OutInterval(this long self, long min = long.MinValue, long max = long.MaxValue) => self.OutInterval<long>(min, max);

        public static bool InIntervals(this long self, params (long, long)[] interval) => self.InIntervals<long>(interval);
        public static bool OutIntervals(this long self, params (long, long)[] interval) => self.OutIntervals<long>(interval);
    }
}
