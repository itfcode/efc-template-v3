namespace ITFCode.Core.Extensions.ComparableExtendors
{
    public static class ComparableExtensions
    {
        #region Extensions Methods

        public static bool Within<T>(this T self, T start, T finish) where T : struct, IComparable, IComparable<T>
            => Predicate(self).Invoke((start, finish));

        public static bool WithinAny<T>(this T self, params (T, T)[] values) where T : struct, IComparable, IComparable<T>
            => values.Any(Predicate(self));

        public static bool WithinAll<T>(this T self, params (T, T)[] values) where T : struct, IComparable, IComparable<T>
            => values.All(Predicate(self));

        #endregion

        #region Private Methods 

        private static Func<(T, T), bool> Predicate<T>(T value) where T : IComparable, IComparable<T>
            => (x) => value.CompareTo(x.Item1) >= 0 && value.CompareTo(x.Item2) <= 0;

        #endregion
    }
}