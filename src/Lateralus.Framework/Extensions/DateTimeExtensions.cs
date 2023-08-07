namespace Lateralus.Framework;

public static class DateTimeExtensions
{
    [Pure]
    [Obsolete("Use System.Globalization.ISOWeek", DiagnosticId = "MEZ_NETCORE3_1")]
    public static DateTime FirstDateOfWeekIso8601(int year, int weekOfYear, DayOfWeek weekStart = DayOfWeek.Monday)
    {
        var jan1 = new DateTime(year, 1, 1);
        var fourthDay = (DayOfWeek)(((int)weekStart + 3) % 7);
        var daysOffset = fourthDay - jan1.DayOfWeek;

        var firstThursday = jan1.AddDays(daysOffset);
        var cal = CultureInfo.CurrentCulture.Calendar;
        var firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, weekStart);

        var weekNum = weekOfYear;
        if (firstWeek <= 1)
        {
            weekNum--;
        }

        var result = firstThursday.AddDays(weekNum * 7);
        return result.AddDays(-3);
    }

    [Pure]
    public static DateTime StartOfWeek(this DateTime dt) => StartOfWeek(dt, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

    [Pure]
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        var diff = dt.DayOfWeek - startOfWeek;
        if (diff < 0)
        {
            diff += 7;
        }

        return dt.AddDays(-1 * diff);
    }

    [Pure]
    public static DateTime StartOfMonth(this DateTime dt) => StartOfMonth(dt, keepTime: false);

    [Pure]
    public static DateTime StartOfMonth(this DateTime dt, bool keepTime)
    {
        if (keepTime)
        {
            return dt.AddDays(-dt.Day + 1);
        }

        return new DateTime(dt.Year, dt.Month, 1);
    }

    [Pure]
    public static DateTime EndOfMonth(this DateTime dt) => new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));

    [Pure]
    public static DateTime StartOfYear(this DateTime dt) => StartOfYear(dt, keepTime: false);

    [Pure]
    public static DateTime StartOfYear(this DateTime dt, bool keepTime)
    {
        if (keepTime)
        {
            return dt.AddDays(-dt.DayOfYear + 1);
        }
        else
        {
            return new DateTime(dt.Year, 1, 1);
        }
    }

    [Pure]
    public static DateTime TruncateMilliseconds(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Kind);

    public static string ToStringInvariant(this DateTime dt) => dt.ToString(CultureInfo.InvariantCulture);

    public static string ToStringInvariant(this DateTime dt, string format) => dt.ToString(format, CultureInfo.InvariantCulture);

    public static string ToStringInvariant(this DateOnly date) => date.ToString(CultureInfo.InvariantCulture);

    public static string ToStringInvariant(this DateOnly date, string format) => date.ToString(format, CultureInfo.InvariantCulture);

    public static string ToStringInvariant(this TimeOnly time) => time.ToString(CultureInfo.InvariantCulture);

    public static string ToStringInvariant(this TimeOnly time, string format) => time.ToString(format, CultureInfo.InvariantCulture);
    /// <summary>
    /// Sets the time of the current System.DateTime to Midnight, the Date remains the same.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime ToMinTime(this DateTime dt) => dt.Date;

    /// <summary>
    /// Sets the time of the current System.DateTime to 11:59:59.999, the Date remains the same.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime ToMaxTime(this DateTime dt) => dt.Date.AddDays(1).AddTicks(-1);

    /// <summary>
    /// Sets the time of the current System.DateTime to Midnight, the Date remains the same.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime? ToMinTime(this DateTime? dt)
    {
        if (dt.HasValue)
            return dt.Value.Date;
        else
            return dt;

    }

    /// <summary>
    /// Sets the time of the current System.DateTime to 11:59:59.999, the Date remains the same.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime? ToMaxTime(this DateTime? dt)
    {
        if (dt.HasValue)
            return dt.Value.Date.AddDays(1).AddTicks(-1);
        else
            return dt;
    }

    ///<summary>
    ///Returns the first day of the month.
    ///</summary>
    ///<param name="dt"></param>
    ///<returns></returns>
    public static DateTime GetFirstDayOfMonth(this DateTime dt)
    {
        DateTime firstOfMonth = new(dt.Year, dt.Month, 1);
        return firstOfMonth;
    }

    ///<summary>
    ///Returns the last day of the month.
    ///</summary>
    ///<param name="dt"></param>
    ///<returns></returns>
    public static DateTime GetLastDayOfMonth(this DateTime dt)
    {
        DateTime lastOfMonth = new(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        return lastOfMonth;
    }

    ///<summary>
    ///Returns true if the date if the last day of the month.
    ///</summary>
    ///<param name="dt"></param>
    ///<returns></returns>
    public static bool IsLastDayOfTheMonth(this DateTime dateTime) => dateTime == new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);

    ///<summary>
    ///Returns the first day of next month.
    ///</summary>
    ///<param name="dt"></param>
    ///<returns></returns>
    public static DateTime GetFirstDayOfNextMonth(this DateTime dt) => dt.AddMonths(1).GetFirstDayOfMonth();

    ///<summary>
    ///Returns the first day of previous month.
    ///</summary>
    ///<param name="dt"></param>
    ///<returns></returns>
    public static DateTime GetFirstDayOfPrevMonth(this DateTime dt) => dt.AddMonths(-1).GetFirstDayOfMonth();

    ///<summary>
    ///Returns the last day of the next month.
    ///</summary>
    ///<param name="dt"></param>
    ///<returns></returns>
    public static DateTime GetLastDayOfNextMonth(this DateTime dt) => dt.AddMonths(1).GetLastDayOfMonth();

    ///<summary>
    ///Returns the last day of the previous month.
    ///</summary>
    ///<param name="dt"></param>
    ///<returns></returns>
    public static DateTime GetLastDayOfPreviousMonth(this DateTime dt) => dt.AddMonths(-1).GetLastDayOfMonth();

    /// <remarks>Author: Torre Lasley, Date: 09/09/2010</remarks>
    /// <summary>
    /// Indicates whether or not this date is between the given begin and end dates. If the begin date is after the end date this method always returns false. Begin and end dates are inclusive (10/09/1980 falls between 10/01/1980 and 10/09/1980 so this method will return TRUE).
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="beginDate">The starting date.</param>
    /// <param name="endDate">The ending date.</param>
    /// <returns></returns>
    public static bool Between(this DateTime dt, DateTime? beginDate, DateTime? endDate) => dt.Between(beginDate.GetValueOrDefault(DateTime.MinValue), endDate, true);

    /// <summary>
    /// Indicates whether or not this date is between the given begin and end dates. If the begin date is after the end date this method always returns false.
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="beginDate">The starting date.</param>
    /// <param name="endDate">The ending date.</param>
    /// <param name="inclusive">Indicates whether or not begin and end dates should be inclusive (i.e., if inclusive=true, 10/09/1980 falls between 10/01/1980 and 10/09/1980 so this method will return TRUE).</param>
    /// <returns></returns>
    public static bool Between(this DateTime dt, DateTime beginDate, DateTime? endDate, bool inclusive)
    {
        // No End Date means it doesn't end.
        if (!endDate.HasValue)
        {
            endDate = DateTime.MaxValue;
        }

        if (beginDate.CompareTo(endDate.Value) > 0)
        {
            // Begin date is before the End Date.
            return false;
        }

        if (inclusive)
        {
            return (DateTime.Compare(dt, beginDate) >= 0 && DateTime.Compare(dt, endDate.Value) <= 0);
        }
        else
        {
            return (DateTime.Compare(dt, beginDate) > 0 && DateTime.Compare(dt, endDate.Value) < 0);
        }
    }

    /// <summary>
    /// Returns a DateTime with the seconds and milliseconds set to 0 making it minute precision.
    /// </summary>
    /// <param name="dateTime">The DateTime to convert</param>
    /// <returns></returns>
    public static DateTime ToMinutePrecision(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);

    /// <summary>
    /// Returns a DateTime? with the seconds and milliseconds set to 0 making it minute precision.
    /// </summary>
    /// <param name="dateTime">The DateTime to convert</param>
    /// <returns></returns>
    public static DateTime? ToMinutePrecision(this DateTime? dateTime)
    {
        if (dateTime.HasValue)
            return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, dateTime.Value.Hour, dateTime.Value.Minute, 0);
        else
            return null;
    }


    /// <summary>
    /// Now changed to use NodaTime library because of a bug trying to roll our own datetime 
    /// http://nodatime.org/
    /// </summary>
    /// <param name="firstDateTime"></param>
    /// <param name="secondDateTime"></param>
    /// <returns></returns>
    public static int MonthDifference(this DateTime firstDateTime, DateTime secondDateTime)
    {
        LocalDate start = new(firstDateTime.Year, firstDateTime.Month, firstDateTime.Day);
        LocalDate end = new(secondDateTime.Year, secondDateTime.Month, secondDateTime.Day);
        Period period = Period.Between(start, end);

        //Period returns years + months + days + minute etc... so we need to add them for the month total
        long months = (period.Years * 12) + period.Months;
        return (int)months;
    }

    /// <summary>
    /// Return the date that is earlier
    /// </summary>
    /// <param name="firstDateTime"></param>
    /// <param name="secondDateTime"></param>
    /// <returns></returns>
    public static DateTime MinDateTime(this DateTime firstDateTime, DateTime secondDateTime)
    {
        if (firstDateTime == secondDateTime)
            return firstDateTime;

        if (firstDateTime < secondDateTime)
        {
            return firstDateTime;
        }
        else
        {
            return secondDateTime;
        }
    }

    /// <summary>
    /// Return the date that is earlier
    /// </summary>
    /// <param name="firstDateTime"></param>
    /// <param name="secondDateTime"></param>
    /// <returns></returns>
    public static DateTime MaxDateTime(this DateTime firstDateTime, DateTime secondDateTime)
    {
        if (firstDateTime == secondDateTime)
            return firstDateTime;

        if (firstDateTime > secondDateTime)
        {
            return firstDateTime;
        }
        else
        {
            return secondDateTime;
        }
    }

    /// <summary>
    /// Returns the month name for a given date. (June)
    /// </summary>
    /// <param name="dateTime">The DateTime to convert</param>
    /// <returns></returns>
    public static string ToMonthName(this DateTime dateTime) => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);

    /// <summary>
    /// Returns the short month name for a given date. (Jun)
    /// </summary>
    /// <param name="dateTime">The DateTime to convert</param>
    /// <returns></returns>
    public static string ToShortMonthName(this DateTime dateTime) => CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);

    /// <summary>
    /// Returns true if the datetime is 12-30-1899 (Delphi "Null" date).
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static bool IsDelphiNullDate(this DateTime dateTime)
    {
        var dateTime1899 = new DateTime(1899, 12, 30);
        return (dateTime == dateTime1899);
    }

    /// <summary>
    /// Returns null if the datetime is 12-30-1899 (Delphi "Null" date).
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime? NullIf1899(this DateTime dateTime)
    {
        if (dateTime.IsDelphiNullDate() == false)
        {
            return dateTime;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Returns null if the datetime is 12-30-1899 (Delphi "Null" date).
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime? NullIf1899(this DateTime? dateTime)
    {
        if (dateTime.HasValue && dateTime.Value.IsDelphiNullDate() == false)
        {
            return dateTime;
        }
        else
        {
            return null;
        }
    }

    public static DateTime StartOfDay(this DateTime dateTime) => new DateTime(
           dateTime.Year,
           dateTime.Month,
           dateTime.Day,
           0, 0, 0, 0);

    public static DateTime EndOfDay(this DateTime dateTime) => new DateTime(
           dateTime.Year,
           dateTime.Month,
           dateTime.Day,
           23, 59, 59, 999);
}
