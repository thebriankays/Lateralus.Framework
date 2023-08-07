namespace Lateralus.Framework;

public static class TimeSpanExtensions
{
    public static TimeSpan? ToMinutePrecision(this TimeSpan? timeSpan) => timeSpan.HasValue
            ? new TimeSpan(timeSpan.Value.Days, timeSpan.Value.Hours, timeSpan.Value.Minutes, 0, 0)
            : (TimeSpan?)null;

    public static TimeSpan ToMinutePrecision(this TimeSpan timeSpan) => ((TimeSpan?)timeSpan).ToMinutePrecision()
            .Value;
}
