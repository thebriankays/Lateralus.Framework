namespace Lateralus.Framework;


public static class IOUtilities
{
    private static readonly string[] ReservedFileNames = new[]
    {
        "con", "prn", "aux", "nul",
        "com0", "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9",
        "lpt0", "lpt1", "lpt2", "lpt3", "lpt4", "lpt5", "lpt6", "lpt7", "lpt8", "lpt9",
    };

    /// <summary>
    /// Makes sure a directory exists for a given file path.
    /// </summary>
    /// <param name="filePath">The file path. Note this is not to be confused with the directory path. May not be null.</param>
    public static void PathCreateDirectory(string filePath)
    {
        if (filePath is null)
            throw new ArgumentNullException(nameof(filePath));

        if (!Path.IsPathRooted(filePath))
        {
            filePath = Path.GetFullPath(filePath);
        }

        var dir = Path.GetDirectoryName(filePath);
        if (dir == null)
            return;

        Directory.CreateDirectory(dir);
    }

    /// <summary>
    /// Unprotects the given file path.
    /// </summary>
    /// <param name="path">The file path. May not be null.</param>
    public static void PathUnprotect(string path)
    {
        if (path is null)
            throw new ArgumentNullException(nameof(path));

        var fi = new FileInfo(path);
        if (fi.Exists)
        {
            if (fi.IsReadOnly)
            {
                fi.IsReadOnly = false;
            }
        }
    }

    [Obsolete("Use FullPath struct instead")]
    public static bool ArePathEqual(string path1, string path2)
    {
        if (path1 is null)
            throw new ArgumentNullException(nameof(path1));
        if (path2 is null)
            throw new ArgumentNullException(nameof(path2));

        var uri1 = new Uri(path1);
        var uri2 = new Uri(path2);

        return Uri.Compare(uri1, uri2, UriComponents.AbsoluteUri, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) == 0;
    }

    [Obsolete("Use FullPath struct instead")]
    public static bool IsChildPathOf(string parent, string child)
    {
        if (parent is null)
            throw new ArgumentNullException(nameof(parent));
        if (child is null)
            throw new ArgumentNullException(nameof(child));

        var parentUri = new Uri(parent);
        var childUri = new Uri(child);

        return parentUri.IsBaseOf(childUri);
    }

    [Obsolete("Use FullPath struct instead")]
    public static string MakeRelativePath(string root, string path)
    {
        if (root is null)
            throw new ArgumentNullException(nameof(root));
        if (path is null)
            throw new ArgumentNullException(nameof(path));

        var parentUri = new Uri(root);
        var childUri = new Uri(path);

        var relativeUri = parentUri.MakeRelativeUri(childUri).ToString();
        return relativeUri.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
    }

    /// <summary>
    /// Converts a text into a valid file name.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="reservedNameFormat">The reserved format to use for reserved names. If null '_{0}_' will be used.</param>
    /// <param name="reservedCharFormat">The reserved format to use for reserved characters. If null '_x{0}_' will be used.</param>
    /// <returns>
    /// A valid file name.
    /// </returns>
    public static string ToValidFileName(string fileName, string reservedNameFormat = "_{0}_", string reservedCharFormat = "_x{0}_")
    {
        if (fileName is null)
            throw new ArgumentNullException(nameof(fileName));

        if (reservedNameFormat is null)
            throw new ArgumentNullException(nameof(reservedNameFormat));

        if (reservedCharFormat is null)
            throw new ArgumentNullException(nameof(reservedCharFormat));

        if (ReservedFileNames.ContainsIgnoreCase(fileName) || IsAllDots(fileName))
        {
            return string.Format(CultureInfo.InvariantCulture, reservedNameFormat, fileName);
        }

        var invalid = Path.GetInvalidFileNameChars();

        var sb = new StringBuilder(fileName.Length);
        foreach (var c in fileName)
        {
            if (Array.IndexOf(invalid, c) >= 0)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, reservedCharFormat, (short)c);
            }
            else
            {
                sb.Append(c);
            }
        }

        var s = sb.ToString();
        if (string.Equals(s, fileName, StringComparison.Ordinal))
        {
            s = fileName;
        }

        return s;
    }

    private static bool IsAllDots(string fileName)
    {
        foreach (var c in fileName)
        {
            if (c != '.')
                return false;
        }
        return true;
    }

    public static void CopyDirectory(string sourcePath, string destinationPath)
    {
        if (sourcePath is null)
            throw new ArgumentNullException(nameof(sourcePath));

        if (destinationPath is null)
            throw new ArgumentNullException(nameof(destinationPath));

        // Get the subdirectories for the specified directory.
        var dir = new DirectoryInfo(sourcePath);
        if (!dir.Exists)
            throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourcePath);

        var dirs = dir.GetDirectories();
        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }

        // Get the files in the directory and copy them to the new location.
        var files = dir.GetFiles();
        foreach (var file in files)
        {
            var temppath = Path.Combine(destinationPath, file.Name);
            file.CopyTo(temppath, overwrite: false);
        }

        // Copy subdirectories
        foreach (var subdir in dirs)
        {
            var tempPath = Path.Combine(destinationPath, subdir.Name);
            CopyDirectory(subdir.FullName, tempPath);
        }
    }

    public static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
    {
        if (!source.Exists)
            throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + source);

        destination.Create();

        // Get the files in the directory and copy them to the new location.
        var files = source.GetFiles();
        foreach (var file in files)
        {
            var temppath = Path.Combine(destination.FullName, file.Name);
            file.CopyTo(temppath, overwrite: false);
        }

        // Copy subdirectories
        var dirs = source.GetDirectories();
        foreach (var subdir in dirs)
        {
            var temppath = new DirectoryInfo(Path.Combine(destination.FullName, subdir.Name));
            CopyDirectory(subdir, temppath);
        }
    }

    public static bool IsSharingViolation(IOException exception)
    {
        if (exception is null)
            throw new ArgumentNullException(nameof(exception));

        var hr = exception.HResult;
        return hr == -2147024864; // 0x80070020 ERROR_SHARING_VIOLATION
    }

    public static void Delete(string path)
    {
        var di = new DirectoryInfo(path);
        if (di.Exists)
        {
            Delete(di);
            return;
        }

        var fi = new FileInfo(path);
        if (fi.Exists)
        {
            Delete(fi);
        }
    }

    public static void Delete(FileSystemInfo fileSystemInfo)
    {
        if (!fileSystemInfo.Exists)
            return;

        try
        {
            if (fileSystemInfo is DirectoryInfo directoryInfo)
            {
                foreach (var childInfo in directoryInfo.GetFileSystemInfos())
                {
                    if (childInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        try
                        {
                            RetryOnSharingViolation(() => RemoveReadOnlyAttribute(childInfo));
                            RetryOnSharingViolation(() => childInfo.Delete());
                        }
                        catch (FileNotFoundException)
                        {
                        }
                        catch (DirectoryNotFoundException)
                        {
                        }
                    }
                    else
                    {
                        Delete(childInfo);
                    }
                }
            }

            RetryOnSharingViolation(() => RemoveReadOnlyAttribute(fileSystemInfo));
            RetryOnSharingViolation(() => DeleteFileSystemInfo(fileSystemInfo));
        }
        catch (FileNotFoundException)
        {
        }
        catch (DirectoryNotFoundException)
        {
        }
    }

    private static void RetryOnSharingViolation(Action action)
    {
        var attempt = 0;
        while (attempt < 10)
        {
            try
            {
                action();
                return;
            }
            catch (IOException ex) when (IsSharingViolation(ex))
            {
            }

            attempt++;
            Thread.Sleep(50);
        }
    }

    private static void RemoveReadOnlyAttribute(FileSystemInfo fileSystemInfo)
    {
        var newAttributes = fileSystemInfo.Attributes & ~FileAttributes.ReadOnly;
        if (fileSystemInfo.Attributes != newAttributes)
        {
            fileSystemInfo.Attributes = newAttributes;
        }
    }

    private static void DeleteFileSystemInfo(FileSystemInfo fsi)
    {
        if (fsi is DirectoryInfo di)
        {
            di.Delete(recursive: true);
        }
        else
        {
            fsi.Delete();
        }
    }

    public static ValueTask DeleteAsync(string path, CancellationToken cancellationToken = default)
    {
        var di = new DirectoryInfo(path);
        if (di.Exists)
            return DeleteAsync(di, cancellationToken);

        var fi = new FileInfo(path);
        if (fi.Exists)
            return DeleteAsync(fi, cancellationToken);

        return default;
    }

    public static async ValueTask DeleteAsync(FileSystemInfo fileSystemInfo, CancellationToken cancellationToken = default)
    {
        if (!fileSystemInfo.Exists)
            return;

        if (fileSystemInfo is DirectoryInfo directoryInfo)
        {
            foreach (var childInfo in directoryInfo.GetFileSystemInfos())
            {
                if (childInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    try
                    {
                        await RetryOnSharingViolationAsync(() => childInfo.Delete(), cancellationToken).ConfigureAwait(false);
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                }
                else
                {
                    await DeleteAsync(childInfo, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        try
        {
            await RetryOnSharingViolationAsync(() => RemoveReadOnlyAttribute(fileSystemInfo), cancellationToken).ConfigureAwait(false);
            await RetryOnSharingViolationAsync(() => DeleteFileSystemInfo(fileSystemInfo), cancellationToken).ConfigureAwait(false);
        }
        catch (FileNotFoundException)
        {
        }
        catch (DirectoryNotFoundException)
        {
        }
    }

    private static async ValueTask RetryOnSharingViolationAsync(Action action, CancellationToken cancellationToken)
    {
        var attempt = 0;
        while (attempt < 10)
        {
            try
            {
                action();
                return;
            }
            catch (IOException ex) when (IsSharingViolation(ex))
            {
            }

            attempt++;
            await Task.Delay(50, cancellationToken).ConfigureAwait(false);
        }
    }


}
