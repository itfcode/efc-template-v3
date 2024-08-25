namespace ITFCode.Extensions.StringExtendors
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string self)
            => self switch
            {
                null => throw new ArgumentNullException(nameof(self)),
                "" => throw new ArgumentException($"Param '{nameof(self)}' Cannot Be Empty", nameof(self)),
                _ => string.Concat(self[0].ToString().ToUpper(), self.AsSpan(1))
            };
    }
}