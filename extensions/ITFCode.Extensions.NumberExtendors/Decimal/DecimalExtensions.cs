using System.Collections.Generic;
using System.Linq;

namespace ITFCode.Extensions.NumberExtendors
{
    public static class DecimalExtensions
    {
        public static bool InRange(this decimal self, IEnumerable<decimal> values) => self.InRange<decimal>(values);
        public static bool InRange(this decimal self, params decimal[] values) => self.InRange<decimal>(values);
        public static bool OutRange(this decimal self, IEnumerable<decimal> values) => self.OutRange<decimal>(values);
        public static bool OutRange(this decimal self, params decimal[] values) => self.OutRange<decimal>(values);

        public static bool InRanges(this decimal self, IEnumerable<IEnumerable<decimal>> values) => self.InRanges<decimal>(values);
        public static bool OutRanges(this decimal self, IEnumerable<IEnumerable<decimal>> values) => self.OutRanges<decimal>(values);

        public static bool InInterval(this decimal self, decimal min = decimal.MinValue, decimal max = decimal.MaxValue) => self.InInterval<decimal>(min, max);
        public static bool OutInterval(this decimal self, decimal min = decimal.MinValue, decimal max = decimal.MaxValue) => self.OutInterval<decimal>(min, max);

        public static bool InIntervals(this decimal self, params (decimal, decimal)[] interval) => self.InIntervals<decimal>(interval);
        public static bool OutIntervals(this decimal self, params (decimal, decimal)[] interval) => self.OutIntervals<decimal>(interval);
    }
}