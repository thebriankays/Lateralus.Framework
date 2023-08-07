namespace Lateralus.Framework.Tests;

public sealed class CountryTests
{
    [Theory]
    [InlineData("fr")]
    [InlineData("FR")]
    [InlineData("fR")]
    [InlineData("Fr")]
    public void FrenchFlag(string name)
    {
        Country.GetUnicodeFlag(name).Should().Be("\U0001F1EB\U0001F1F7");
        Country.GetUnicodeFlag(name).Should().Be("\U0001F1EB\U0001F1F7");
    }

    [RunIfFact(globalizationMode: FactInvariantGlobalizationMode.Disabled)]
    public void FrenchFlagFromRegion()
    {
        Country.GetUnicodeFlag(new RegionInfo("FR")).Should().Be("\U0001F1EB\U0001F1F7");
    }
}
