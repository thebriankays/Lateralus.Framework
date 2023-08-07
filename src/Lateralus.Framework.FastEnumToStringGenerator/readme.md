# Lateralus.Framework.FastEnumToStringGenerator

The source generator generates a `ToStringFast` method for some enumerations

````csharp
[assembly: FastEnumToStringAttribute(typeof(Sample.Color), IsPublic = true, ExtensionMethodNamespace = "Sample.Extensions")]

namespace Sample
{
    public enum Color
    {
        Blue,
        Red,
        Green,
    }
}
````

The source generator adds the following code:

````c#
internal static partial class FastEnumToStringExtensions
{
    internal static string ToStringFast(this Sample.Color value)
    {
        return value switch
        {
            case Blue => "Blue",
            case Red => "Red",
            case Green => "Green",
            _ => value.ToString(),
        };
    }
}
````

You can now replace the `ToString` method with the new `ToStringFast` method:

````c#
Color value = Color.Green;
Console.WriteLine(value.ToStringFast());
````

