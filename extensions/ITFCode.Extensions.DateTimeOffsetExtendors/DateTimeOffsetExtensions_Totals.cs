namespace ITFCode.Extensions.DateTimeOffsetExtendors
{
    public static partial class DateTimeOffsetExtensions
    {
        #region P

        #endregion
        public static int TotalMinutes(this DateTimeOffset self) => self.Minute + self.Hour * 60;

        public static int TotalSeconds(this DateTimeOffset self) => self.TotalMinutes() * 60 + self.Second;

        public static int TotalMilliseconds(this DateTimeOffset self) => self.TotalSeconds() * 1000 + self.Millisecond;
    }
}