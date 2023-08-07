namespace Lateralus.Framework;

public static class LookupExtensions
{
    public static T FirstOrDefault<T>(this Lookup<int, T> codeLookup, int? key)
    {
        if (key == null)
            return default(T);
        return codeLookup[(int)key].FirstOrDefault();
    }
}
