namespace Lateralus.Framework;

public static class DecimalExtensions
{
    /// <summary>
    /// Returns an integer rounded using MidpointRounding.AwayFromZero (if .5 or greater, round up, else round down).
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static int Rounded(this Decimal d) => d.Rounded(MidpointRounding.AwayFromZero);

    /// <summary>
    /// Returns an integer rounded using the specified MidpointRounding mode.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static int Rounded(this Decimal d, MidpointRounding mode) => (int)Decimal.Round(d, mode);

    /// <summary>
    /// Returns an integer rounded to the nearest $50.00 amount
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static int RoundToNearestFiftyDollars(this Decimal d) => (int)Math.Round((Double)d / 50.0) * 50;

    /// <summary>
    /// Returns a string formatted to display money.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToCurrencyString(this Decimal value) => value.ToCurrencyString(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns a string formatted to display money.
    /// You can pass a culture if desired.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public static string ToCurrencyString(this Decimal value, CultureInfo culture) => value.ToString("c", culture);

    /// <summary>
    /// Returns a string formatted to display as a percentage.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToPercentageString(this Decimal value) => value.ToString("#0.0%");
}
