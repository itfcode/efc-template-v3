using System;
using System.Collections.Generic;
using System.Linq;

namespace ITFCode.Extensions.NumberExtendors
{
    internal static class NumericGenericExtensions
    {
        public static bool InRange<T>(this T self, IEnumerable<T> values) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => values.Any(x => x.Equals(self));

        public static bool InRange<T>(this T self, params T[] values) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => values.Any(x => x.Equals(self));

        public static bool InRanges<T>(this T self, IEnumerable<IEnumerable<T>> values) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => values.Any(x => self.InRange(x));

        public static bool OutRange<T>(this T self, IEnumerable<T> values) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => !self.InRange(values);

        public static bool OutRange<T>(this T self, params T[] values) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => !self.InRange(values);

        public static bool OutRanges<T>(this T self, IEnumerable<IEnumerable<T>> values) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => !self.InRanges(values);

        public static bool InInterval<T>(this T self, T min, T max) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => self.CompareTo(min) >= 0 && self.CompareTo(max) <= 0;

        public static bool OutInterval<T>(this T self, T min, T max) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => !self.InInterval(min, max);

        public static bool InIntervals<T>(this T self, params (T, T)[] interval) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => interval.Any(r => self.InInterval(r.Item1, r.Item2));

        public static bool OutIntervals<T>(this T self, params (T, T)[] interval) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => !self.InIntervals(interval);
    }
}