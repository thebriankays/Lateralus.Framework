namespace Lateralus.Framework.Html;


public enum HtmlFragmentType
{
    Text,
    TagOpen,     // <
    TagEnd,      // -> TagEnd
    TagEndClose, // />
    TagClose,    // </body
    AttName,
    AttValue,
    Comment,
    CDataText,
}
