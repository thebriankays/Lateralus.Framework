namespace Lateralus.Framework;

public static class DictionaryExtensions
{
    /// <summary>
    /// Similar to Dictionary.TryGetValue except that the value is returned if the given key
    /// was found, otherwise null is returned.
    /// </summary>
    /// <param name="key">The key whose value is needed</param>
    /// <returns></returns>
    public static object? TryGetValueOrNull(this IDictionary<object, object> dictionary, object key)
    {
        object? value;

        if (dictionary.TryGetValue(key, out value)) return value;
        else return null;
    }

    /// <summary>
    /// Gets the value associated with the specified TKey, if <paramref name="key"/> is not null. Otherwise, returns the default value of TValue.
    /// </summary>
    /// <typeparam name="T1">The element type of keyvaluepair key</typeparam>
    /// <typeparam name="T2">The element type of keyvaluepair value</typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T2 TryGetNullableValue<T1, T2>(this IDictionary<T1, T2> dictionary, T1 key)
    {
        return key != null && dictionary.ContainsKey(key)
          ? dictionary[key]
          : default(T2);
    }
}

