#if NET472
using Microsoft.IO;
using Microsoft.IO.Enumeration;
#else
using System.IO.Enumeration;
#endif

namespace Lateralus.Framework.Globbing;

public abstract class GlobCollectionFileSystemEnumerator<T> : FileSystemEnumerator<T>
{
    private readonly GlobCollection _globs;

    protected GlobCollectionFileSystemEnumerator(GlobCollection globs, string directory, EnumerationOptions? options = null)
        : base(directory, options)
    {
        _globs = globs;
    }

    protected override bool ShouldRecurseIntoEntry(ref FileSystemEntry entry)
    {
        return base.ShouldRecurseIntoEntry(ref entry) && _globs.IsPartialMatch(ref entry);
    }

    protected override bool ShouldIncludeEntry(ref FileSystemEntry entry)
    {
        return base.ShouldIncludeEntry(ref entry) && !entry.IsDirectory && _globs.IsMatch(ref entry);
    }
}

public sealed class GlobCollectionFileSystemEnumerator : GlobCollectionFileSystemEnumerator<string>
{
    public GlobCollectionFileSystemEnumerator(GlobCollection globs, string directory, EnumerationOptions? options = null)
        : base(globs, directory, options)
    {
    }

    protected override string TransformEntry(ref FileSystemEntry entry)
    {
        return entry.ToFullPath();
    }
}
