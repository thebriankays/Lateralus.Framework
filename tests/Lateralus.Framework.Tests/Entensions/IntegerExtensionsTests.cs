namespace Lateralus.Framework.Tests;

public class DecimalExtensionsTests
{
    [Theory]
    [InlineData(0.1, 0)]
    [InlineData(0.5, 1)]
    [InlineData(0.8, 1)]
    public void DecimalRounded(decimal value, int expected)
    {
        value.Rounded().Should().Be(expected);
    }


    [Theory]
    [InlineData(0.1, MidpointRounding.ToZero,  0)]
    [InlineData(0.1, MidpointRounding.ToNegativeInfinity, 0)]
    [InlineData(0.1, MidpointRounding.ToPositiveInfinity, 0)]
    [InlineData(0.1, MidpointRounding.ToEven, 0)]
    [InlineData(0.1, MidpointRounding.AwayFromZero, 0)]
    public void DecimalRoundedMidpoint(decimal value, MidpointRounding mode, int expected)
    {
        value.Rounded().Should().Be(expected);
    }

    [Theory]
    [InlineData(5, 0)]
    [InlineData(200,200)]
    [InlineData(10000, 10000)]
    public void DecimalRoundedRoundToNearestFiftyDollars(decimal value, int expected)
    {
        value.RoundToNearestFiftyDollars().Should().Be(expected);
    }
}
