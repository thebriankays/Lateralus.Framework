namespace Lateralus.Framework;

public static class BooleanExtensions
{
    /// <summary>
    /// Returns a System.String that represents the current System.Object.
    /// </summary>
    /// <param name="trueValue">The string to return if the value is True.</param>
    /// <param name="falseString">The string to return if the value is False.</param>
    /// <param name="nullString">The string to return if the value is Null.</param>
    /// <returns></returns>
    public static String ToString(this Boolean? value, string trueString, string falseString, string nullString)
    {
        if (value.HasValue)
        {
            if (value == true)
            {
                return trueString;
            }
            else
            {
                return falseString;
            }
        }
        else
        {
            return nullString;
        }
    }

    /// <summary>
    /// Returns a System.String that represents the current System.Object.
    /// </summary>
    /// <param name="trueValue">The string to return if the value is True.</param>
    /// <param name="falseString">The string to return if the value is False.</param>
    /// <returns></returns>
    public static String ToString(this Boolean value, string trueString, string falseString)
        => value ? trueString : falseString;

    /// <summary>
    /// Returns the literal string "Yes" or "No" accordingly.
    /// </summary>
    /// <returns></returns>
    public static String YesOrNo(this Boolean value) => value.ToString("Yes", "No");

    /// <summary>
    /// Returns the literal string "Yes", "No", or "" accordingly.
    /// </summary>
    /// <returns></returns>
    public static String YesOrNo(this Boolean? value) => value.ToString("Yes", "No", "");

    /// <summary>
    /// Returns the literal string "Y" or "N" accordingly.
    /// </summary>
    /// <returns></returns>
    public static String YorN(this Boolean value) => value.ToString("Y", "N");

    /// <summary>
    /// Returns the literal string "Y", "N", or "" accordingly.
    /// </summary>
    /// <returns></returns>
    public static String YorN(this Boolean? value) => value.ToString("Y", "N", "");

    /// <summary>
    /// Returns the literal string "True" or "False" accordingly (equivalent to the output of .ToString()).
    /// </summary>
    /// <returns></returns>
    public static String TrueOrFalse(this Boolean value) => value.ToString("True", "False");

    /// <summary>
    /// Returns the literal string "True", "False", or "" accordingly.
    /// </summary>
    /// <returns></returns>
    public static String TrueOrFalse(this Boolean? value) => value.ToString("True", "False", "");

    /// <summary>
    /// Returns the literal string "T" or "F" accordingly.
    /// </summary>
    /// <returns></returns>
    public static String TorF(this Boolean value) => value.ToString("T", "F");

    /// <summary>
    /// Returns the literal string "T", "F", or "" accordingly.
    /// </summary>
    /// <returns></returns>
    public static String TorF(this Boolean? value) => value.ToString("T", "F", "");

    /// <summary>
    /// Convert boolean into a bit type for database calls when using SQL string command
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ToBit(this Boolean value)
    {
        if (value == true)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

}

