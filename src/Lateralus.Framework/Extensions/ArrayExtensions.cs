namespace Lateralus.Framework;

public static class ArrayExtensions
{
    public static bool IsOneOf<TOriginal, TArray>(this TOriginal original, IEnumerable<TArray> items)
            where TOriginal : IComparable where TArray : IComparable => items.Any(item => original.Equals(item));

    public static bool IsOneOfCaseInsensitive<TOriginal, TArray>(this TOriginal original, IEnumerable<TArray> items)
        where TOriginal : IComparable where TArray : IComparable => items.Any(item => original.ToString().ToLower().Equals(item.ToString().ToLower()));
}

