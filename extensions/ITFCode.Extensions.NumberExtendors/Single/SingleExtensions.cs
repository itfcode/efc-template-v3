using System.Collections.Generic;

namespace ITFCode.Extensions.NumberExtendors
{
    public static class SingleExtensions
    {
        public static bool InRange(this float self, IEnumerable<float> values) => self.InRange<float>(values);
        public static bool InRange(this float self, params float[] values) => self.InRange<float>(values);

        public static bool OutRange(this float self, IEnumerable<float> values) => self.OutRange<float>(values);
        public static bool OutRange(this float self, params float[] values) => self.OutRange<float>(values);

        public static bool InRanges(this float self, IEnumerable<IEnumerable<float>> values) => self.InRanges<float>(values);
        public static bool OutRanges(this float self, IEnumerable<IEnumerable<float>> values) => self.OutRanges<float>(values);

        public static bool InInterval(this float self, float min = float.MinValue, float max = float.MaxValue) => self.InInterval<float>(min, max);
        public static bool OutInterval(this float self, float min = float.MinValue, float max = float.MaxValue) => self.OutInterval<float>(min, max);

        public static bool InIntervals(this float self, params (float, float)[] interval) => self.InIntervals<float>(interval);
        public static bool OutIntervals(this float self, params (float, float)[] interval) => self.OutIntervals<float>(interval);
    }
}