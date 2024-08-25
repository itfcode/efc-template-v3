using System.Collections.Generic;

namespace ITFCode.Extensions.NumberExtendors
{
    public static class SByteExtensions
    {
        public static bool InRange(this sbyte self, IEnumerable<sbyte> values) => self.InRange<sbyte>(values);
        public static bool InRange(this sbyte self, params sbyte[] values) => self.InRange<sbyte>(values);

        public static bool OutRange(this sbyte self, IEnumerable<sbyte> values) => self.OutRange<sbyte>(values);
        public static bool OutRange(this sbyte self, params sbyte[] values) => self.OutRange<sbyte>(values);

        public static bool InRanges(this sbyte self, IEnumerable<IEnumerable<sbyte>> values) => self.InRanges<sbyte>(values);
        public static bool OutRanges(this sbyte self, IEnumerable<IEnumerable<sbyte>> values) => self.OutRanges<sbyte>(values);

        public static bool InInterval(this sbyte self, sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue) => self.InInterval<sbyte>(min, max);
        public static bool OutInterval(this sbyte self, sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue) => self.OutInterval<sbyte>(min, max);

        public static bool InIntervals(this sbyte self, params (sbyte, sbyte)[] interval) => self.InIntervals<sbyte>(interval);
        public static bool OutIntervals(this sbyte self, params (sbyte, sbyte)[] interval) => self.OutIntervals<sbyte>(interval);
    }
}