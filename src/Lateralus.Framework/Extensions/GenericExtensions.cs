namespace Lateralus.Framework;

public static class GenericExtensions
{
    /// <summary>
    /// Returns TRUE if the item is in the specified list. Example: actionCode.In(1, 2, 3)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="me"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool In<T>(this T me, IEnumerable<T> list) => list.Contains(me);

    /// <summary>
    /// Returns TRUE if the item is NOT in the specified list. Example: actionCode.In(1, 2, 3)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="me"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool NotIn<T>(this T me, IEnumerable<T> list) => !list.Contains(me);

    /// <summary>
    /// Returns TRUE if the item is in the specified list. Example: actionCode.In(1, 2, 3)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="me"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool In<T>(this T me, params T[] list) => list.Contains(me);

    /// <summary>
    /// Returns TRUE if the item is NOT in the specified list. Example: actionCode.In(1, 2, 3)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="me"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool NotIn<T>(this T me, params T[] list) => !list.Contains(me);

    /// <summary>
    /// Get a value as the given Type of T from an object, but
    /// replaces DBNull or Null with the default value of T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <remarks>
    /// James Brooks
    /// 4-16-12
    /// Created
    /// </remarks>
    public static T To<T>(this object source) => To<T>(source, default(T));

    /// <summary>
    /// Get a value as the given Type of T from an object, but
    /// replaces DBNull or Null values with the given defaultValue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">object to convert</param>
    /// <param name="defaultValue">default to return when object is DBNull or null</param>
    /// <returns>converted object</returns>
    public static T To<T>(this object source, T defaultValue)
    {
        T result = default(T);

        try
        {
            // Make sure it isn't null, if so just return the defaultValue.
            if (source == null || source == DBNull.Value) return defaultValue;

            // Get the underlying target type.
            Type conversionType = typeof(T);

            // Special case for strings.
            if (source != null && source.GetType() == typeof(string) && String.IsNullOrWhiteSpace(source as string))
            {
                result = default(T);
            }
            else
            {
                //Check for nullable types.
                if (IsNullable(conversionType))
                {
                    // Make sure it isn't null, if so just return the defaultValue.
                    if (source == null) return defaultValue;

                    conversionType = UnderlyingTypeOf(conversionType);
                }

                // We have had issues with field values having .00 when trying to parse an integer from a string.
                if (conversionType == typeof(short) || conversionType == typeof(int) || conversionType == typeof(long))
                {
                    if (source is string && source != null)
                    {
                        source = source.ToString().TrimEnd(".00");
                        source = source.ToString().TrimEnd(".0");
                        source = source.ToString().TrimEnd(".");
                    }
                }

                // Convert.ChangeType() won't work converting a string to enum, but Enum.Parse will.
                if (conversionType.BaseType != null && conversionType.BaseType.Equals(typeof(Enum)))
                {
                    result = (T)Enum.Parse(conversionType, source.ToString());
                }
                else
                {
                    if (conversionType.Equals(typeof(DateTime)))
                    {
                        if (source.ToString().Length == 20 && !source.ToString().Contains("/"))
                        {
                            // Dates are in this format: 20130621T16:01:07000
                            source = DateTime.ParseExact(source.ToString(), "yyyyMMdd'T'HH:mm:ssfff", CultureInfo.InvariantCulture).To<T>();
                        }
                        else
                        {
                            if (!(source is DateTime))
                            {
                                source = (object)source.ToString().Replace("\\", "/");
                            }
                        }
                    }

                    if (CanChangeType(source, conversionType))
                        result = (T)Convert.ChangeType(source, conversionType);
                    else
                        result = (T)source;

                }
            }
        }
        catch (FormatException)
        {
            // Nom nom...if we failed to convert, we'll just return the default value of result.
        }

        return result;
    }

    /// <summary>
    /// Tests to see if the specified object can be changed to the specified type
    /// </summary>
    /// <date>2/3/2017</date>
    /// <param name="value">The object whose type needs to be changed</param>
    /// <param name="conversionType">The type to change the object to</param>
    /// <returns></returns>
    public static bool CanChangeType(object value, Type conversionType)
    {
        if (conversionType == null || value == null || (value as IConvertible) == null)
            return false;

        return true;
    }

    private static bool IsNullable(Type t)
    {
        if (!t.IsGenericType) return false;
        Type g = t.GetGenericTypeDefinition();
        return (g.Equals(typeof(Nullable<>)));
    }

    private static Type UnderlyingTypeOf(Type t) => t.GetGenericArguments()[0];

    /// <summary>
    /// Check a value to determine if it is a number.
    /// </summary>
    /// <param name="expression">Object to check</param>
    /// <returns>True if the value is a number, false otherwise.</returns>
    public static bool IsNumeric(this object expression)
    {
        if (expression == null || expression is DateTime)
            return false;

        if (expression is short || expression is int || expression is long || expression is Decimal || expression is Single || expression is Double || expression is Boolean)
            return true;

        try
        {
            // NOTE: I choose Double over the old method of long because Double is the longest numeric type
            Double parsed;
            return Double.TryParse(expression.ToString(), out parsed);
        }
        catch
        {
            // Ignore formatting errors
        }

        return false;
    }

    /// <summary>
    /// Add an item to an array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original"></param>
    /// <param name="itemToAdd"></param>
    /// <returns></returns>
    public static T[] AddItem<T>(this T[] original, T itemToAdd)
    {
        T[] finalArray = new T[original == null ? 1 : original.Length + 1];

        for (int i = 0; i < (original == null ? 0 : original.Length); i++)
        {
            finalArray[i] = original[i];
        }

        finalArray[finalArray.Length - 1] = itemToAdd;

        return finalArray;
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
      => enumerable == null || !enumerable.Any(predicate);

    public static bool IsNullOrEmpty(this IEnumerable<int?> enumerable)
      => enumerable == null || !enumerable.Any() || !enumerable.Any(x => x.HasValue);

    public static T[] RemoveAt<T>(this T[] source, int index)
    {
        if (source != null && source.Length > 0 && index >= 0 && index < source.Length)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        return new T[0];
    }

    /// <summary>
    /// Pollyfill for <a href="https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset">Enumerable.ToHashSet()</a>
    /// that was added in .NET 4.7.2 and 4.8.0
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
    {
        var result = new HashSet<T>();
        result.UnionWith(source);
        return result;
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable?.Any() != true;
    }
}

