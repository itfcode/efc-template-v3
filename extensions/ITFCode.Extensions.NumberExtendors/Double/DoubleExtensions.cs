using System.Collections.Generic;

namespace ITFCode.Extensions.NumberExtendors
{
    public static class DoubleExtensions
    {
        public static bool InRange(this double self, IEnumerable<double> values) => self.InRange<double>(values);
        public static bool InRange(this double self, params double[] values) => self.InRange<double>(values);

        public static bool OutRange(this double self, IEnumerable<double> values) => self.OutRange<double>(values);
        public static bool OutRange(this double self, params double[] values) => self.OutRange<double>(values);

        public static bool InRanges(this double self, IEnumerable<IEnumerable<double>> values) => self.InRanges<double>(values);
        public static bool OutRanges(this double self, IEnumerable<IEnumerable<double>> values) => self.OutRanges<double>(values);

        public static bool InInterval(this double self, double min = double.MinValue, double max = double.MaxValue) => self.InInterval<double>(min, max);
        public static bool OutInterval(this double self, double min = double.MinValue, double max = double.MaxValue) => self.OutInterval<double>(min, max);

        public static bool InIntervals(this double self, params (double, double)[] interval) => self.InIntervals<double>(interval);
        public static bool OutIntervals(this double self, params (double, double)[] interval) => self.OutIntervals<double>(interval);
    }
}
