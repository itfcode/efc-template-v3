namespace ITFCode.Extensions.EquitableExtendors
{
    public static class EquatableExtensions
    {
        public static bool InRange<T>(this T self, IEnumerable<T> values) where T : IEquatable<T>
        {
            ArgumentNullException.ThrowIfNull(self, nameof(self));
            ArgumentNullException.ThrowIfNull(values, nameof(values));

            if (!values.Any())
                throw new ArgumentException("Value collection cannot be empty");

            return values.Any(x => x.Equals(self));
        }

        public static bool InRange<T>(this T self, params T[] values) where T : IEquatable<T>
        {
            ArgumentNullException.ThrowIfNull(self, nameof(self));

            return values.Any(x => x.Equals(self));
        }

        public static bool InRangeAny<T>(this T self, IEnumerable<IEnumerable<T>> values) where T : IEquatable<T>
        {
            ArgumentNullException.ThrowIfNull(self, nameof(self));
            ArgumentNullException.ThrowIfNull(values, nameof(values));

            if (!values.Any())
                throw new ArgumentException("Value collection cannot be empty");

            return values.Any(x => self.InRange(x));
        }

        public static bool InRangeAll<T>(this T self, IEnumerable<IEnumerable<T>> values) where T : IEquatable<T>
        {
            ArgumentNullException.ThrowIfNull(self, nameof(self));
            ArgumentNullException.ThrowIfNull(values, nameof(values));

            if (!values.Any())
                throw new ArgumentException("Value collection cannot be empty");

            return values.All(x => self.InRange(x));
        }
    }
}