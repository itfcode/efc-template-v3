using System.Collections.Generic;

namespace ITFCode.Extensions.NumberExtendors
{
    public static class ShortExtensions
    {
        public static bool InRange(this short self, IEnumerable<short> values) => self.InRange<short>(values);
        public static bool InRange(this short self, params short[] values) => self.InRange<short>(values);

        public static bool OutRange(this short self, IEnumerable<short> values) => self.OutRange<short>(values);
        public static bool OutRange(this short self, params short[] values) => self.OutRange<short>(values);

        public static bool InRanges(this short self, IEnumerable<IEnumerable<short>> values) => self.InRanges<short>(values);
        public static bool OutRanges(this short self, IEnumerable<IEnumerable<short>> values) => self.OutRanges<short>(values);

        public static bool InInterval(this short self, short min = short.MinValue, short max = short.MaxValue) => self.InInterval<short>(min, max);
        public static bool OutInterval(this short self, short min = short.MinValue, short max = short.MaxValue) => self.OutInterval<short>(min, max);

        public static bool InIntervals(this short self, params (short, short)[] interval) => self.InIntervals<short>(interval);
        public static bool OutIntervals(this short self, params (short, short)[] interval) => self.OutIntervals<short>(interval);
    }
}