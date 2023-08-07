namespace Lateralus.Framework.Tests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("abc", "abc")]
    [InlineData("abcé", "abce")]
    [InlineData("abce\u0301", "abce")]
    public void RemoveDiacritics_Test(string str, string expected)
    {
        var actual = str.RemoveDiacritics();
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(null, null, true)]
    [InlineData("", "", true)]
    [InlineData("abc", "abc", true)]
    [InlineData("abc", "aBc", true)]
    [InlineData("aabc", "abc", false)]
    public void EqualsIgnoreCase(string left, string right, bool expectedResult)
    {
        left.EqualsIgnoreCase(right).Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("", "", true)]
    [InlineData("abc", "abc", true)]
    [InlineData("abc", "aBc", true)]
    [InlineData("aabc", "abc", true)]
    [InlineData("bc", "abc", false)]
    public void ContainsIgnoreCase(string left, string right, bool expectedResult)
    {
        left.ContainsIgnoreCase(right).Should().Be(expectedResult);
    }

    [Fact]
    public void SplitLine_Stop()
    {
        var actual = new List<(string, string)>();
        foreach (var (line, separator) in "a\nb\nc\nd".SplitLines())
        {
            actual.Add((line.ToString(), separator.ToString()));
            if (line.Equals("b", StringComparison.Ordinal))
                break;
        }

        actual.Should().Equal(new[] { ("a", "\n"), ("b", "\n") });
    }

    [Theory]
    [MemberData(nameof(SplitLineData))]
    public void SplitLineSpan(string str, (string Line, string Separator)[] expected)
    {
        var actual = new List<(string, string)>();
        foreach (var (line, separator) in str.SplitLines())
        {
            actual.Add((line.ToString(), separator.ToString()));
        }

        actual.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SplitLineData))]
    public void SplitLineSpan2(string str, (string Line, string Separator)[] expected)
    {
        var actual = new List<string>();
        foreach (ReadOnlySpan<char> line in str.SplitLines())
        {
            actual.Add(line.ToString());
        }

        actual.Should().Equal(expected.Select(item => item.Line).ToArray());
    }

    public static TheoryData<string, (string Line, string Separator)[]> SplitLineData()
    {
        return new TheoryData<string, (string Line, string Separator)[]>
        {
            { "", Array.Empty<(string, string)>() },
            { "ab", new[] { ("ab", "") } },
            { "ab\r\n", new[] { ("ab", "\r\n") } },
            { "ab\r\ncd", new[] { ("ab", "\r\n"), ("cd", "") } },
            { "ab\rcd", new[] { ("ab", "\r"), ("cd", "") } },
            { "ab\ncd", new[] { ("ab", "\n"), ("cd", "") } },
            { "\ncd", new[] { ("", "\n"), ("cd", "") } },
        };
    }

    [Theory]
    [InlineData("", "", StringComparison.Ordinal, "")]
    [InlineData("abc", "c", StringComparison.Ordinal, "ab")]
    [InlineData("abcc", "c", StringComparison.Ordinal, "abc")]
    [InlineData("abcc", "cc", StringComparison.Ordinal, "ab")]
    [InlineData("abcC", "c", StringComparison.Ordinal, "abcC")]
    [InlineData("abC", "c", StringComparison.OrdinalIgnoreCase, "ab")]
    [InlineData("abC", "C", StringComparison.OrdinalIgnoreCase, "ab")]
    [InlineData("abc", "C", StringComparison.OrdinalIgnoreCase, "ab")]
    public void String_RemoveSuffix(string str, string suffx, StringComparison comparison, string expected)
    {
        str.RemoveSuffix(suffx, comparison).Should().Be(expected);
    }

    [Theory]
    [InlineData("", "", StringComparison.Ordinal, "")]
    [InlineData("abc", "a", StringComparison.Ordinal, "bc")]
    [InlineData("aabc", "a", StringComparison.Ordinal, "abc")]
    [InlineData("aabc", "aa", StringComparison.Ordinal, "bc")]
    [InlineData("Aabc", "a", StringComparison.Ordinal, "Aabc")]
    [InlineData("Abc", "a", StringComparison.OrdinalIgnoreCase, "bc")]
    [InlineData("Abc", "A", StringComparison.OrdinalIgnoreCase, "bc")]
    [InlineData("abc", "A", StringComparison.OrdinalIgnoreCase, "bc")]
    public void RemovePrefix(string str, string suffx, StringComparison comparison, string expected)
    {
        str.RemovePrefix(suffx, comparison).Should().Be(expected);
    }

    [Fact]
    public void String_WordCount()
    {
        string str = "Test string";
        str.WordCount().Should().Be(2);
    }

    [Theory]
    [InlineData("", 1, "")]
    [InlineData("Hello World", 5, "World")]
    public void Right(string str, int characters, string expected)
    {
        str.Right(characters).Should().Be(expected);
    }

    [Theory]
    [InlineData("", 1, "")]
    [InlineData("Hello World", 5, "Hello")]
    public void Left(string str, int characters, string expected)
    {
        str.Left(characters).Should().Be(expected);
    }

    [Theory]
    [InlineData("Yum Yum Sauce", "Yum ", "", "Yum Sauce")]
    public void ReplaceLast(string str, string find, string replace, string expected)
    {
        str.ReplaceLast(find, replace).Should().Be(expected);
    }

    [Theory]
    [InlineData("Yum Yum Sauce", "Yum ", "", "Yum Sauce")]
    public void ReplaceFirst(string str, string find, string replace, string expected)
    {
        str.ReplaceFirst(find, replace).Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello World&", "Hello World")]
    public void StripCharacters(string str, string expected)
    {
        str.StripCharacters().Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello World&111", "111")]
    public void StripNonNumericCharacters(string str, string expected)
    {
        str.StripNonNumericCharacters().Should().Be(expected);
    }

    [Theory]
    [InlineData("Yes", false, true)]
    [InlineData("No", false, false)]
    [InlineData("Y", false, true)]
    [InlineData("N", false, false)]
    [InlineData("True", false, true)]
    [InlineData("False", false, false)]
    [InlineData("T", false, true)]
    [InlineData("F", false, false)]
    public void ToBoolean(string str, bool defaultValue, bool expected)
    {
        str.ToBoolean(defaultValue).Should().Be(expected);
    }

    [Theory]
    [InlineData("30", true)]
    [InlineData("", false)]
    [InlineData("Hello World", false)]
    public void IsNumeric(string str, bool expected)
    {
        str.IsNumeric().Should().Be(expected);
    }
}
