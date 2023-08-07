namespace Lateralus.Framework.Tests;

public class DictionaryExtensionsTests
{
    [Fact]
    public void Dictionary_TryGetValueOrNull()
    {

        var dictionary = new Dictionary<object, object>() { {1, "Test1" }, {2, "Test2" } };
        dictionary.TryGetValueOrNull(1).Should().Be("Test1");
        dictionary.TryGetValueOrNull(3).Should().Be(null);
    }
}
