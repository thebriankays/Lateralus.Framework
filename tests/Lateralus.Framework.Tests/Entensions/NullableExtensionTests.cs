namespace Lateralus.Framework.Tests;

public sealed class NullableExtensionTests
{

    [Fact]
    public void ItAssertsNullValuesReturnTrueWhenIsNullExtensionIsUsed()
    {
        object nullVar = null; ;
        nullVar.IsNull().Should().BeTrue();
    }
}
