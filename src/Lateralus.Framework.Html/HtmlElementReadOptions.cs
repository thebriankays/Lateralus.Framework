#nullable disable

namespace Lateralus.Framework.Html;

public enum HtmlElementReadOptions
{
    None = 0x0,
    InnerRaw = 0x1,
    AutoClosed = 0x2,
    NoChild = 0x4,
}
