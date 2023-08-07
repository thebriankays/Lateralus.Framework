namespace Lateralus.Framework.Tests;

public class BooleanExtensionsTests
{
    [Theory]
    [InlineData(false, "True", "False", "", "False")]
    [InlineData(true, "True", "False", "", "True")]
    [InlineData(null, "True", "False", "", "")]
    public void NullableBoolean_ToString(bool? value, string trueString, string falseString, string nullString, string expected)
    {
        value.ToString(trueString, falseString, nullString).Should().Be(expected);
    }


    [Theory]
    [InlineData(false, "True", "False", "False")]
    [InlineData(true, "True", "False", "True")]
    public void Boolean_ToString(bool value, string trueString, string falseString, string expected)
    {
        value.ToString(trueString, falseString).Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "No")]
    [InlineData(true, "Yes")]
    public void Boolean_YesorNo(bool value, string expected)
    {
        value.YesOrNo().Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "No")]
    [InlineData(true, "Yes")]
    [InlineData(null, "")]
    public void NullageBoolean_YesorNo(bool? value, string expected)
    {
        value.YesOrNo().Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "N")]
    [InlineData(true, "Y")]
    public void Boolean_YorN(bool value, string expected)
    {
        value.YorN().Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "N")]
    [InlineData(true, "Y")]
    [InlineData(null, "")]
    public void NullableBoolean_YorN(bool? value, string expected)
    {
        value.YorN().Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "False")]
    [InlineData(true, "True")]
    public void Boolean_TrueorFalse(bool value, string expected)
    {
        value.TrueOrFalse().Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "False")]
    [InlineData(true, "True")]
    [InlineData(null, "")]
    public void NullageBoolean_TrueorFalse(bool? value, string expected)
    {
        value.TrueOrFalse().Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "F")]
    [InlineData(true, "T")]
    public void Boolean_TorF(bool value, string expected)
    {
        value.TorF().Should().Be(expected);
    }

    [Theory]
    [InlineData(false, "F")]
    [InlineData(true, "T")]
    [InlineData(null, "")]
    public void NullableBoolean_TorF(bool? value, string expected)
    {
        value.TorF().Should().Be(expected);
    }

    [Theory]
    [InlineData(false, 0)]
    [InlineData(true, 1)]
    public void Boolean_ToBit(bool value, int expected)
    {
        value.ToBit().Should().Be(expected);
    }
}
