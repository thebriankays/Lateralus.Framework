using System.IO.Enumeration;

namespace Lateralus.Framework.DependencyScanning;

public delegate bool FileSystemEntryPredicate(ref FileSystemEntry entry);
