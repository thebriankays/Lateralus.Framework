namespace Lateralus.Framework;

public static class IEnumberableExtensions
{
    public static BindingList<T> ToBindingList<T>(this IEnumerable<T> list) => new BindingList<T>(list.ToList());

    public static IEnumerable<T> ConcatMany<T>(this IEnumerable<T> source, params IEnumerable<T>[] many)
    {
        return source.Concat(many.SelectMany(x => x));
    }

    public static string ToCommaDelimitedString(this IEnumerable<string> list)
    {
        return list.IsNullOrEmpty()
          ? null
          : string.Join(",", list);
    }

    public static ArrayList ToArrayList<T>(this IEnumerable<T> collection)
    {
        return new ArrayList(collection.ToList());
    }
}
