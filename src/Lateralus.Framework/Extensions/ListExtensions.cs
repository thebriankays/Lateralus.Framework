namespace Lateralus.Framework;

public static  class ListExtensions
{
    public static IList<T> Prepend<T>(this IList<T> list, Func<T> newItem)
    {
        list.Insert(0, newItem());

        return list;
    }

    public static List<T> Clone<T>(this List<T> collection) where T : class, ICloneable => collection.Select(d => d.Clone() as T).ToList();

    public static DataTable ToDataTable<T>(this IList<T> list)
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new();
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }
        object[] values = new object[props.Count];
        foreach (T item in list)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = props[i].GetValue(item) ?? DBNull.Value;
            table.Rows.Add(values);
        }
        return table;
    }
}
