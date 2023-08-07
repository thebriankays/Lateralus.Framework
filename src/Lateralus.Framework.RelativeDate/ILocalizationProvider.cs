using System.Globalization;

namespace Lateralus.Framework;

public interface ILocalizationProvider
{
    string GetString(string name, CultureInfo? culture);
}
