namespace Lateralus.Framework;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        return value
            .GetType()
            .GetMember(value.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DescriptionAttribute>()
            ?.Description
          ?? value.ToString();
    }

    /// <summary>
    /// Get XmlEnumAttribute attribute of an enum
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetEnumXmlEnumAttribute(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        XmlEnumAttribute[] attributes =
            (XmlEnumAttribute[])fi.GetCustomAttributes(
            typeof(XmlEnumAttribute),
            false);

        if (attributes != null &&
            attributes.Length > 0)
            return attributes[0].Name;
        else
            return value.ToString();
    }

    public static List<T> GetAllPublicConstantValues<T>(this Type type) => type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
            .Select(x => (T)x.GetRawConstantValue())
            .ToList();
}
