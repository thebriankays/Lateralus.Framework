namespace Lateralus.Framework.Globbing;

[Flags]
public enum GlobOptions
{
    None = 0,
    IgnoreCase = 0x1,

    /// <summary>Use gitignore patterns</summary>
    Git = 0x2,
}
