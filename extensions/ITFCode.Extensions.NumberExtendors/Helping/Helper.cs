using System.Collections.Generic;

namespace ITFCode.Extensions.NumberExtendors
{
    internal static class Helper
    {
        internal static T? NullChecking<T>(T? value, bool throwIfNull) where T : struct
        {
            if (!value.HasValue && throwIfNull)
                throw new System.ArgumentNullException();

            return value;
        }

        internal static T? NullChecking<T>(T? value, bool throwIfNull, IEnumerable<T> values) where T : struct
        {
            if (!value.HasValue && throwIfNull)
                throw new System.ArgumentNullException();

            if (values == null)
                throw new System.NullReferenceException();

            return value;
        }

        internal static T? NullChecking<T>(this T? value, bool throwIfNull, IEnumerable<IEnumerable<T>> values)  where T : struct
        {
            if (!value.HasValue && throwIfNull)
                throw new System.ArgumentNullException();

            if (values == null)
                throw new System.NullReferenceException();

            return value;
        }
    }
}