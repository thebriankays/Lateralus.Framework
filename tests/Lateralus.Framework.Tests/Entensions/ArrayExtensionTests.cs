namespace Lateralus.Framework.Tests;

public sealed class ArrayExtensionTests
{
    [Fact]
    public void ItFindsAStringMatch()
    {
        var sut = "Dave".IsOneOf(new[] { "Dave" });

        sut.Should().BeTrue();
    }

    [Fact]
    public void ItDoesntFindsAStringMatch()
    {
        var sut = "Dave".IsOneOf(new[] { "Bernard", "Jeremy" });

        sut.Should().BeFalse();
    }

    [Fact]
    public void ItFindsAnIntMatch()
    {
        var sut = 6.IsOneOf(new[] { 1, 2, 3, 4, 5, 6 });

        sut.Should().BeTrue();
    }

    [Fact]
    public void ItDoesntFindAnIntMatch()
    {
        var sut = 6.IsOneOf(new[] { 1, 2, 3, 4, 5 });

        sut.Should().BeFalse();
    }

    [Fact]
    public void ItFindsAFloatMatch()
    {
        var aFloat = 4.53;
        var sut = aFloat.IsOneOf(new[] { 3.45, 4.53, 6.34 });

        sut.Should().BeTrue();
    }

    [Fact]
    public void ItDoesntFindAFloatMatch()
    {
        var aFloat = 4.53;
        var sut = aFloat.IsOneOf(new[] { 3.45, 6.34 });

        sut.Should().BeFalse();
    }

    [Fact]
    public void ItFindsACaseInsensitiveStringMatch()
    {
        var sut = "Dave".IsOneOfCaseInsensitive(new[] { "dave" });

        sut.Should().BeTrue();
    }

    [Fact]
    public void ItDoesntFindsACaseInsensitiveStringMatch()
    {
        var sut = "Dave".IsOneOfCaseInsensitive(new[] { "Bernard", "Jeremy" });

        sut.Should().BeFalse();
    }
}
