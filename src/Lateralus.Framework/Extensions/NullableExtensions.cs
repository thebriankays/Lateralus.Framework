namespace Lateralus.Framework;
public  static class NullableExtensions
{
    public static bool IsNull(this object obj) => obj == null;

    public static bool IsNotNull(this object obj) => obj != null;

    public static bool HasValueEqualTo<TOriginal, TValue>(this TOriginal? original, TValue equalTo)
    where TOriginal : struct where TValue : IComparable => original.HasValue && original.Value.Equals(equalTo);

}

