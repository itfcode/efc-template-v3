using System.Collections.Generic;
using System.Linq;

namespace ITFCode.Extensions.NumberExtendors
{
    public static class Int32Extensions
    {
        public static bool InRange(this int self, IEnumerable<int> values) => self.InRange<int>(values);
        public static bool InRange(this int self, params int[] values) => self.InRange<int>(values);

        public static bool OutRange(this int self, IEnumerable<int> values) => self.OutRange<int>(values);
        public static bool OutRange(this int self, params int[] values) => self.OutRange<int>(values);

        public static bool InRanges(this int self, IEnumerable<IEnumerable<int>> values) => self.InRanges<int>(values);
        public static bool OutRanges(this int self, IEnumerable<IEnumerable<int>> values) => self.OutRanges<int>(values);

        public static bool InInterval(this int self, int min = int.MinValue, int max = int.MaxValue) => self.InInterval<int>(min, max);
        public static bool OutInterval(this int self, int min = int.MinValue, int max = int.MaxValue) => self.OutInterval<int>(min, max);

        public static bool InIntervals(this int self, params (int, int)[] interval) => self.InIntervals<int>(interval);
        public static bool OutIntervals(this int self, params (int, int)[] interval) => self.OutIntervals<int>(interval);
    }
}
