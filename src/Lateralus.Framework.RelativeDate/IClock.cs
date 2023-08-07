namespace Lateralus.Framework;

public interface IClock
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
