using System.Collections.Generic;

namespace ITFCode.Extensions.NumberExtendors
{
    public static class ByteExtensions
    {
        public static bool InRange(this byte self, IEnumerable<byte> values) => self.InRange<byte>(values);
        public static bool InRange(this byte self, params byte[] values) => self.InRange<byte>(values);

        public static bool OutRange(this byte self, IEnumerable<byte> values) => self.OutRange<byte>(values);
        public static bool OutRange(this byte self, params byte[] values) => self.OutRange<byte>(values);

        public static bool InRanges(this byte self, IEnumerable<IEnumerable<byte>> values) => self.InRanges<byte>(values);
        public static bool OutRanges(this byte self, IEnumerable<IEnumerable<byte>> values) => self.OutRanges<byte>(values);

        public static bool InInterval(this byte self, byte min = byte.MinValue, byte max = byte.MaxValue) => self.InInterval<byte>(min, max);
        public static bool OutInterval(this byte self, byte min = byte.MinValue, byte max = byte.MaxValue) => self.OutInterval<byte>(min, max);

        public static bool InIntervals(this byte self, params (byte, byte)[] interval) => self.InIntervals<byte>(interval);
        public static bool OutIntervals(this byte self, params (byte, byte)[] interval) => self.OutIntervals<byte>(interval);
    }
}