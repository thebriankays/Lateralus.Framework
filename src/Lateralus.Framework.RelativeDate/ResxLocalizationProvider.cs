using System.Globalization;
using System.Resources;

namespace Lateralus.Framework;

internal sealed class ResxLocalizationProvider : ILocalizationProvider
{
    private static readonly ResourceManager ResourceManager = new ResourceManager("Lateralus.Framework.RelativeDates", typeof(RelativeDates).Assembly);

    public static ILocalizationProvider Instance { get; } = new ResxLocalizationProvider();

    public string GetString(string name, CultureInfo? culture)
    {
        return ResourceManager.GetString(name, culture) ?? "";
    }
}
