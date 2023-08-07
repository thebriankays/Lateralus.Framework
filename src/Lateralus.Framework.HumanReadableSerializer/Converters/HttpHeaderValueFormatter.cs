namespace Lateralus.Framework.HumanReadable.Converters;

public abstract class HttpHeaderValueFormatter
{
    public abstract string FormatHeaderValue(string headerName, string headerValue);
}
