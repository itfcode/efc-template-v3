using System.Collections.Immutable;

namespace ITFCode.Core.InfrastructureV3.Helper
{
    internal static class CollectionHelper
    {
        public static ICollection<object[]> ToArraysOfObjects(IEnumerable<object> values)
        {
            return values.Select(x => new object[] { x }).ToList();
        }

        public static ICollection<object[]> ToArraysOfObjects(IEnumerable<(object, object)> values)
        {
            return values.Select(x => new object[] { x.Item1, x.Item2 }).ToList();
        }

        public static ICollection<object[]> ToArraysOfObjects(IEnumerable<(object, object, object)> values)
        {
            return values.Select(x => new object[] { x.Item1, x.Item2, x.Item3 }).ToList();
        }

        public static ICollection<object[]> ToArraysOfObjects<T>(IEnumerable<T> values) where T : IEquatable<T>
        {
            return values.Select(x => new object[] { x }).ToList();
        }

        public static ICollection<(object, object)> ToArraysOfObjects<T1, T2>(IEnumerable<(T1, T2)> values)
            where T1 : IEquatable<T1>
            where T2 : IEquatable<T2>
        {
            return values.Select(x => ((object)x.Item1, (object)x.Item2)).ToList();
        }

        public static ICollection<(object, object, object)> ToArraysOfObjects<T1, T2, T3>(IEnumerable<(T1, T2, T3)> values)
            where T1 : IEquatable<T1>
            where T2 : IEquatable<T2>
            where T3 : IEquatable<T3>
        {
            return values.Select(x => ((object)x.Item1, (object)x.Item2, (object)x.Item3)).ToList();
        }
    }
}