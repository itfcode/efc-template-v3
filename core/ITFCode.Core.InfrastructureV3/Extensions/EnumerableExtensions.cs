namespace ITFCode.Core.InfrastructureV3.Extensions
{
    internal static class EnumerableExtensions
    {
        public static List<object[]> ToObjectArrayList(this IEnumerable<object> self)
        {
            ArgumentNullException.ThrowIfNull(self, nameof(self));
            return self.Select(x => new object[] { x }).ToList();
        }

        public static List<object[]> ToObjectArrayList(this IEnumerable<(object, object)> self)
        {
            ArgumentNullException.ThrowIfNull(self, nameof(self));
            return self.Select(x => new object[] { x.Item1, x.Item2 }).ToList();
        }

        public static List<object[]> ToObjectArrayList(this IEnumerable<(object, object, object)> self)
        {
            ArgumentNullException.ThrowIfNull(self, nameof(self));
            return self.Select(x => new object[] { x.Item1, x.Item2, x.Item3 }).ToList();
        }

        public static IEnumerable<(object, object)> ToTuplesOfObjects<T1, T2>(this IEnumerable<(T1, T2)> self)
            where T1 : IEquatable<T1>
            where T2 : IEquatable<T2>
        {
            return self.Select(x => ((object)x.Item1, (object)x.Item2)).ToArray();
        }

        public static IEnumerable<(object, object, object)> ToTuplesOfObjects<T1, T2, T3>(this IEnumerable<(T1, T2, T3)> self)
            where T1 : IEquatable<T1>
            where T2 : IEquatable<T2>
            where T3 : IEquatable<T3>
        {
            return self.Select(x => ((object)x.Item1, (object)x.Item2, (object)x.Item3)).ToArray();
        }
    }
}