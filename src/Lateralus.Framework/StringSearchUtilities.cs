namespace Lateralus.Framework;

public static class StringSearchUtilities
{
    /// <summary>
    /// Compute the Hamming distance. http://en.wikipedia.org/wiki/Hamming_distance
    /// </summary>
    /// <param name="word1"> The first word.</param>
    /// <param name="word2"> The second word.</param>
    /// <returns> The hamming distance.</returns>
    [Pure]
    public static uint Hamming(uint word1, uint word2)
    {
        uint result = 0;
        while (word1 != 0 || word2 != 0)
        {
            var u = (word1 & 1) ^ (word2 & 1);
            result += u;
            word1 = (word1 >> 1) & 0x7FFFFFFF;
            word2 = (word2 >> 1) & 0x7FFFFFFF;
        }

        return result;
    }

    /// <summary>
    /// Compute the Hamming distance. http://en.wikipedia.org/wiki/Hamming_distance
    /// </summary>
    /// <param name="word1">The first word.</param>
    /// <param name="word2">The second word.</param>
    /// <exception cref="ArgumentException">Lists must have the same length.</exception>
    /// <returns> The hamming distance.</returns>
    [Pure]
    public static int Hamming(string word1, string word2)
    {
        ArgumentNullException.ThrowIfNull(word1);
        ArgumentNullException.ThrowIfNull(word2);

        if (word1.Length != word2.Length)
            throw new ArgumentException("Strings must have the same length.", nameof(word2));

        var result = 0;
        for (var i = 0; i < word1.Length; i++)
        {
            if (word1[i] != word2[i])
            {
                result++;
            }
        }

        return result;
    }

    /// <summary>
    /// Compute the Hamming distance. http://en.wikipedia.org/wiki/Hamming_distance
    /// </summary>
    /// <typeparam name="T">Type of elements.</typeparam>
    /// <param name="word1">The first list.</param>
    /// <param name="word2">The second most.</param>
    /// <exception cref="ArgumentException">Lists must have the same length.</exception>
    /// <returns> The hamming distance.</returns>
    [Pure]
    public static int Hamming<T>(IEnumerable<T> word1, IEnumerable<T> word2)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(word1);
        ArgumentNullException.ThrowIfNull(word2);

        var result = 0;

        using var enumerator1 = word1.GetEnumerator();
        using var enumerator2 = word2.GetEnumerator();
        bool firstMoveNext;
        var secondMoveNext = false;

        while ((firstMoveNext = enumerator1.MoveNext()) && (secondMoveNext = enumerator2.MoveNext()))
        {
            if (!enumerator1.Current.Equals(enumerator2.Current))
            {
                result++;
            }
        }

        if (firstMoveNext != secondMoveNext)
            throw new ArgumentException("Lists must have the same length.", nameof(word2));

        return result;
    }

    /// <summary>
    /// Compute the Levenshtein distance. http://en.wikipedia.org/wiki/Levenshtein_distance
    /// </summary>
    /// <param name="word1"> The first word.</param>
    /// <param name="word2"> The second word.</param>
    /// <returns> The Levenshtein distance.</returns>
    [Pure]
    public static int Levenshtein(string word1, string word2)
    {
        ArgumentNullException.ThrowIfNull(word1);
        ArgumentNullException.ThrowIfNull(word2);

        if (word1.Length == 0)
        {
            return word2.Length;
        }

        if (word2.Length == 0)
        {
            return word1.Length;
        }

        var lastColumn = new List<int>(word2.Length);
        for (var i = 1; i <= word2.Length; i++)
        {
            lastColumn.Add(i);
        }

        var lastValue = 0;
        for (var j = 1; j <= word1.Length; j++)
        {
            for (var i = 0; i < word2.Length; i++)
            {
                var x = (i == 0 ? j : lastValue) + 1;
                var y = lastColumn[i] + 1;
                var z = (i == 0 ? j - 1 : lastColumn[i - 1]) + (word1[j - 1] == word2[i] ? 0 : 1);

                var forLastValue = lastValue;
                lastValue = Math.Min(Math.Min(x, y), z);
                if (i > 0)
                {
                    lastColumn[i - 1] = forLastValue;
                }

                if (i == word2.Length - 1)
                {
                    lastColumn[i] = lastValue;
                }
            }
        }

        return lastValue;
    }

    [Pure]
    public static string Metaphone(string s)
    {
        ArgumentNullException.ThrowIfNull(s);

        const string Vowels = "AEIOU";
        const string Frontv = "EIY";
        const string Varson = "CSPTG";
        const int MaxCodeLen = 4;

        if (s.Length == 0)
            return string.Empty;

        if (s.Length == 1)
            return s.ToUpperInvariant();

        var inwd = s.ToUpperInvariant().ToCharArray();
        var local = new StringBuilder(40); // manipulate
        var code = new StringBuilder(10); // output

        // handle initial 2 characters exceptions
        switch (inwd[0])
        {
            case 'K':
            case 'G':
            case 'P': /* looking for KN, etc*/
                if (inwd[1] == 'N')
                {
                    local.Append(inwd, 1, inwd.Length - 1);
                }
                else
                {
                    local.Append(inwd);
                }

                break;
            case 'A': /* looking for AE */
                if (inwd[1] == 'E')
                {
                    local.Append(inwd, 1, inwd.Length - 1);
                }
                else
                {
                    local.Append(inwd);
                }

                break;
            case 'W': /* looking for WR or WH */
                if (inwd[1] == 'R')
                {
                    // WR -> R
                    local.Append(inwd, 1, inwd.Length - 1);
                    break;
                }

                if (inwd[1] == 'H')
                {
                    local.Append(inwd, 1, inwd.Length - 1);
                    local[0] = 'W'; // WH -> W
                }
                else
                {
                    local.Append(inwd);
                }

                break;
            case 'X': /* initial X becomes S */
                inwd[0] = 'S';
                local.Append(inwd);
                break;
            default:
                local.Append(inwd);
                break;
        }

        // now local has working string with initials fixed
        var wdsz = local.Length;
        var mtsz = 0;
        var n = 0;
        while ((mtsz < MaxCodeLen) && // max code size of 4 works well
               (n < wdsz))
        {
            var symb = local[n];

            // remove duplicate letters except C
            if ((symb != 'C') && (n > 0) && (local[n - 1] == symb))
            {
                n++;
            }
            else
            {
                // not dup
                string tmpS;
                switch (symb)
                {
                    case 'A':
                    case 'E':
                    case 'I':
                    case 'O':
                    case 'U':
                        if (n == 0)
                        {
                            code.Append(symb);
                            mtsz++;
                        }

                        break; // only use vowel if leading char
                    case 'B':
                        if ((n > 0) && n == wdsz - 1 && (local[n - 1] == 'M'))
                        {
                            break;
                        }

                        code.Append(symb);
                        mtsz++;
                        break;

                    case 'C': // lots of C special cases
                        /* discard if SCI, SCE or SCY */
                        if ((n > 0) && (local[n - 1] == 'S') && (n + 1 < wdsz)
                            && (Frontv.Contains(local[n + 1], StringComparison.Ordinal)))
                        {
                            break;
                        }

                        tmpS = local.ToString();
                        Contract.Assume(local.Length == tmpS.Length);
                        if (tmpS.IndexOf("CIA", n, StringComparison.Ordinal) == n)
                        {
                            // "CIA" -> X
                            code.Append('X');
                            mtsz++;
                            break;
                        }

                        if ((n + 1 < wdsz) && (Frontv.Contains(local[n + 1], StringComparison.Ordinal)))
                        {
                            code.Append('S');
                            mtsz++;
                            break; // CI,CE,CY -> S
                        }

                        if ((n > 0) && (tmpS.IndexOf("SCH", n - 1, StringComparison.Ordinal) == n - 1))
                        {
                            // SCH->sk
                            code.Append('K');
                            mtsz++;
                            break;
                        }

                        if (tmpS.IndexOf("CH", n, StringComparison.Ordinal) == n)
                        {
                            // detect CH
                            if ((n == 0) && (wdsz >= 3) && // CH consonant -> K consonant
                                !Vowels.Contains(local[2], StringComparison.Ordinal))
                            {
                                code.Append('K');
                            }
                            else
                            {
                                code.Append('X'); // CHvowel -> X
                            }

                            mtsz++;
                        }
                        else
                        {
                            code.Append('K');
                            mtsz++;
                        }

                        break;

                    case 'D':
                        if ((n + 2 < wdsz) && // DGE DGI DGY -> J
                            (local[n + 1] == 'G') && (Frontv.Contains(local[n + 2], StringComparison.Ordinal)))
                        {
                            code.Append('J');
                            n += 2;
                        }
                        else
                        {
                            code.Append('T');
                        }

                        mtsz++;
                        break;

                    case 'G': // GH silent at end or before consonant
                        if ((n + 2 == wdsz) && (local[n + 1] == 'H'))
                            break;

                        if ((n + 2 < wdsz) && (local[n + 1] == 'H') && !Vowels.Contains(local[n + 2], StringComparison.Ordinal))
                            break;

                        tmpS = local.ToString();
                        if (n > 0 && (tmpS.IndexOf("GN", n, StringComparison.Ordinal) == n || tmpS.IndexOf("GNED", n, StringComparison.Ordinal) == n))
                            break; // silent G

                        // bool hard = false;
                        // if ((n > 0) &&
                        // (local[n - 1] == 'G')) hard = true;//totest
                        // else hard = false;
                        if ((n + 1 < wdsz) && Frontv.Contains(local[n + 1], StringComparison.Ordinal) /*&& !hard*/)
                        {
                            code.Append('J');
                        }
                        else
                        {
                            code.Append('K');
                        }

                        mtsz++;
                        break;

                    case 'H':
                        if (n + 1 == wdsz)
                            break; // terminal H

                        if (n > 0 && Varson.Contains(local[n - 1], StringComparison.Ordinal))
                            break;

                        if (Vowels.Contains(local[n + 1], StringComparison.Ordinal))
                        {
                            code.Append('H');
                            mtsz++; // Hvowel
                        }

                        break;

                    case 'F':
                    case 'J':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'R':
                        code.Append(symb);
                        mtsz++;
                        break;

                    case 'K':
                        if (n > 0)
                        {
                            // not initial
                            if (local[n - 1] != 'C')
                            {
                                code.Append(symb);
                            }
                        }
                        else
                        {
                            code.Append(symb); // initial K
                        }

                        mtsz++;
                        break;

                    case 'P':
                        // PH -> F
                        if ((n + 1 < wdsz) && (local[n + 1] == 'H'))
                        {
                            code.Append('F');
                        }
                        else
                        {
                            code.Append(symb);
                        }

                        mtsz++;
                        break;

                    case 'Q':
                        code.Append('K');
                        mtsz++;
                        break;

                    case 'S':
                        tmpS = local.ToString();
                        Contract.Assume(tmpS.Length == local.Length);
                        if ((tmpS.IndexOf("SH", n, StringComparison.Ordinal) == n) || (tmpS.IndexOf("SIO", n, StringComparison.Ordinal) == n)
                            || (tmpS.IndexOf("SIA", n, StringComparison.Ordinal) == n))
                        {
                            code.Append('X');
                        }
                        else
                        {
                            code.Append('S');
                        }

                        mtsz++;
                        break;

                    case 'T':
                        tmpS = local.ToString(); // TIA TIO -> X
                        Contract.Assume(tmpS.Length == local.Length);
                        if ((tmpS.IndexOf("TIA", n, StringComparison.Ordinal) == n) || (tmpS.IndexOf("TIO", n, StringComparison.Ordinal) == n))
                        {
                            code.Append('X');
                            mtsz++;
                            break;
                        }

                        if (tmpS.IndexOf("TCH", n, StringComparison.Ordinal) == n)
                            break;

                        // substitute numeral 0 for TH (resembles theta after all)
                        code.Append(tmpS.IndexOf("TH", n, StringComparison.Ordinal) == n ? '0' : 'T');
                        mtsz++;
                        break;

                    case 'V':
                        code.Append('F');
                        mtsz++;
                        break;

                    case 'W':
                    case 'Y': // silent if not followed by vowel
                        if ((n + 1 < wdsz) && Vowels.Contains(local[n + 1], StringComparison.Ordinal))
                        {
                            code.Append(symb);
                            mtsz++;
                        }

                        break;

                    case 'X':
                        code.Append('K');
                        code.Append('S');
                        mtsz += 2;
                        break;

                    case 'Z':
                        code.Append('S');
                        mtsz++;
                        break;
                }

                // end switch
                n++;
            }

            // end else from symb != 'C'
            if (mtsz > 4)
            {
                code.Length = 4;
            }
        }

        return code.ToString();
    }

    /// <summary>
    /// Compute the soundex of a string.
    /// </summary>
    /// <param name="s"> The string.</param>
    /// <param name="dic"> Dictionary containing value of characters.</param>
    /// <param name="replace"> List of replacement to do before computing the soundex.</param>
    /// <returns> The soundex.</returns>
    /// <exception cref="ArgumentException">Dictionary does not contain character a character of the string <paramref name="s" /></exception>
    [Pure]
    public static string Soundex(string s, IReadOnlyDictionary<char, byte> dic, IReadOnlyDictionary<string, char>? replace = null)
    {
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(dic);

        s = SoundexStringPrep(s, replace);
        if (s.Length == 0)
        {
            return "0000";
        }

        var sb = s[0].ToString(CultureInfo.InvariantCulture);

        var oldPos = true;
        if (!dic.TryGetValue(s[0], out var oldCode))
        {
            // throw new ArgumentException(string.Format("Dictionary does not contain character '{0}'", s[0]), "dic");
            oldCode = 0;
        }

        var i = 1;
        while (sb.Length < 4 && i < s.Length)
        {
            if (dic.TryGetValue(s[i], out var value))
            {
                if (value > 0)
                {
                    if (value != oldCode)
                    {
                        sb += value;
                        oldPos = true;
                    }
                    else
                    {
                        if (!oldPos)
                        {
                            sb += value;
                            oldPos = true;
                        }
                    }
                }
            }
            else
            {
                oldPos = oldPos && (s[i] == 'H' || s[i] == 'W');
            }

            if (value > 0)
            {
                oldCode = value;
            }

            i++;
        }

        // Soundex must be four character long
        while (sb.Length < 4)
        {
            sb += '0';
        }

        return sb;
    }

    /// <summary>
    ///     Compute an improved French soundex.
    /// </summary>
    /// <param name="s"> The string. </param>
    /// <returns> The soundex. </returns>
    [Pure]
    public static string Soundex2(string s)
    {
        ArgumentNullException.ThrowIfNull(s);

        var sb = new StringBuilder();
        foreach (var t in s.TrimStart().ToUpperInvariant().RemoveDiacritics())
        {
            if (t != ' ')
            {
                sb.Append(t);
            }
            else
            {
                break;
            }
        }

        if (sb.Length == 0)
        {
            return "    ";
        }

        if (sb.Length == 1)
        {
            sb.Append("   ");
            return sb.ToString();
        }

        sb = sb.Replace("GUI", "KI");
        sb = sb.Replace("GUE", "KE");
        sb = sb.Replace("GA", "KA");
        sb = sb.Replace("GO", "KO");
        sb = sb.Replace("GU", "K");
        sb = sb.Replace("CA", "KA");
        sb = sb.Replace("CO", "KO");
        sb = sb.Replace("CU", "KU");
        sb = sb.Replace("Q", "K");
        sb = sb.Replace("CC", "K");
        sb = sb.Replace("CK", "K");

        // Replace E, I, O, U by A
        for (var i = 1; i < sb.Length; i++)
        {
            switch (sb[i])
            {
                case 'E':
                case 'I':
                case 'O':
                case 'U':
                    sb[i] = 'A';
                    break;
            }
        }

        ChangePrefix(sb, "MAC", "MCC");
        ChangePrefix(sb, "ASA", "AZA");
        ChangePrefix(sb, "KN", "NN");
        ChangePrefix(sb, "PF", "FF");
        ChangePrefix(sb, "SCH", "SSS");
        ChangePrefix(sb, "PH", "FF");

        // Remove H except if the previous letter is a C or an S
        var cs = false;
        for (var i = 0; i < sb.Length; i++)
        {
            if (sb[i] == 'H' && !cs)
            {
                sb = sb.Remove(i, 1);
                i = i == 0 ? 0 : i - 1;
            }

            if (sb.Length > i)
            {
                cs = "CS".Contains(sb[i], StringComparison.Ordinal);
            }
        }

        // Remove Y except if the previous letter is an A
        var a = false;
        for (var i = 0; i < sb.Length; i++)
        {
            if (sb[i] == 'Y' && !a)
            {
                sb = sb.Remove(i, 1);
                i = i == 0 ? 0 : i - 1;
            }

            if (sb.Length > i)
            {
                a = sb[i] == 'A';
            }
        }

        // Remove the last character if it's an A or a T or a D or an S
        if (sb.Length > 0 && "ATDS".Contains(sb[^1], StringComparison.Ordinal))
        {
            sb = sb.Remove(sb.Length - 1, 1);
        }

        // Remove all A except if the A is the first letter
        for (var i = 1; i < sb.Length; i++)
        {
            if (sb[i] == 'A')
            {
                sb = sb.Remove(i, 1);
                i--;
            }
        }

        // Remove substring composed of repeated letters
        for (var i = 0; i < sb.Length - 1; i++)
        {
            if (sb[i] == sb[i + 1])
            {
                sb = sb.Remove(i, 1);
                i--;
            }
        }

        // The soundex must be four character long.
        while (sb.Length < 4)
        {
            sb.Append(' ');
        }

        return sb.ToString(0, 4);
    }

    /// <summary>
    ///     Compute English soundex.
    /// </summary>
    /// <param name="s"> The string. </param>
    /// <returns> The soundex. </returns>
    [Pure]
    public static string SoundexEnglish(string s)
    {
        var dic = new Dictionary<char, byte>
            {
                {'B', 1},
                {'F', 1},
                {'P', 1},
                {'V', 1},
                {'C', 2},
                {'G', 2},
                {'J', 2},
                {'K', 2},
                {'Q', 2},
                {'S', 2},
                {'X', 2},
                {'Z', 2},
                {'D', 3},
                {'T', 3},
                {'L', 4},
                {'M', 5},
                {'N', 5},
                {'R', 6},
            };

        return Soundex(s, dic);
    }

    /// <summary>
    ///     Compute French soundex.
    /// </summary>
    /// <param name="s"> The string. </param>
    /// <returns> The soundex. </returns>
    [Pure]
    public static string SoundexFrench(string s)
    {
        var dic = new Dictionary<char, byte>
            {
                {'B', 1},
                {'P', 1},
                {'C', 2},
                {'K', 2},
                {'Q', 2},
                {'D', 3},
                {'T', 3},
                {'L', 4},
                {'M', 5},
                {'N', 5},
                {'R', 6},
                {'G', 7},
                {'J', 7},
                {'S', 8},
                {'X', 8},
                {'Z', 8},
                {'F', 9},
                {'V', 9},
            };

        return Soundex(s, dic);
    }

    private static void ChangePrefix(StringBuilder sb, string prefix, string replace)
    {
        var i = 0;
        while (i < sb.Length && i < prefix.Length)
        {
            if (sb[i] != prefix[i])
                return;

            i++;
        }

        if (!sb.StartsWith(prefix))
            return;

        sb.Replace(prefix, replace, 0, 1);
    }

    [Pure]
    private static string SoundexStringPrep(string s, IReadOnlyDictionary<string, char>? replace = null)
    {

        // takes only the first word of the string.
        var sb = new StringBuilder();
        foreach (var t in s.TrimStart().ToUpperInvariant().RemoveDiacritics())
        {
            if (char.IsWhiteSpace(t))
                break; // Exit after the first space

            if (char.IsLetter(t))
            {
                sb.Append(t);
            }
        }

        // Return empty string if string is empty
        if (sb.Length == 0)
            return string.Empty;

        // Replace characters
        if (replace != null)
        {
            foreach (var pair in replace)
            {
                sb = sb.Replace(pair.Key, pair.Value.ToString(CultureInfo.InvariantCulture));
            }
        }

        return sb.ToString();
    }
}
