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
    }
}