namespace Lateralus.Framework;

public static class StackExtensions
{
    public static Stack<T> Clone<T>(this Stack<T> collection)
    {
        Stack<T> stack = new();

        foreach (T item in collection.Reverse())
        {
            stack.Push(item);
        }

        return stack;
    }
}
