namespace Lateralus.Framework.Tests;

public class ListExtensionsTests
{
    [Fact]
    public void List_Prepend()
    {
        var newItem = new Utility.LookupItem { Name = "", Value = null };
        var newList = Utility.EnumToList<FactOperatingSystem>().Prepend(() => newItem);
        newList[0].Should().Be(newItem);
    }
}
