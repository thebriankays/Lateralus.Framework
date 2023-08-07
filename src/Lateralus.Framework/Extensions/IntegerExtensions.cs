namespace Lateralus.Framework;

public static class IntegerExtensions
{
    /// <summary>
    /// Returns a string of a specified length in which the beginning of the current integer is padded with zeroes or with a specified character
    /// keeping the negative sign (if it exists) before the padded characters.
    /// NOTE: Unlike String.PadLeft the default is 0's not spaces. 
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static string ToPaddedStringWithSign(this int i, int length) => i.ToPaddedStringWithSign(length, '0');

    public static string ToPaddedStringWithSign(this int i, int length, char paddingChar) => i < 0 ? "-" + Math.Abs(i).ToString().PadLeft(length - 1, paddingChar) : i.ToString().PadLeft(length, paddingChar);

    public static bool Between(this int num, int lower, int upper) => lower <= num && num <= upper;
}
