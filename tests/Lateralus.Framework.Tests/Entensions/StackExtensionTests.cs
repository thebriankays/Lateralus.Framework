namespace Lateralus.Framework.Tests;

public class EnumExtensionsTests
{
    [Fact]
    public void Enum_GetEnumDescription()
    {
        FactOperatingSystem.Windows.GetDescription().Should().Be("Windows");
    }
}
