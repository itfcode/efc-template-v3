using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV2.Helper
{
    public static class ExpressionHelper
    {
        #region One Key

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TValue>(TValue key, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentOutOfRangeException("Primary Key Property Name Not Defined");

            var parameter = Expression.Parameter(typeof(TSource));
            var body = Expression.Equal(
                    left: Expression.Property(parameter, propertyName),
                    right: Expression.Constant(key));

            return Expression.Lambda<Func<TSource, bool>>(body, parameter);
        }

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TValue1, TValue2>((TValue1, TValue2) key, string[] propertyNames)
        {
            if (propertyNames.Length != 2)
                throw new ArgumentOutOfRangeException("Number Of Property Names Should Be Equals To 2");

            var parameter = Expression.Parameter(typeof(TSource));

            var body = Expression.AndAlso(
                    left: Expression.Equal(
                        left: Expression.Property(parameter, propertyNames[0]),
                        right: Expression.Constant(key.Item1)),
                    right: Expression.Equal(
                        left: Expression.Property(parameter, propertyNames[1]),
                        right: Expression.Constant(key.Item2)));

            return Expression.Lambda<Func<TSource, bool>>(body, parameter);
        }

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TValue1, TValue2, TValue3>((TValue1, TValue2, TValue3) key, string[] propertyNames)
        {
            if (propertyNames.Length != 3)
                throw new ArgumentOutOfRangeException("Number Of Property Names Should Be Equals To 3");

            var parameter = Expression.Parameter(typeof(TSource));

            var body = Expression.AndAlso(
                    left: Expression.AndAlso(
                        left: Expression.Equal(
                            left: Expression.Property(parameter, propertyNames[0]),
                            right: Expression.Constant(key.Item1)),
                        right: Expression.Equal(
                            left: Expression.Property(parameter, propertyNames[1]),
                            right: Expression.Constant(key.Item2))),
                    right: Expression.Equal(
                        left: Expression.Property(parameter, propertyNames[2]),
                        right: Expression.Constant(key.Item3)));

            return Expression.Lambda<Func<TSource, bool>>(body, parameter);
        }

        #endregion

        #region Many Keys 

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TValue>(IEnumerable<TValue> values, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentOutOfRangeException("Primary Key Property Name Not Defined");

            var parameter = Expression.Parameter(typeof(TSource));
            var body = values.Select(key => Expression.Equal(
                    left: Expression.Property(parameter, propertyName),
                    right: Expression.Constant(key)))
                .Aggregate(Expression.OrElse);

            return Expression.Lambda<Func<TSource, bool>>(body, parameter);
        }

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TValue1, TValue2>(IEnumerable<(TValue1, TValue2)> keys, string[] propertyNames)
        {
            if (propertyNames.Length != 2)
                throw new ArgumentOutOfRangeException("Number Of Property Names Should Be Equals To 2");

            var parameter = Expression.Parameter(typeof(TSource));

            var body = keys.Select(b => Expression.AndAlso(
                    left: Expression.Equal(
                        left: Expression.Property(parameter, propertyNames[0]),
                        right: Expression.Constant(b.Item1)),
                    right: Expression.Equal(
                        left: Expression.Property(parameter, propertyNames[1]),
                        right: Expression.Constant(b.Item2))))
                .Aggregate(Expression.OrElse);

            return Expression.Lambda<Func<TSource, bool>>(body, parameter);
        }

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TValue1, TValue2, TValue3>(IEnumerable<(TValue1, TValue2, TValue3)> keys, string[] propertyNames)
        {
            if (propertyNames.Length != 3)
                throw new ArgumentOutOfRangeException("Number Of Property Names Should Be Equals To 3");

            var parameter = Expression.Parameter(typeof(TSource));

            var body = keys.Select(b => Expression.AndAlso(
                    left: Expression.AndAlso(
                        left: Expression.Equal(
                            left: Expression.Property(parameter, propertyNames[0]),
                            right: Expression.Constant(b.Item1)),
                        right: Expression.Equal(
                            left: Expression.Property(parameter, propertyNames[1]),
                            right: Expression.Constant(b.Item2))),
                    right: Expression.Equal(
                        left: Expression.Property(parameter, propertyNames[2]),
                        right: Expression.Constant(b.Item3))))
                .Aggregate(Expression.OrElse);

            return Expression.Lambda<Func<TSource, bool>>(body, parameter);
        }

        #endregion
    }
}