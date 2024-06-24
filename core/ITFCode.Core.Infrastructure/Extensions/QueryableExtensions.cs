using System.Linq.Expressions;

namespace ITFCode.Core.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> MultiFilterByKey<TEntity, TKey>(this IQueryable<TEntity> self, IEnumerable<TKey> keys, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentOutOfRangeException("Primary Key Property Name Not Defined");

            var parameter = Expression.Parameter(typeof(TEntity));

            var body = keys.Select(key => Expression.Equal(
                    left: Expression.Property(parameter, propertyName),
                    right: Expression.Constant(key)))
                .Aggregate(Expression.OrElse);

            var predicate = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

            return self.Where(predicate);
        }

        public static IQueryable<TEntity> MultiFilterByKey<TEntity, TKey1, TKey2>(this IQueryable<TEntity> self,
            IEnumerable<(TKey1, TKey2)> keys, string[] propertyNames)
        {
            if (propertyNames.Length != 2)
                throw new ArgumentOutOfRangeException("Number Of Primary Key Property Names Should Be Equals To 2");

            var parameter = Expression.Parameter(typeof(TEntity));

            var body = keys.Select(b => Expression.AndAlso(
                    left: Expression.Equal(
                        left: Expression.Property(parameter, propertyNames[0]),
                        right: Expression.Constant(b.Item1)),
                    right: Expression.Equal(
                        left: Expression.Property(parameter, propertyNames[1]),
                        right: Expression.Constant(b.Item2))))
                .Aggregate(Expression.OrElse);

            var predicate = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

            return self.Where(predicate);
        }

        public static IQueryable<TEntity> MultiFilterByKey<TEntity, TKey1, TKey2, TKey3>(this IQueryable<TEntity> self,
            IEnumerable<(TKey1, TKey2, TKey3)> keys, string[] propertyNames)
        {
            if (propertyNames.Length != 3)
                throw new ArgumentOutOfRangeException("Number Of Primary Key Property Names Should Be Equals To 3");

            var parameter = Expression.Parameter(typeof(TEntity));

            var body = keys.Select(b => Expression.AndAlso(
                    left: Expression.AndAlso(
                        left: Expression.Equal(
                            left: Expression.Property(parameter, propertyNames[0]),
                            right: Expression.Constant(b.Item1)),
                        right: Expression.Equal(
                            left: Expression.Property(parameter, propertyNames[1]),
                            right: Expression.Constant(b.Item2))),
                    right:Expression.Equal(
                        left: Expression.Property(parameter, propertyNames[2]),
                        right: Expression.Constant(b.Item3))))
                .Aggregate(Expression.OrElse);

            var predicate = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

            return self.Where(predicate);
        }
    }
}