namespace Lateralus.Framework.Tests;

public class StackExtensionsTests
{
    [Fact]
    public void Stack_Clone()
    {
        Stack<int> stack = new();
        stack.Push(1);
        var clone = stack.Clone();
        clone.Should().Equal(stack);
    }
}
