namespace Lateralus.Framework;

public static class StringExtensions
{
    [Pure]
    public static string? Nullify(this string? str, bool trim)
    {
        if (str == null)
            return null;

        if (trim)
        {
            str = str.Trim();
        }

        if (string.IsNullOrEmpty(str))
            return null;

        return str;
    }

    [Pure]
    public static bool EqualsOrdinal(this string? str1, string? str2) => string.Equals(str1, str2, StringComparison.Ordinal);

    /// <summary>
    /// Returns TRUE if the specified email address is valid.
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <returns></returns>
    public static bool IsValidEmail(this string str1, string? emailAddress)
    {
        bool isValid = true;

        if (!String.IsNullOrWhiteSpace(emailAddress))
        {
            System.Text.RegularExpressions.Regex regex = new(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
            if (!regex.IsMatch(emailAddress))
            {
                isValid = false;
            }
        }

        return isValid;
    }

    /// <summary>
    /// Locates position to break the given line so as to avoid
    /// breaking words.
    /// </summary>
    /// <param name="text">String that contains line of text</param>
    /// <param name="pos">Index where line of text starts</param>
    /// <param name="max">Maximum line length</param>
    /// <returns>The modified line length</returns>
    public static int BreakLine(this string str1, int pos, int max)
    {
        // Find last whitespace in line
        int i = max;
        while (i >= 0 && !Char.IsWhiteSpace(str1[pos + i]))
            i--;

        // If no whitespace found, break at maximum length
        if (i < 0)
            return max;

        // Find start of whitespace
        while (i >= 0 && Char.IsWhiteSpace(str1[pos + i]))
            i--;

        // Return length of text before whitespace
        return i + 1;
    }

    // Source: http://www.codeproject.com/Articles/51488/Implementing-Word-Wrap-in-C
    /// <summary>
    /// Word wraps the given text to fit within the specified width.
    /// </summary>
    /// <param name="text">Text to be word wrapped</param>
    /// <param name="width">Width, in characters, to which the text
    /// should be word wrapped</param>
    /// <returns>The modified text</returns>
    public static string WorkWrap(this string str1, int width)
    {
        int pos, next;
        StringBuilder sb = new();

        // Lucidity check
        if (width < 1)
            return str1;

        // Parse each line of text
        for (pos = 0; pos < str1.Length; pos = next)
        {
            // Find end of line
            int eol = str1.IndexOf(Environment.NewLine, pos);
            if (eol == -1)
                next = eol = str1.Length;
            else
                next = eol + Environment.NewLine.Length;

            // Copy this line of text, breaking into smaller lines as needed
            if (eol > pos)
            {
                do
                {
                    int len = eol - pos;
                    if (len > width)
                        len = BreakLine(str1, pos, width);
                    sb.Append(str1, pos, len);
                    sb.Append(Environment.NewLine);

                    // Trim whitespace following break
                    pos += len;
                    while (pos < eol && Char.IsWhiteSpace(str1[pos]))
                        pos++;
                } while (eol > pos);
            }
            else sb.Append(Environment.NewLine); // Empty line
        }
        return sb.ToString();
    }

    public static bool EqualsIgnoreCase(this string? str1, string? str2) => string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);

    [Pure]
    public static bool ContainsIgnoreCase(this string str, string value) => str.Contains(value, StringComparison.OrdinalIgnoreCase);

    private static readonly Lazy<Dictionary<char, char>> DiacriticDictionary = new(CreateDiacriticDictionary);

    private static Dictionary<char, char> CreateDiacriticDictionary() => new Dictionary<char, char>()
        {
            { '\u00C0' /* 'À' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u00C1' /* 'Á' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u00C2' /* 'Â' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u00C3' /* 'Ã' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u00C4' /* 'Ä' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u00C5' /* 'Å' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u00C7' /* 'Ç' UppercaseLetter */, '\u0043' /* 'C' UppercaseLetter */ },
            { '\u00C8' /* 'È' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u00C9' /* 'É' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u00CA' /* 'Ê' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u00CB' /* 'Ë' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u00CC' /* 'Ì' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u00CD' /* 'Í' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u00CE' /* 'Î' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u00CF' /* 'Ï' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u00D1' /* 'Ñ' UppercaseLetter */, '\u004E' /* 'N' UppercaseLetter */ },
            { '\u00D2' /* 'Ò' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u00D3' /* 'Ó' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u00D4' /* 'Ô' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u00D5' /* 'Õ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u00D6' /* 'Ö' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u00D9' /* 'Ù' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u00DA' /* 'Ú' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u00DB' /* 'Û' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u00DC' /* 'Ü' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u00DD' /* 'Ý' UppercaseLetter */, '\u0059' /* 'Y' UppercaseLetter */ },
            { '\u00E0' /* 'à' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u00E1' /* 'á' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u00E2' /* 'â' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u00E3' /* 'ã' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u00E4' /* 'ä' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u00E5' /* 'å' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u00E7' /* 'ç' LowercaseLetter */, '\u0063' /* 'c' LowercaseLetter */ },
            { '\u00E8' /* 'è' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u00E9' /* 'é' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u00EA' /* 'ê' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u00EB' /* 'ë' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u00EC' /* 'ì' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u00ED' /* 'í' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u00EE' /* 'î' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u00EF' /* 'ï' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u00F1' /* 'ñ' LowercaseLetter */, '\u006E' /* 'n' LowercaseLetter */ },
            { '\u00F2' /* 'ò' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u00F3' /* 'ó' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u00F4' /* 'ô' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u00F5' /* 'õ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u00F6' /* 'ö' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u00F9' /* 'ù' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u00FA' /* 'ú' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u00FB' /* 'û' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u00FC' /* 'ü' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u00FD' /* 'ý' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u00FF' /* 'ÿ' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u0100' /* 'Ā' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u0101' /* 'ā' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u0102' /* 'Ă' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u0103' /* 'ă' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u0104' /* 'Ą' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u0105' /* 'ą' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u0106' /* 'Ć' UppercaseLetter */, '\u0043' /* 'C' UppercaseLetter */ },
            { '\u0107' /* 'ć' LowercaseLetter */, '\u0063' /* 'c' LowercaseLetter */ },
            { '\u0108' /* 'Ĉ' UppercaseLetter */, '\u0043' /* 'C' UppercaseLetter */ },
            { '\u0109' /* 'ĉ' LowercaseLetter */, '\u0063' /* 'c' LowercaseLetter */ },
            { '\u010A' /* 'Ċ' UppercaseLetter */, '\u0043' /* 'C' UppercaseLetter */ },
            { '\u010B' /* 'ċ' LowercaseLetter */, '\u0063' /* 'c' LowercaseLetter */ },
            { '\u010C' /* 'Č' UppercaseLetter */, '\u0043' /* 'C' UppercaseLetter */ },
            { '\u010D' /* 'č' LowercaseLetter */, '\u0063' /* 'c' LowercaseLetter */ },
            { '\u010E' /* 'Ď' UppercaseLetter */, '\u0044' /* 'D' UppercaseLetter */ },
            { '\u010F' /* 'ď' LowercaseLetter */, '\u0064' /* 'd' LowercaseLetter */ },
            { '\u0112' /* 'Ē' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u0113' /* 'ē' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u0114' /* 'Ĕ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u0115' /* 'ĕ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u0116' /* 'Ė' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u0117' /* 'ė' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u0118' /* 'Ę' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u0119' /* 'ę' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u011A' /* 'Ě' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u011B' /* 'ě' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u011C' /* 'Ĝ' UppercaseLetter */, '\u0047' /* 'G' UppercaseLetter */ },
            { '\u011D' /* 'ĝ' LowercaseLetter */, '\u0067' /* 'g' LowercaseLetter */ },
            { '\u011E' /* 'Ğ' UppercaseLetter */, '\u0047' /* 'G' UppercaseLetter */ },
            { '\u011F' /* 'ğ' LowercaseLetter */, '\u0067' /* 'g' LowercaseLetter */ },
            { '\u0120' /* 'Ġ' UppercaseLetter */, '\u0047' /* 'G' UppercaseLetter */ },
            { '\u0121' /* 'ġ' LowercaseLetter */, '\u0067' /* 'g' LowercaseLetter */ },
            { '\u0122' /* 'Ģ' UppercaseLetter */, '\u0047' /* 'G' UppercaseLetter */ },
            { '\u0123' /* 'ģ' LowercaseLetter */, '\u0067' /* 'g' LowercaseLetter */ },
            { '\u0124' /* 'Ĥ' UppercaseLetter */, '\u0048' /* 'H' UppercaseLetter */ },
            { '\u0125' /* 'ĥ' LowercaseLetter */, '\u0068' /* 'h' LowercaseLetter */ },
            { '\u0128' /* 'Ĩ' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u0129' /* 'ĩ' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u012A' /* 'Ī' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u012B' /* 'ī' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u012C' /* 'Ĭ' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u012D' /* 'ĭ' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u012E' /* 'Į' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u012F' /* 'į' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u0130' /* 'İ' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u0134' /* 'Ĵ' UppercaseLetter */, '\u004A' /* 'J' UppercaseLetter */ },
            { '\u0135' /* 'ĵ' LowercaseLetter */, '\u006A' /* 'j' LowercaseLetter */ },
            { '\u0136' /* 'Ķ' UppercaseLetter */, '\u004B' /* 'K' UppercaseLetter */ },
            { '\u0137' /* 'ķ' LowercaseLetter */, '\u006B' /* 'k' LowercaseLetter */ },
            { '\u0139' /* 'Ĺ' UppercaseLetter */, '\u004C' /* 'L' UppercaseLetter */ },
            { '\u013A' /* 'ĺ' LowercaseLetter */, '\u006C' /* 'l' LowercaseLetter */ },
            { '\u013B' /* 'Ļ' UppercaseLetter */, '\u004C' /* 'L' UppercaseLetter */ },
            { '\u013C' /* 'ļ' LowercaseLetter */, '\u006C' /* 'l' LowercaseLetter */ },
            { '\u013D' /* 'Ľ' UppercaseLetter */, '\u004C' /* 'L' UppercaseLetter */ },
            { '\u013E' /* 'ľ' LowercaseLetter */, '\u006C' /* 'l' LowercaseLetter */ },
            { '\u0143' /* 'Ń' UppercaseLetter */, '\u004E' /* 'N' UppercaseLetter */ },
            { '\u0144' /* 'ń' LowercaseLetter */, '\u006E' /* 'n' LowercaseLetter */ },
            { '\u0145' /* 'Ņ' UppercaseLetter */, '\u004E' /* 'N' UppercaseLetter */ },
            { '\u0146' /* 'ņ' LowercaseLetter */, '\u006E' /* 'n' LowercaseLetter */ },
            { '\u0147' /* 'Ň' UppercaseLetter */, '\u004E' /* 'N' UppercaseLetter */ },
            { '\u0148' /* 'ň' LowercaseLetter */, '\u006E' /* 'n' LowercaseLetter */ },
            { '\u014C' /* 'Ō' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u014D' /* 'ō' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u014E' /* 'Ŏ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u014F' /* 'ŏ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u0150' /* 'Ő' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u0151' /* 'ő' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u0154' /* 'Ŕ' UppercaseLetter */, '\u0052' /* 'R' UppercaseLetter */ },
            { '\u0155' /* 'ŕ' LowercaseLetter */, '\u0072' /* 'r' LowercaseLetter */ },
            { '\u0156' /* 'Ŗ' UppercaseLetter */, '\u0052' /* 'R' UppercaseLetter */ },
            { '\u0157' /* 'ŗ' LowercaseLetter */, '\u0072' /* 'r' LowercaseLetter */ },
            { '\u0158' /* 'Ř' UppercaseLetter */, '\u0052' /* 'R' UppercaseLetter */ },
            { '\u0159' /* 'ř' LowercaseLetter */, '\u0072' /* 'r' LowercaseLetter */ },
            { '\u015A' /* 'Ś' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u015B' /* 'ś' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u015C' /* 'Ŝ' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u015D' /* 'ŝ' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u015E' /* 'Ş' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u015F' /* 'ş' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u0160' /* 'Š' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u0161' /* 'š' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u0162' /* 'Ţ' UppercaseLetter */, '\u0054' /* 'T' UppercaseLetter */ },
            { '\u0163' /* 'ţ' LowercaseLetter */, '\u0074' /* 't' LowercaseLetter */ },
            { '\u0164' /* 'Ť' UppercaseLetter */, '\u0054' /* 'T' UppercaseLetter */ },
            { '\u0165' /* 'ť' LowercaseLetter */, '\u0074' /* 't' LowercaseLetter */ },
            { '\u0168' /* 'Ũ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u0169' /* 'ũ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u016A' /* 'Ū' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u016B' /* 'ū' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u016C' /* 'Ŭ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u016D' /* 'ŭ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u016E' /* 'Ů' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u016F' /* 'ů' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u0170' /* 'Ű' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u0171' /* 'ű' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u0172' /* 'Ų' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u0173' /* 'ų' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u0174' /* 'Ŵ' UppercaseLetter */, '\u0057' /* 'W' UppercaseLetter */ },
            { '\u0175' /* 'ŵ' LowercaseLetter */, '\u0077' /* 'w' LowercaseLetter */ },
            { '\u0176' /* 'Ŷ' UppercaseLetter */, '\u0059' /* 'Y' UppercaseLetter */ },
            { '\u0177' /* 'ŷ' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u0178' /* 'Ÿ' UppercaseLetter */, '\u0059' /* 'Y' UppercaseLetter */ },
            { '\u0179' /* 'Ź' UppercaseLetter */, '\u005A' /* 'Z' UppercaseLetter */ },
            { '\u017A' /* 'ź' LowercaseLetter */, '\u007A' /* 'z' LowercaseLetter */ },
            { '\u017B' /* 'Ż' UppercaseLetter */, '\u005A' /* 'Z' UppercaseLetter */ },
            { '\u017C' /* 'ż' LowercaseLetter */, '\u007A' /* 'z' LowercaseLetter */ },
            { '\u017D' /* 'Ž' UppercaseLetter */, '\u005A' /* 'Z' UppercaseLetter */ },
            { '\u017E' /* 'ž' LowercaseLetter */, '\u007A' /* 'z' LowercaseLetter */ },
            { '\u01A0' /* 'Ơ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u01A1' /* 'ơ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u01AF' /* 'Ư' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u01B0' /* 'ư' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u01CD' /* 'Ǎ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u01CE' /* 'ǎ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u01CF' /* 'Ǐ' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u01D0' /* 'ǐ' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u01D1' /* 'Ǒ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u01D2' /* 'ǒ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u01D3' /* 'Ǔ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u01D4' /* 'ǔ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u01D5' /* 'Ǖ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u01D6' /* 'ǖ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u01D7' /* 'Ǘ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u01D8' /* 'ǘ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u01D9' /* 'Ǚ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u01DA' /* 'ǚ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u01DB' /* 'Ǜ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u01DC' /* 'ǜ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u01DE' /* 'Ǟ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u01DF' /* 'ǟ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u01E0' /* 'Ǡ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u01E1' /* 'ǡ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u01E2' /* 'Ǣ' UppercaseLetter */, '\u00C6' /* 'Æ' UppercaseLetter */ },
            { '\u01E3' /* 'ǣ' LowercaseLetter */, '\u00E6' /* 'æ' LowercaseLetter */ },
            { '\u01E6' /* 'Ǧ' UppercaseLetter */, '\u0047' /* 'G' UppercaseLetter */ },
            { '\u01E7' /* 'ǧ' LowercaseLetter */, '\u0067' /* 'g' LowercaseLetter */ },
            { '\u01E8' /* 'Ǩ' UppercaseLetter */, '\u004B' /* 'K' UppercaseLetter */ },
            { '\u01E9' /* 'ǩ' LowercaseLetter */, '\u006B' /* 'k' LowercaseLetter */ },
            { '\u01EA' /* 'Ǫ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u01EB' /* 'ǫ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u01EC' /* 'Ǭ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u01ED' /* 'ǭ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u01EE' /* 'Ǯ' UppercaseLetter */, '\u01B7' /* 'Ʒ' UppercaseLetter */ },
            { '\u01EF' /* 'ǯ' LowercaseLetter */, '\u0292' /* 'ʒ' LowercaseLetter */ },
            { '\u01F0' /* 'ǰ' LowercaseLetter */, '\u006A' /* 'j' LowercaseLetter */ },
            { '\u01F4' /* 'Ǵ' UppercaseLetter */, '\u0047' /* 'G' UppercaseLetter */ },
            { '\u01F5' /* 'ǵ' LowercaseLetter */, '\u0067' /* 'g' LowercaseLetter */ },
            { '\u01F8' /* 'Ǹ' UppercaseLetter */, '\u004E' /* 'N' UppercaseLetter */ },
            { '\u01F9' /* 'ǹ' LowercaseLetter */, '\u006E' /* 'n' LowercaseLetter */ },
            { '\u01FA' /* 'Ǻ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u01FB' /* 'ǻ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u01FC' /* 'Ǽ' UppercaseLetter */, '\u00C6' /* 'Æ' UppercaseLetter */ },
            { '\u01FD' /* 'ǽ' LowercaseLetter */, '\u00E6' /* 'æ' LowercaseLetter */ },
            { '\u01FE' /* 'Ǿ' UppercaseLetter */, '\u00D8' /* 'Ø' UppercaseLetter */ },
            { '\u01FF' /* 'ǿ' LowercaseLetter */, '\u00F8' /* 'ø' LowercaseLetter */ },
            { '\u0200' /* 'Ȁ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u0201' /* 'ȁ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u0202' /* 'Ȃ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u0203' /* 'ȃ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u0204' /* 'Ȅ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u0205' /* 'ȅ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u0206' /* 'Ȇ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u0207' /* 'ȇ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u0208' /* 'Ȉ' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u0209' /* 'ȉ' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u020A' /* 'Ȋ' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u020B' /* 'ȋ' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u020C' /* 'Ȍ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u020D' /* 'ȍ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u020E' /* 'Ȏ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u020F' /* 'ȏ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u0210' /* 'Ȑ' UppercaseLetter */, '\u0052' /* 'R' UppercaseLetter */ },
            { '\u0211' /* 'ȑ' LowercaseLetter */, '\u0072' /* 'r' LowercaseLetter */ },
            { '\u0212' /* 'Ȓ' UppercaseLetter */, '\u0052' /* 'R' UppercaseLetter */ },
            { '\u0213' /* 'ȓ' LowercaseLetter */, '\u0072' /* 'r' LowercaseLetter */ },
            { '\u0214' /* 'Ȕ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u0215' /* 'ȕ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u0216' /* 'Ȗ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u0217' /* 'ȗ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u0218' /* 'Ș' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u0219' /* 'ș' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u021A' /* 'Ț' UppercaseLetter */, '\u0054' /* 'T' UppercaseLetter */ },
            { '\u021B' /* 'ț' LowercaseLetter */, '\u0074' /* 't' LowercaseLetter */ },
            { '\u021E' /* 'Ȟ' UppercaseLetter */, '\u0048' /* 'H' UppercaseLetter */ },
            { '\u021F' /* 'ȟ' LowercaseLetter */, '\u0068' /* 'h' LowercaseLetter */ },
            { '\u0226' /* 'Ȧ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u0227' /* 'ȧ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u0228' /* 'Ȩ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u0229' /* 'ȩ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u022A' /* 'Ȫ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u022B' /* 'ȫ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u022C' /* 'Ȭ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u022D' /* 'ȭ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u022E' /* 'Ȯ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u022F' /* 'ȯ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u0230' /* 'Ȱ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u0231' /* 'ȱ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u0232' /* 'Ȳ' UppercaseLetter */, '\u0059' /* 'Y' UppercaseLetter */ },
            { '\u0233' /* 'ȳ' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u0374' /* 'ʹ' ModifierLetter */, '\u02B9' /* 'ʹ' ModifierLetter */ },
            { '\u0386' /* 'Ά' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u0388' /* 'Έ' UppercaseLetter */, '\u0395' /* 'Ε' UppercaseLetter */ },
            { '\u0389' /* 'Ή' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u038A' /* 'Ί' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u038C' /* 'Ό' UppercaseLetter */, '\u039F' /* 'Ο' UppercaseLetter */ },
            { '\u038E' /* 'Ύ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u038F' /* 'Ώ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u0390' /* 'ΐ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u03AA' /* 'Ϊ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u03AB' /* 'Ϋ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u03AC' /* 'ά' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u03AD' /* 'έ' LowercaseLetter */, '\u03B5' /* 'ε' LowercaseLetter */ },
            { '\u03AE' /* 'ή' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u03AF' /* 'ί' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u03B0' /* 'ΰ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u03CA' /* 'ϊ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u03CB' /* 'ϋ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u03CC' /* 'ό' LowercaseLetter */, '\u03BF' /* 'ο' LowercaseLetter */ },
            { '\u03CD' /* 'ύ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u03CE' /* 'ώ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u03D3' /* 'ϓ' UppercaseLetter */, '\u03D2' /* 'ϒ' UppercaseLetter */ },
            { '\u03D4' /* 'ϔ' UppercaseLetter */, '\u03D2' /* 'ϒ' UppercaseLetter */ },
            { '\u0400' /* 'Ѐ' UppercaseLetter */, '\u0415' /* 'Е' UppercaseLetter */ },
            { '\u0401' /* 'Ё' UppercaseLetter */, '\u0415' /* 'Е' UppercaseLetter */ },
            { '\u0403' /* 'Ѓ' UppercaseLetter */, '\u0413' /* 'Г' UppercaseLetter */ },
            { '\u0407' /* 'Ї' UppercaseLetter */, '\u0406' /* 'І' UppercaseLetter */ },
            { '\u040C' /* 'Ќ' UppercaseLetter */, '\u041A' /* 'К' UppercaseLetter */ },
            { '\u040D' /* 'Ѝ' UppercaseLetter */, '\u0418' /* 'И' UppercaseLetter */ },
            { '\u040E' /* 'Ў' UppercaseLetter */, '\u0423' /* 'У' UppercaseLetter */ },
            { '\u0419' /* 'Й' UppercaseLetter */, '\u0418' /* 'И' UppercaseLetter */ },
            { '\u0439' /* 'й' LowercaseLetter */, '\u0438' /* 'и' LowercaseLetter */ },
            { '\u0450' /* 'ѐ' LowercaseLetter */, '\u0435' /* 'е' LowercaseLetter */ },
            { '\u0451' /* 'ё' LowercaseLetter */, '\u0435' /* 'е' LowercaseLetter */ },
            { '\u0453' /* 'ѓ' LowercaseLetter */, '\u0433' /* 'г' LowercaseLetter */ },
            { '\u0457' /* 'ї' LowercaseLetter */, '\u0456' /* 'і' LowercaseLetter */ },
            { '\u045C' /* 'ќ' LowercaseLetter */, '\u043A' /* 'к' LowercaseLetter */ },
            { '\u045D' /* 'ѝ' LowercaseLetter */, '\u0438' /* 'и' LowercaseLetter */ },
            { '\u045E' /* 'ў' LowercaseLetter */, '\u0443' /* 'у' LowercaseLetter */ },
            { '\u0476' /* 'Ѷ' UppercaseLetter */, '\u0474' /* 'Ѵ' UppercaseLetter */ },
            { '\u0477' /* 'ѷ' LowercaseLetter */, '\u0475' /* 'ѵ' LowercaseLetter */ },
            { '\u04C1' /* 'Ӂ' UppercaseLetter */, '\u0416' /* 'Ж' UppercaseLetter */ },
            { '\u04C2' /* 'ӂ' LowercaseLetter */, '\u0436' /* 'ж' LowercaseLetter */ },
            { '\u04D0' /* 'Ӑ' UppercaseLetter */, '\u0410' /* 'А' UppercaseLetter */ },
            { '\u04D1' /* 'ӑ' LowercaseLetter */, '\u0430' /* 'а' LowercaseLetter */ },
            { '\u04D2' /* 'Ӓ' UppercaseLetter */, '\u0410' /* 'А' UppercaseLetter */ },
            { '\u04D3' /* 'ӓ' LowercaseLetter */, '\u0430' /* 'а' LowercaseLetter */ },
            { '\u04D6' /* 'Ӗ' UppercaseLetter */, '\u0415' /* 'Е' UppercaseLetter */ },
            { '\u04D7' /* 'ӗ' LowercaseLetter */, '\u0435' /* 'е' LowercaseLetter */ },
            { '\u04DA' /* 'Ӛ' UppercaseLetter */, '\u04D8' /* 'Ә' UppercaseLetter */ },
            { '\u04DB' /* 'ӛ' LowercaseLetter */, '\u04D9' /* 'ә' LowercaseLetter */ },
            { '\u04DC' /* 'Ӝ' UppercaseLetter */, '\u0416' /* 'Ж' UppercaseLetter */ },
            { '\u04DD' /* 'ӝ' LowercaseLetter */, '\u0436' /* 'ж' LowercaseLetter */ },
            { '\u04DE' /* 'Ӟ' UppercaseLetter */, '\u0417' /* 'З' UppercaseLetter */ },
            { '\u04DF' /* 'ӟ' LowercaseLetter */, '\u0437' /* 'з' LowercaseLetter */ },
            { '\u04E2' /* 'Ӣ' UppercaseLetter */, '\u0418' /* 'И' UppercaseLetter */ },
            { '\u04E3' /* 'ӣ' LowercaseLetter */, '\u0438' /* 'и' LowercaseLetter */ },
            { '\u04E4' /* 'Ӥ' UppercaseLetter */, '\u0418' /* 'И' UppercaseLetter */ },
            { '\u04E5' /* 'ӥ' LowercaseLetter */, '\u0438' /* 'и' LowercaseLetter */ },
            { '\u04E6' /* 'Ӧ' UppercaseLetter */, '\u041E' /* 'О' UppercaseLetter */ },
            { '\u04E7' /* 'ӧ' LowercaseLetter */, '\u043E' /* 'о' LowercaseLetter */ },
            { '\u04EA' /* 'Ӫ' UppercaseLetter */, '\u04E8' /* 'Ө' UppercaseLetter */ },
            { '\u04EB' /* 'ӫ' LowercaseLetter */, '\u04E9' /* 'ө' LowercaseLetter */ },
            { '\u04EC' /* 'Ӭ' UppercaseLetter */, '\u042D' /* 'Э' UppercaseLetter */ },
            { '\u04ED' /* 'ӭ' LowercaseLetter */, '\u044D' /* 'э' LowercaseLetter */ },
            { '\u04EE' /* 'Ӯ' UppercaseLetter */, '\u0423' /* 'У' UppercaseLetter */ },
            { '\u04EF' /* 'ӯ' LowercaseLetter */, '\u0443' /* 'у' LowercaseLetter */ },
            { '\u04F0' /* 'Ӱ' UppercaseLetter */, '\u0423' /* 'У' UppercaseLetter */ },
            { '\u04F1' /* 'ӱ' LowercaseLetter */, '\u0443' /* 'у' LowercaseLetter */ },
            { '\u04F2' /* 'Ӳ' UppercaseLetter */, '\u0423' /* 'У' UppercaseLetter */ },
            { '\u04F3' /* 'ӳ' LowercaseLetter */, '\u0443' /* 'у' LowercaseLetter */ },
            { '\u04F4' /* 'Ӵ' UppercaseLetter */, '\u0427' /* 'Ч' UppercaseLetter */ },
            { '\u04F5' /* 'ӵ' LowercaseLetter */, '\u0447' /* 'ч' LowercaseLetter */ },
            { '\u04F8' /* 'Ӹ' UppercaseLetter */, '\u042B' /* 'Ы' UppercaseLetter */ },
            { '\u04F9' /* 'ӹ' LowercaseLetter */, '\u044B' /* 'ы' LowercaseLetter */ },
            { '\u1E00' /* 'Ḁ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1E01' /* 'ḁ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1E02' /* 'Ḃ' UppercaseLetter */, '\u0042' /* 'B' UppercaseLetter */ },
            { '\u1E03' /* 'ḃ' LowercaseLetter */, '\u0062' /* 'b' LowercaseLetter */ },
            { '\u1E04' /* 'Ḅ' UppercaseLetter */, '\u0042' /* 'B' UppercaseLetter */ },
            { '\u1E05' /* 'ḅ' LowercaseLetter */, '\u0062' /* 'b' LowercaseLetter */ },
            { '\u1E06' /* 'Ḇ' UppercaseLetter */, '\u0042' /* 'B' UppercaseLetter */ },
            { '\u1E07' /* 'ḇ' LowercaseLetter */, '\u0062' /* 'b' LowercaseLetter */ },
            { '\u1E08' /* 'Ḉ' UppercaseLetter */, '\u0043' /* 'C' UppercaseLetter */ },
            { '\u1E09' /* 'ḉ' LowercaseLetter */, '\u0063' /* 'c' LowercaseLetter */ },
            { '\u1E0A' /* 'Ḋ' UppercaseLetter */, '\u0044' /* 'D' UppercaseLetter */ },
            { '\u1E0B' /* 'ḋ' LowercaseLetter */, '\u0064' /* 'd' LowercaseLetter */ },
            { '\u1E0C' /* 'Ḍ' UppercaseLetter */, '\u0044' /* 'D' UppercaseLetter */ },
            { '\u1E0D' /* 'ḍ' LowercaseLetter */, '\u0064' /* 'd' LowercaseLetter */ },
            { '\u1E0E' /* 'Ḏ' UppercaseLetter */, '\u0044' /* 'D' UppercaseLetter */ },
            { '\u1E0F' /* 'ḏ' LowercaseLetter */, '\u0064' /* 'd' LowercaseLetter */ },
            { '\u1E10' /* 'Ḑ' UppercaseLetter */, '\u0044' /* 'D' UppercaseLetter */ },
            { '\u1E11' /* 'ḑ' LowercaseLetter */, '\u0064' /* 'd' LowercaseLetter */ },
            { '\u1E12' /* 'Ḓ' UppercaseLetter */, '\u0044' /* 'D' UppercaseLetter */ },
            { '\u1E13' /* 'ḓ' LowercaseLetter */, '\u0064' /* 'd' LowercaseLetter */ },
            { '\u1E14' /* 'Ḕ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1E15' /* 'ḕ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1E16' /* 'Ḗ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1E17' /* 'ḗ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1E18' /* 'Ḙ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1E19' /* 'ḙ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1E1A' /* 'Ḛ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1E1B' /* 'ḛ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1E1C' /* 'Ḝ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1E1D' /* 'ḝ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1E1E' /* 'Ḟ' UppercaseLetter */, '\u0046' /* 'F' UppercaseLetter */ },
            { '\u1E1F' /* 'ḟ' LowercaseLetter */, '\u0066' /* 'f' LowercaseLetter */ },
            { '\u1E20' /* 'Ḡ' UppercaseLetter */, '\u0047' /* 'G' UppercaseLetter */ },
            { '\u1E21' /* 'ḡ' LowercaseLetter */, '\u0067' /* 'g' LowercaseLetter */ },
            { '\u1E22' /* 'Ḣ' UppercaseLetter */, '\u0048' /* 'H' UppercaseLetter */ },
            { '\u1E23' /* 'ḣ' LowercaseLetter */, '\u0068' /* 'h' LowercaseLetter */ },
            { '\u1E24' /* 'Ḥ' UppercaseLetter */, '\u0048' /* 'H' UppercaseLetter */ },
            { '\u1E25' /* 'ḥ' LowercaseLetter */, '\u0068' /* 'h' LowercaseLetter */ },
            { '\u1E26' /* 'Ḧ' UppercaseLetter */, '\u0048' /* 'H' UppercaseLetter */ },
            { '\u1E27' /* 'ḧ' LowercaseLetter */, '\u0068' /* 'h' LowercaseLetter */ },
            { '\u1E28' /* 'Ḩ' UppercaseLetter */, '\u0048' /* 'H' UppercaseLetter */ },
            { '\u1E29' /* 'ḩ' LowercaseLetter */, '\u0068' /* 'h' LowercaseLetter */ },
            { '\u1E2A' /* 'Ḫ' UppercaseLetter */, '\u0048' /* 'H' UppercaseLetter */ },
            { '\u1E2B' /* 'ḫ' LowercaseLetter */, '\u0068' /* 'h' LowercaseLetter */ },
            { '\u1E2C' /* 'Ḭ' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u1E2D' /* 'ḭ' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u1E2E' /* 'Ḯ' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u1E2F' /* 'ḯ' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u1E30' /* 'Ḱ' UppercaseLetter */, '\u004B' /* 'K' UppercaseLetter */ },
            { '\u1E31' /* 'ḱ' LowercaseLetter */, '\u006B' /* 'k' LowercaseLetter */ },
            { '\u1E32' /* 'Ḳ' UppercaseLetter */, '\u004B' /* 'K' UppercaseLetter */ },
            { '\u1E33' /* 'ḳ' LowercaseLetter */, '\u006B' /* 'k' LowercaseLetter */ },
            { '\u1E34' /* 'Ḵ' UppercaseLetter */, '\u004B' /* 'K' UppercaseLetter */ },
            { '\u1E35' /* 'ḵ' LowercaseLetter */, '\u006B' /* 'k' LowercaseLetter */ },
            { '\u1E36' /* 'Ḷ' UppercaseLetter */, '\u004C' /* 'L' UppercaseLetter */ },
            { '\u1E37' /* 'ḷ' LowercaseLetter */, '\u006C' /* 'l' LowercaseLetter */ },
            { '\u1E38' /* 'Ḹ' UppercaseLetter */, '\u004C' /* 'L' UppercaseLetter */ },
            { '\u1E39' /* 'ḹ' LowercaseLetter */, '\u006C' /* 'l' LowercaseLetter */ },
            { '\u1E3A' /* 'Ḻ' UppercaseLetter */, '\u004C' /* 'L' UppercaseLetter */ },
            { '\u1E3B' /* 'ḻ' LowercaseLetter */, '\u006C' /* 'l' LowercaseLetter */ },
            { '\u1E3C' /* 'Ḽ' UppercaseLetter */, '\u004C' /* 'L' UppercaseLetter */ },
            { '\u1E3D' /* 'ḽ' LowercaseLetter */, '\u006C' /* 'l' LowercaseLetter */ },
            { '\u1E3E' /* 'Ḿ' UppercaseLetter */, '\u004D' /* 'M' UppercaseLetter */ },
            { '\u1E3F' /* 'ḿ' LowercaseLetter */, '\u006D' /* 'm' LowercaseLetter */ },
            { '\u1E40' /* 'Ṁ' UppercaseLetter */, '\u004D' /* 'M' UppercaseLetter */ },
            { '\u1E41' /* 'ṁ' LowercaseLetter */, '\u006D' /* 'm' LowercaseLetter */ },
            { '\u1E42' /* 'Ṃ' UppercaseLetter */, '\u004D' /* 'M' UppercaseLetter */ },
            { '\u1E43' /* 'ṃ' LowercaseLetter */, '\u006D' /* 'm' LowercaseLetter */ },
            { '\u1E44' /* 'Ṅ' UppercaseLetter */, '\u004E' /* 'N' UppercaseLetter */ },
            { '\u1E45' /* 'ṅ' LowercaseLetter */, '\u006E' /* 'n' LowercaseLetter */ },
            { '\u1E46' /* 'Ṇ' UppercaseLetter */, '\u004E' /* 'N' UppercaseLetter */ },
            { '\u1E47' /* 'ṇ' LowercaseLetter */, '\u006E' /* 'n' LowercaseLetter */ },
            { '\u1E48' /* 'Ṉ' UppercaseLetter */, '\u004E' /* 'N' UppercaseLetter */ },
            { '\u1E49' /* 'ṉ' LowercaseLetter */, '\u006E' /* 'n' LowercaseLetter */ },
            { '\u1E4A' /* 'Ṋ' UppercaseLetter */, '\u004E' /* 'N' UppercaseLetter */ },
            { '\u1E4B' /* 'ṋ' LowercaseLetter */, '\u006E' /* 'n' LowercaseLetter */ },
            { '\u1E4C' /* 'Ṍ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1E4D' /* 'ṍ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1E4E' /* 'Ṏ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1E4F' /* 'ṏ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1E50' /* 'Ṑ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1E51' /* 'ṑ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1E52' /* 'Ṓ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1E53' /* 'ṓ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1E54' /* 'Ṕ' UppercaseLetter */, '\u0050' /* 'P' UppercaseLetter */ },
            { '\u1E55' /* 'ṕ' LowercaseLetter */, '\u0070' /* 'p' LowercaseLetter */ },
            { '\u1E56' /* 'Ṗ' UppercaseLetter */, '\u0050' /* 'P' UppercaseLetter */ },
            { '\u1E57' /* 'ṗ' LowercaseLetter */, '\u0070' /* 'p' LowercaseLetter */ },
            { '\u1E58' /* 'Ṙ' UppercaseLetter */, '\u0052' /* 'R' UppercaseLetter */ },
            { '\u1E59' /* 'ṙ' LowercaseLetter */, '\u0072' /* 'r' LowercaseLetter */ },
            { '\u1E5A' /* 'Ṛ' UppercaseLetter */, '\u0052' /* 'R' UppercaseLetter */ },
            { '\u1E5B' /* 'ṛ' LowercaseLetter */, '\u0072' /* 'r' LowercaseLetter */ },
            { '\u1E5C' /* 'Ṝ' UppercaseLetter */, '\u0052' /* 'R' UppercaseLetter */ },
            { '\u1E5D' /* 'ṝ' LowercaseLetter */, '\u0072' /* 'r' LowercaseLetter */ },
            { '\u1E5E' /* 'Ṟ' UppercaseLetter */, '\u0052' /* 'R' UppercaseLetter */ },
            { '\u1E5F' /* 'ṟ' LowercaseLetter */, '\u0072' /* 'r' LowercaseLetter */ },
            { '\u1E60' /* 'Ṡ' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u1E61' /* 'ṡ' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u1E62' /* 'Ṣ' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u1E63' /* 'ṣ' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u1E64' /* 'Ṥ' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u1E65' /* 'ṥ' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u1E66' /* 'Ṧ' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u1E67' /* 'ṧ' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u1E68' /* 'Ṩ' UppercaseLetter */, '\u0053' /* 'S' UppercaseLetter */ },
            { '\u1E69' /* 'ṩ' LowercaseLetter */, '\u0073' /* 's' LowercaseLetter */ },
            { '\u1E6A' /* 'Ṫ' UppercaseLetter */, '\u0054' /* 'T' UppercaseLetter */ },
            { '\u1E6B' /* 'ṫ' LowercaseLetter */, '\u0074' /* 't' LowercaseLetter */ },
            { '\u1E6C' /* 'Ṭ' UppercaseLetter */, '\u0054' /* 'T' UppercaseLetter */ },
            { '\u1E6D' /* 'ṭ' LowercaseLetter */, '\u0074' /* 't' LowercaseLetter */ },
            { '\u1E6E' /* 'Ṯ' UppercaseLetter */, '\u0054' /* 'T' UppercaseLetter */ },
            { '\u1E6F' /* 'ṯ' LowercaseLetter */, '\u0074' /* 't' LowercaseLetter */ },
            { '\u1E70' /* 'Ṱ' UppercaseLetter */, '\u0054' /* 'T' UppercaseLetter */ },
            { '\u1E71' /* 'ṱ' LowercaseLetter */, '\u0074' /* 't' LowercaseLetter */ },
            { '\u1E72' /* 'Ṳ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1E73' /* 'ṳ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1E74' /* 'Ṵ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1E75' /* 'ṵ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1E76' /* 'Ṷ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1E77' /* 'ṷ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1E78' /* 'Ṹ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1E79' /* 'ṹ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1E7A' /* 'Ṻ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1E7B' /* 'ṻ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1E7C' /* 'Ṽ' UppercaseLetter */, '\u0056' /* 'V' UppercaseLetter */ },
            { '\u1E7D' /* 'ṽ' LowercaseLetter */, '\u0076' /* 'v' LowercaseLetter */ },
            { '\u1E7E' /* 'Ṿ' UppercaseLetter */, '\u0056' /* 'V' UppercaseLetter */ },
            { '\u1E7F' /* 'ṿ' LowercaseLetter */, '\u0076' /* 'v' LowercaseLetter */ },
            { '\u1E80' /* 'Ẁ' UppercaseLetter */, '\u0057' /* 'W' UppercaseLetter */ },
            { '\u1E81' /* 'ẁ' LowercaseLetter */, '\u0077' /* 'w' LowercaseLetter */ },
            { '\u1E82' /* 'Ẃ' UppercaseLetter */, '\u0057' /* 'W' UppercaseLetter */ },
            { '\u1E83' /* 'ẃ' LowercaseLetter */, '\u0077' /* 'w' LowercaseLetter */ },
            { '\u1E84' /* 'Ẅ' UppercaseLetter */, '\u0057' /* 'W' UppercaseLetter */ },
            { '\u1E85' /* 'ẅ' LowercaseLetter */, '\u0077' /* 'w' LowercaseLetter */ },
            { '\u1E86' /* 'Ẇ' UppercaseLetter */, '\u0057' /* 'W' UppercaseLetter */ },
            { '\u1E87' /* 'ẇ' LowercaseLetter */, '\u0077' /* 'w' LowercaseLetter */ },
            { '\u1E88' /* 'Ẉ' UppercaseLetter */, '\u0057' /* 'W' UppercaseLetter */ },
            { '\u1E89' /* 'ẉ' LowercaseLetter */, '\u0077' /* 'w' LowercaseLetter */ },
            { '\u1E8A' /* 'Ẋ' UppercaseLetter */, '\u0058' /* 'X' UppercaseLetter */ },
            { '\u1E8B' /* 'ẋ' LowercaseLetter */, '\u0078' /* 'x' LowercaseLetter */ },
            { '\u1E8C' /* 'Ẍ' UppercaseLetter */, '\u0058' /* 'X' UppercaseLetter */ },
            { '\u1E8D' /* 'ẍ' LowercaseLetter */, '\u0078' /* 'x' LowercaseLetter */ },
            { '\u1E8E' /* 'Ẏ' UppercaseLetter */, '\u0059' /* 'Y' UppercaseLetter */ },
            { '\u1E8F' /* 'ẏ' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u1E90' /* 'Ẑ' UppercaseLetter */, '\u005A' /* 'Z' UppercaseLetter */ },
            { '\u1E91' /* 'ẑ' LowercaseLetter */, '\u007A' /* 'z' LowercaseLetter */ },
            { '\u1E92' /* 'Ẓ' UppercaseLetter */, '\u005A' /* 'Z' UppercaseLetter */ },
            { '\u1E93' /* 'ẓ' LowercaseLetter */, '\u007A' /* 'z' LowercaseLetter */ },
            { '\u1E94' /* 'Ẕ' UppercaseLetter */, '\u005A' /* 'Z' UppercaseLetter */ },
            { '\u1E95' /* 'ẕ' LowercaseLetter */, '\u007A' /* 'z' LowercaseLetter */ },
            { '\u1E96' /* 'ẖ' LowercaseLetter */, '\u0068' /* 'h' LowercaseLetter */ },
            { '\u1E97' /* 'ẗ' LowercaseLetter */, '\u0074' /* 't' LowercaseLetter */ },
            { '\u1E98' /* 'ẘ' LowercaseLetter */, '\u0077' /* 'w' LowercaseLetter */ },
            { '\u1E99' /* 'ẙ' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u1E9B' /* 'ẛ' LowercaseLetter */, '\u017F' /* 'ſ' LowercaseLetter */ },
            { '\u1EA0' /* 'Ạ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EA1' /* 'ạ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EA2' /* 'Ả' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EA3' /* 'ả' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EA4' /* 'Ấ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EA5' /* 'ấ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EA6' /* 'Ầ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EA7' /* 'ầ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EA8' /* 'Ẩ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EA9' /* 'ẩ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EAA' /* 'Ẫ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EAB' /* 'ẫ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EAC' /* 'Ậ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EAD' /* 'ậ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EAE' /* 'Ắ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EAF' /* 'ắ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EB0' /* 'Ằ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EB1' /* 'ằ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EB2' /* 'Ẳ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EB3' /* 'ẳ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EB4' /* 'Ẵ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EB5' /* 'ẵ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EB6' /* 'Ặ' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u1EB7' /* 'ặ' LowercaseLetter */, '\u0061' /* 'a' LowercaseLetter */ },
            { '\u1EB8' /* 'Ẹ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1EB9' /* 'ẹ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1EBA' /* 'Ẻ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1EBB' /* 'ẻ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1EBC' /* 'Ẽ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1EBD' /* 'ẽ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1EBE' /* 'Ế' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1EBF' /* 'ế' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1EC0' /* 'Ề' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1EC1' /* 'ề' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1EC2' /* 'Ể' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1EC3' /* 'ể' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1EC4' /* 'Ễ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1EC5' /* 'ễ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1EC6' /* 'Ệ' UppercaseLetter */, '\u0045' /* 'E' UppercaseLetter */ },
            { '\u1EC7' /* 'ệ' LowercaseLetter */, '\u0065' /* 'e' LowercaseLetter */ },
            { '\u1EC8' /* 'Ỉ' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u1EC9' /* 'ỉ' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u1ECA' /* 'Ị' UppercaseLetter */, '\u0049' /* 'I' UppercaseLetter */ },
            { '\u1ECB' /* 'ị' LowercaseLetter */, '\u0069' /* 'i' LowercaseLetter */ },
            { '\u1ECC' /* 'Ọ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1ECD' /* 'ọ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1ECE' /* 'Ỏ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1ECF' /* 'ỏ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1ED0' /* 'Ố' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1ED1' /* 'ố' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1ED2' /* 'Ồ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1ED3' /* 'ồ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1ED4' /* 'Ổ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1ED5' /* 'ổ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1ED6' /* 'Ỗ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1ED7' /* 'ỗ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1ED8' /* 'Ộ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1ED9' /* 'ộ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1EDA' /* 'Ớ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1EDB' /* 'ớ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1EDC' /* 'Ờ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1EDD' /* 'ờ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1EDE' /* 'Ở' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1EDF' /* 'ở' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1EE0' /* 'Ỡ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1EE1' /* 'ỡ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1EE2' /* 'Ợ' UppercaseLetter */, '\u004F' /* 'O' UppercaseLetter */ },
            { '\u1EE3' /* 'ợ' LowercaseLetter */, '\u006F' /* 'o' LowercaseLetter */ },
            { '\u1EE4' /* 'Ụ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1EE5' /* 'ụ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1EE6' /* 'Ủ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1EE7' /* 'ủ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1EE8' /* 'Ứ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1EE9' /* 'ứ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1EEA' /* 'Ừ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1EEB' /* 'ừ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1EEC' /* 'Ử' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1EED' /* 'ử' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1EEE' /* 'Ữ' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1EEF' /* 'ữ' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1EF0' /* 'Ự' UppercaseLetter */, '\u0055' /* 'U' UppercaseLetter */ },
            { '\u1EF1' /* 'ự' LowercaseLetter */, '\u0075' /* 'u' LowercaseLetter */ },
            { '\u1EF2' /* 'Ỳ' UppercaseLetter */, '\u0059' /* 'Y' UppercaseLetter */ },
            { '\u1EF3' /* 'ỳ' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u1EF4' /* 'Ỵ' UppercaseLetter */, '\u0059' /* 'Y' UppercaseLetter */ },
            { '\u1EF5' /* 'ỵ' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u1EF6' /* 'Ỷ' UppercaseLetter */, '\u0059' /* 'Y' UppercaseLetter */ },
            { '\u1EF7' /* 'ỷ' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u1EF8' /* 'Ỹ' UppercaseLetter */, '\u0059' /* 'Y' UppercaseLetter */ },
            { '\u1EF9' /* 'ỹ' LowercaseLetter */, '\u0079' /* 'y' LowercaseLetter */ },
            { '\u1F00' /* 'ἀ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F01' /* 'ἁ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F02' /* 'ἂ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F03' /* 'ἃ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F04' /* 'ἄ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F05' /* 'ἅ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F06' /* 'ἆ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F07' /* 'ἇ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F08' /* 'Ἀ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F09' /* 'Ἁ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F0A' /* 'Ἂ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F0B' /* 'Ἃ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F0C' /* 'Ἄ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F0D' /* 'Ἅ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F0E' /* 'Ἆ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F0F' /* 'Ἇ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F10' /* 'ἐ' LowercaseLetter */, '\u03B5' /* 'ε' LowercaseLetter */ },
            { '\u1F11' /* 'ἑ' LowercaseLetter */, '\u03B5' /* 'ε' LowercaseLetter */ },
            { '\u1F12' /* 'ἒ' LowercaseLetter */, '\u03B5' /* 'ε' LowercaseLetter */ },
            { '\u1F13' /* 'ἓ' LowercaseLetter */, '\u03B5' /* 'ε' LowercaseLetter */ },
            { '\u1F14' /* 'ἔ' LowercaseLetter */, '\u03B5' /* 'ε' LowercaseLetter */ },
            { '\u1F15' /* 'ἕ' LowercaseLetter */, '\u03B5' /* 'ε' LowercaseLetter */ },
            { '\u1F18' /* 'Ἐ' UppercaseLetter */, '\u0395' /* 'Ε' UppercaseLetter */ },
            { '\u1F19' /* 'Ἑ' UppercaseLetter */, '\u0395' /* 'Ε' UppercaseLetter */ },
            { '\u1F1A' /* 'Ἒ' UppercaseLetter */, '\u0395' /* 'Ε' UppercaseLetter */ },
            { '\u1F1B' /* 'Ἓ' UppercaseLetter */, '\u0395' /* 'Ε' UppercaseLetter */ },
            { '\u1F1C' /* 'Ἔ' UppercaseLetter */, '\u0395' /* 'Ε' UppercaseLetter */ },
            { '\u1F1D' /* 'Ἕ' UppercaseLetter */, '\u0395' /* 'Ε' UppercaseLetter */ },
            { '\u1F20' /* 'ἠ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F21' /* 'ἡ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F22' /* 'ἢ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F23' /* 'ἣ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F24' /* 'ἤ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F25' /* 'ἥ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F26' /* 'ἦ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F27' /* 'ἧ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F28' /* 'Ἠ' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F29' /* 'Ἡ' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F2A' /* 'Ἢ' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F2B' /* 'Ἣ' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F2C' /* 'Ἤ' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F2D' /* 'Ἥ' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F2E' /* 'Ἦ' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F2F' /* 'Ἧ' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F30' /* 'ἰ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F31' /* 'ἱ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F32' /* 'ἲ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F33' /* 'ἳ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F34' /* 'ἴ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F35' /* 'ἵ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F36' /* 'ἶ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F37' /* 'ἷ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F38' /* 'Ἰ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1F39' /* 'Ἱ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1F3A' /* 'Ἲ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1F3B' /* 'Ἳ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1F3C' /* 'Ἴ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1F3D' /* 'Ἵ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1F3E' /* 'Ἶ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1F3F' /* 'Ἷ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1F40' /* 'ὀ' LowercaseLetter */, '\u03BF' /* 'ο' LowercaseLetter */ },
            { '\u1F41' /* 'ὁ' LowercaseLetter */, '\u03BF' /* 'ο' LowercaseLetter */ },
            { '\u1F42' /* 'ὂ' LowercaseLetter */, '\u03BF' /* 'ο' LowercaseLetter */ },
            { '\u1F43' /* 'ὃ' LowercaseLetter */, '\u03BF' /* 'ο' LowercaseLetter */ },
            { '\u1F44' /* 'ὄ' LowercaseLetter */, '\u03BF' /* 'ο' LowercaseLetter */ },
            { '\u1F45' /* 'ὅ' LowercaseLetter */, '\u03BF' /* 'ο' LowercaseLetter */ },
            { '\u1F48' /* 'Ὀ' UppercaseLetter */, '\u039F' /* 'Ο' UppercaseLetter */ },
            { '\u1F49' /* 'Ὁ' UppercaseLetter */, '\u039F' /* 'Ο' UppercaseLetter */ },
            { '\u1F4A' /* 'Ὂ' UppercaseLetter */, '\u039F' /* 'Ο' UppercaseLetter */ },
            { '\u1F4B' /* 'Ὃ' UppercaseLetter */, '\u039F' /* 'Ο' UppercaseLetter */ },
            { '\u1F4C' /* 'Ὄ' UppercaseLetter */, '\u039F' /* 'Ο' UppercaseLetter */ },
            { '\u1F4D' /* 'Ὅ' UppercaseLetter */, '\u039F' /* 'Ο' UppercaseLetter */ },
            { '\u1F50' /* 'ὐ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F51' /* 'ὑ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F52' /* 'ὒ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F53' /* 'ὓ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F54' /* 'ὔ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F55' /* 'ὕ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F56' /* 'ὖ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F57' /* 'ὗ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F59' /* 'Ὑ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u1F5B' /* 'Ὓ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u1F5D' /* 'Ὕ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u1F5F' /* 'Ὗ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u1F60' /* 'ὠ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F61' /* 'ὡ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F62' /* 'ὢ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F63' /* 'ὣ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F64' /* 'ὤ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F65' /* 'ὥ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F66' /* 'ὦ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F67' /* 'ὧ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F68' /* 'Ὠ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1F69' /* 'Ὡ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1F6A' /* 'Ὢ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1F6B' /* 'Ὣ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1F6C' /* 'Ὤ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1F6D' /* 'Ὥ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1F6E' /* 'Ὦ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1F6F' /* 'Ὧ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1F70' /* 'ὰ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F71' /* 'ά' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F72' /* 'ὲ' LowercaseLetter */, '\u03B5' /* 'ε' LowercaseLetter */ },
            { '\u1F73' /* 'έ' LowercaseLetter */, '\u03B5' /* 'ε' LowercaseLetter */ },
            { '\u1F74' /* 'ὴ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F75' /* 'ή' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F76' /* 'ὶ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F77' /* 'ί' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1F78' /* 'ὸ' LowercaseLetter */, '\u03BF' /* 'ο' LowercaseLetter */ },
            { '\u1F79' /* 'ό' LowercaseLetter */, '\u03BF' /* 'ο' LowercaseLetter */ },
            { '\u1F7A' /* 'ὺ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F7B' /* 'ύ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1F7C' /* 'ὼ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F7D' /* 'ώ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1F80' /* 'ᾀ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F81' /* 'ᾁ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F82' /* 'ᾂ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F83' /* 'ᾃ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F84' /* 'ᾄ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F85' /* 'ᾅ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F86' /* 'ᾆ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F87' /* 'ᾇ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1F88' /* 'ᾈ' TitlecaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F89' /* 'ᾉ' TitlecaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F8A' /* 'ᾊ' TitlecaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F8B' /* 'ᾋ' TitlecaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F8C' /* 'ᾌ' TitlecaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F8D' /* 'ᾍ' TitlecaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F8E' /* 'ᾎ' TitlecaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F8F' /* 'ᾏ' TitlecaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1F90' /* 'ᾐ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F91' /* 'ᾑ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F92' /* 'ᾒ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F93' /* 'ᾓ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F94' /* 'ᾔ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F95' /* 'ᾕ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F96' /* 'ᾖ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F97' /* 'ᾗ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1F98' /* 'ᾘ' TitlecaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F99' /* 'ᾙ' TitlecaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F9A' /* 'ᾚ' TitlecaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F9B' /* 'ᾛ' TitlecaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F9C' /* 'ᾜ' TitlecaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F9D' /* 'ᾝ' TitlecaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F9E' /* 'ᾞ' TitlecaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1F9F' /* 'ᾟ' TitlecaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1FA0' /* 'ᾠ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FA1' /* 'ᾡ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FA2' /* 'ᾢ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FA3' /* 'ᾣ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FA4' /* 'ᾤ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FA5' /* 'ᾥ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FA6' /* 'ᾦ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FA7' /* 'ᾧ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FA8' /* 'ᾨ' TitlecaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FA9' /* 'ᾩ' TitlecaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FAA' /* 'ᾪ' TitlecaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FAB' /* 'ᾫ' TitlecaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FAC' /* 'ᾬ' TitlecaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FAD' /* 'ᾭ' TitlecaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FAE' /* 'ᾮ' TitlecaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FAF' /* 'ᾯ' TitlecaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FB0' /* 'ᾰ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1FB1' /* 'ᾱ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1FB2' /* 'ᾲ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1FB3' /* 'ᾳ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1FB4' /* 'ᾴ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1FB6' /* 'ᾶ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1FB7' /* 'ᾷ' LowercaseLetter */, '\u03B1' /* 'α' LowercaseLetter */ },
            { '\u1FB8' /* 'Ᾰ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1FB9' /* 'Ᾱ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1FBA' /* 'Ὰ' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1FBB' /* 'Ά' UppercaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1FBC' /* 'ᾼ' TitlecaseLetter */, '\u0391' /* 'Α' UppercaseLetter */ },
            { '\u1FBE' /* 'ι' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1FC2' /* 'ῂ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1FC3' /* 'ῃ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1FC4' /* 'ῄ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1FC6' /* 'ῆ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1FC7' /* 'ῇ' LowercaseLetter */, '\u03B7' /* 'η' LowercaseLetter */ },
            { '\u1FC8' /* 'Ὲ' UppercaseLetter */, '\u0395' /* 'Ε' UppercaseLetter */ },
            { '\u1FC9' /* 'Έ' UppercaseLetter */, '\u0395' /* 'Ε' UppercaseLetter */ },
            { '\u1FCA' /* 'Ὴ' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1FCB' /* 'Ή' UppercaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1FCC' /* 'ῌ' TitlecaseLetter */, '\u0397' /* 'Η' UppercaseLetter */ },
            { '\u1FD0' /* 'ῐ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1FD1' /* 'ῑ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1FD2' /* 'ῒ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1FD3' /* 'ΐ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1FD6' /* 'ῖ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1FD7' /* 'ῗ' LowercaseLetter */, '\u03B9' /* 'ι' LowercaseLetter */ },
            { '\u1FD8' /* 'Ῐ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1FD9' /* 'Ῑ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1FDA' /* 'Ὶ' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1FDB' /* 'Ί' UppercaseLetter */, '\u0399' /* 'Ι' UppercaseLetter */ },
            { '\u1FE0' /* 'ῠ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1FE1' /* 'ῡ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1FE2' /* 'ῢ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1FE3' /* 'ΰ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1FE4' /* 'ῤ' LowercaseLetter */, '\u03C1' /* 'ρ' LowercaseLetter */ },
            { '\u1FE5' /* 'ῥ' LowercaseLetter */, '\u03C1' /* 'ρ' LowercaseLetter */ },
            { '\u1FE6' /* 'ῦ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1FE7' /* 'ῧ' LowercaseLetter */, '\u03C5' /* 'υ' LowercaseLetter */ },
            { '\u1FE8' /* 'Ῠ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u1FE9' /* 'Ῡ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u1FEA' /* 'Ὺ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u1FEB' /* 'Ύ' UppercaseLetter */, '\u03A5' /* 'Υ' UppercaseLetter */ },
            { '\u1FEC' /* 'Ῥ' UppercaseLetter */, '\u03A1' /* 'Ρ' UppercaseLetter */ },
            { '\u1FF2' /* 'ῲ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FF3' /* 'ῳ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FF4' /* 'ῴ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FF6' /* 'ῶ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FF7' /* 'ῷ' LowercaseLetter */, '\u03C9' /* 'ω' LowercaseLetter */ },
            { '\u1FF8' /* 'Ὸ' UppercaseLetter */, '\u039F' /* 'Ο' UppercaseLetter */ },
            { '\u1FF9' /* 'Ό' UppercaseLetter */, '\u039F' /* 'Ο' UppercaseLetter */ },
            { '\u1FFA' /* 'Ὼ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FFB' /* 'Ώ' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u1FFC' /* 'ῼ' TitlecaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u2000' /* ' ' SpaceSeparator */, '\u2002' /* ' ' SpaceSeparator */ },
            { '\u2001' /* ' ' SpaceSeparator */, '\u2003' /* ' ' SpaceSeparator */ },
            { '\u2126' /* 'Ω' UppercaseLetter */, '\u03A9' /* 'Ω' UppercaseLetter */ },
            { '\u212A' /* 'K' UppercaseLetter */, '\u004B' /* 'K' UppercaseLetter */ },
            { '\u212B' /* 'Å' UppercaseLetter */, '\u0041' /* 'A' UppercaseLetter */ },
            { '\u2329' /* '〈' OpenPunctuation */, '\u3008' /* '〈' OpenPunctuation */ },
            { '\u232A' /* '〉' ClosePunctuation */, '\u3009' /* '〉' ClosePunctuation */ },
            { '\u309E' /* 'ゞ' ModifierLetter */, '\u309D' /* 'ゝ' ModifierLetter */ },
            { '\u30FE' /* 'ヾ' ModifierLetter */, '\u30FD' /* 'ヽ' ModifierLetter */ },
        };

    [Pure]
    [return: NotNullIfNotNull(parameterName: nameof(str))]
    public static string? RemoveDiacritics(this string? str)
    {
        if (str == null)
            return null;

        var normalizedString = str.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

#if NET6_0_OR_GREATER
        if (GlobalizationMode.Invariant)
        {
            // InvariantMode doesn't support string normalization, so many characters are not replaced
            // https://source.dot.net/#System.Private.CoreLib/Normalization.cs,25
            // Let's replace basic characters. The intent is not to support everything but only the most frequent characters
            var dict = DiacriticDictionary.Value;
            for (var i = 0; i < stringBuilder.Length; i++)
            {
                if (dict.TryGetValue(stringBuilder[i], out var value))
                {
                    stringBuilder[i] = value;
                }
            }
        }
#endif

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Removes the trailing occurrence of a specified string from the current string.
    /// </summary>
    [Pure]
    public static string RemoveSuffix(this string str, string suffix) => RemoveSuffix(str, suffix, StringComparison.Ordinal);

    /// <summary>
    /// Removes the trailing occurrence of a specified string from the current string.
    /// </summary>
    [Pure]
    public static string RemoveSuffix(this string str, string suffix, StringComparison stringComparison)
    {
        if (str.EndsWith(suffix, stringComparison))
        {
            return str[..^suffix.Length];
        }

        return str;
    }

    /// <summary>
    /// Removes the leading occurrence of a specified string from the current string.
    /// </summary>
    [Pure]
    public static string RemovePrefix(this string str, string suffix) => RemovePrefix(str, suffix, StringComparison.Ordinal);

    /// <summary>
    /// Removes the leading occurrence of a specified string from the current string.
    /// </summary>
    [Pure]
    public static string RemovePrefix(this string str, string suffix, StringComparison stringComparison)
    {
        if (str.StartsWith(suffix, stringComparison))
        {
            return str[suffix.Length..];
        }

        return str;
    }

    [Pure]
    public static LineSplitEnumerator SplitLines(this string str) => new(str.AsSpan());

    [StructLayout(LayoutKind.Auto)]
    [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
    public ref struct LineSplitEnumerator
    {
        private ReadOnlySpan<char> _str;

        public LineSplitEnumerator(ReadOnlySpan<char> str)
        {
            _str = str;
            Current = default;
        }

        public readonly LineSplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_str.Length == 0)
                return false;

            var span = _str;
            var index = span.IndexOfAny('\r', '\n');
            if (index == -1)
            {
                _str = ReadOnlySpan<char>.Empty;
                Current = new LineSplitEntry(span, ReadOnlySpan<char>.Empty);
                return true;
            }

            if (index < span.Length - 1 && span[index] == '\r')
            {
                var next = span[index + 1];
                if (next == '\n')
                {
                    Current = new LineSplitEntry(span[..index], span.Slice(index, 2));
                    _str = span[(index + 2)..];
                    return true;
                }
            }

            Current = new LineSplitEntry(span[..index], span.Slice(index, 1));
            _str = span[(index + 1)..];
            return true;
        }

        public LineSplitEntry Current { get; private set; }
    }

    [StructLayout(LayoutKind.Auto)]
    [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
    public readonly ref struct LineSplitEntry
    {
        public LineSplitEntry(ReadOnlySpan<char> line, ReadOnlySpan<char> separator)
        {
            Line = line;
            Separator = separator;
        }

        public ReadOnlySpan<char> Line { get; }
        public ReadOnlySpan<char> Separator { get; }

        public void Deconstruct(out ReadOnlySpan<char> line, out ReadOnlySpan<char> separator)
        {
            line = Line;
            separator = Separator;
        }

        public static implicit operator ReadOnlySpan<char>(LineSplitEntry entry) => entry.Line;
    }

    public static int WordCount(this string input) => input.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;


    /// <remarks>
    /// http://weblogs.asp.net/erwingriekspoor/archive/2009/05/01/convert-string-to-byte-array-and-byte-array-to-string.aspx
    /// </remarks>
    /// <summary>
    /// Convert a string into a generic byte array. Use this if you do not know the encoding type.
    /// Otherwise use Encoding.UTF8.GetBytes("foo"), Encoding.ASCII.GetString(bytes);, etc
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static byte[] ToByteArray(this string input)
    {
        byte[] output;

        using (System.IO.MemoryStream stream = new())
        {
            using (System.IO.StreamWriter writer = new(stream))
            {
                writer.Write(input);
                writer.Flush();
            }

            output = stream.ToArray();
        }

        return output;
    }

    /// <summary>
    /// Removes all trailing and leading occurrences of a string from the current System.String.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="trimString"></param>
    /// <returns></returns>
    public static string Trim(this string input, string trimString)
    {
        if (input != null)
        {
            return input.TrimStart(trimString).TrimEnd(trimString);
        }
        else
        {
            return input;
        }
    }

    /// <summary>
    /// Removes all leading occurrences of a string from the current System.String.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="trimString"></param>
    /// <returns></returns>
    public static string TrimStart(this string input, string trimString)
    {
        if (input != null && trimString != null && input.StartsWith(trimString))
        {
            return input.Substring(trimString.Length, input.Length - trimString.Length);
        }
        else
        {
            return input;
        }
    }

    /// <summary>
    /// Removes all trailing occurrences of a string from the current System.String.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="trimString"></param>
    /// <returns></returns>
    public static string TrimEnd(this string input, string trimString)
    {
        if (input != null && trimString != null && input.EndsWith(trimString))
        {
            return input.Substring(0, input.Length - trimString.Length);
        }
        else
        {
            return input;
        }
    }

    /// <summary>
    /// Convert a string list into a dictionary
    /// NOTE: The "pair" separator is [[,]] and the "value" separator is [[=]]
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Dictionary<string, string> ToDictionary(this string input) => ToDictionary(input, "[[,]]", "[[=]]");

    /// <summary>
    /// Convert a string list into a dictionary
    /// </summary>
    /// <param name="input"></param>
    /// <param name="pairSeparator">delimiter between pairs</param>
    /// <param name="valueSeparator">delimeter between values</param>
    /// <returns></returns>
    public static Dictionary<string, string> ToDictionary(this string input, string pairSeparator, string valueSeparator)
    {
        Dictionary<string, string> dictionaryOut = new();

        // Divide all pairs
        string[] pairs = input.Split(new string[] { pairSeparator }, StringSplitOptions.RemoveEmptyEntries);

        // Walk through each item
        foreach (string pair in pairs)
        {
            string? key = null;
            string? value = null;

            // split the pairs
            try
            {
                key = pair.Substring(0, pair.IndexOf(valueSeparator));
                value = pair.Substring(pair.IndexOf(valueSeparator) + valueSeparator.Length);
            }
            catch (Exception)
            {
                // can't find the value separator so use the entire string as the key
                key = pair;
            }

            dictionaryOut.Add(key, value ?? String.Empty);
        }

        return dictionaryOut;
    }

    /// <summary>
    /// Extension wrapper for IsNullOrEmpty.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

    /// <summary>
    /// Extension wrapper for IsNullOrWhitespace.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNullOrWhitespace(this string str) => String.IsNullOrWhiteSpace(str);

    /// <summary>
    /// Take only the right number of characters.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="characters"></param>
    /// <returns></returns>
    public static string? Right(this string str, int characters)
    {
        if (str == null)
            return null;

        if (str.Length <= characters)
            return str;

        return str.Substring(str.Length - characters);
    }

    /// <summary>
    /// Take only the left number of characters.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="characters"></param>
    /// <returns></returns>
    public static string? Left(this string str, int characters)
    {
        if (str == null)
            return null;

        if (str.Length <= characters)
            return str;

        return str.Substring(0, characters);
    }

    /// <summary>
    /// Replace the last occurrence of a string with another string with a different value.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="find"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
    public static string ReplaceLast(this string str, string find, string replace)
    {
        var index = str.LastIndexOf(find);

        if (index == -1)
            return str;

        str = str.Substring(0, index) + replace + str.Substring(index + find.Length);

        return str;
    }

    /// <summary>
    /// Replace the first occurrence of a string with another string with a different value.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="find"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
    public static string ReplaceFirst(this string str, string find, string replace)
    {
        var index = str.IndexOf(find);

        if (index == -1)
            return str;

        str = str.Substring(0, index) + replace + str.Substring(index + find.Length);

        return str;
    }

    /// <summary>
    /// Loops through a list of characterts to remove from a given string
    /// </summary>
    /// <param name="str"></param>
    /// <param name="find"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
    public static string StripCharacters(this string str)
    {
        string charactersToStrip = "',\",&,%,-,],[";
        string[] characters = charactersToStrip.Split(',');

        foreach (string character in characters)
        {
            str = str.Replace(character, "");
        }

        return str;
    }

    public static string StripNonNumericCharacters(this string input) => new string(input.Where(c => char.IsDigit(c)).ToArray());

    /// <summary>
    /// Strips off invalid characters from a given string so that it becomes a valid filename. Note: Don't use full path, or else the path will be stripped!
    /// </summary>
    /// <returns></returns>
    public static string CleanFileName(this string str)
    {
        string cleanString = str;

        // Source: http://stackoverflow.com/a/146162/280342
        string invalid = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
        foreach (char c in invalid)
        {
            cleanString = cleanString.Replace(c.ToString(), "");
        }

        return cleanString;
    }

    public static T? SafeTo<T>(this string str) where T : struct
    {
        try
        {
            T test = (T)Convert.ChangeType(str, typeof(T));

            return test;
        }
        catch
        {
            return null;
        }
    }

    public static bool ToBoolean(this string value, bool defaultValue = false)
    {
        value = value.Trim();

        if (value.ToUpper().In("0", "N", "NO", "F", "FALSE"))
        {
            return false;
        }

        if (value.ToUpper().In("1", "T", "TRUE", "Y", "YES"))
        {
            return true;
        }

        bool result;

        if (!Boolean.TryParse(value, out result))
        {
            return defaultValue;
        }

        return result;
    }

    /// <summary>
    /// Returns a double from a string formatted as a percentage.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static double FromPercentageString(this string value) => double.Parse(value.Substring(0, value.Length - 1)) / 100;

    public static bool IsNumeric(this string value) => Regex.IsMatch(value, @"^\d+$");
}
