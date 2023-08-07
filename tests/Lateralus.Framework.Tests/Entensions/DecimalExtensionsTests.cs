namespace Lateralus.Framework.Tests;

public class IntegerExtensionsTests
{
    [Theory]
    [InlineData(1, 0, "1")]
    [InlineData(10, 4, "0010")]
    public void IntegerToPaddedStringWithSign(int value, int length, string expected)
    {
        value.ToPaddedStringWithSign(length).Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 0, ' ', "1")]
    [InlineData(10, 4, '0', "0010")]
    public void IntegerToPaddedStringWithSignPadded(int value, int length, char paddingChar, string expected)
    {
        value.ToPaddedStringWithSign(length, paddingChar).Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 0, 5, true)]
    [InlineData(10, 4, 5, false)]
    public void IntegerBetween(int value, int lower, int upper, bool expected)
    {
        value.Between(lower, upper).Should().Be(expected);
    }
}
