# Lateralus.Framework.FullPath

`FullPath` ensures you always deal with full path in your application and provides many common methods to manipulate paths.

````c#
// Create FullPath
FullPath rootPath = FullPath.FromPath("demo"); // It automatically calls Path.GetFullPath to resolve the path
FullPath filePath = FullPath.Combine(rootPath, "temp", "Lateralus.txt"); // Use Path.Combine to join paths (you can combine as many path as you needed)
FullPath temp = FullPath.GetTempPath(); // equivalent of Path.GetTempPath()
FullPath cwd = FullPath.GetCurrentDirectory(); // equivalent of Environment.CurrentDirectory

// Combine path: you can use the / operator to join path
FullPath filePath1 = rootPath / "temp" / "Lateralus.txt";

// Compare path
// Comparisons are case-insensitive on Windows and case-sensitive on other operating systems by default
_ = filePath == rootPath;
_ = filePath.Equals(rootPath, ignoreCase: false);

// Get parent directory
FullPath parent = filePath.Parent;

// Get file/directory name - extension
var name = filePath.Name;
var ext = filePath.Extension;

// Make relative path
string relativePath = filePath.MakeRelativeTo(rootPath); // temp\Lateralus.txt

// Check if a path is under another path
bool isChildOf = filePath.IsChildOf(rootPath);

// FullPath is implicitly converted to string, so it works well with File/Directory methods
System.IO.File.WriteAllText(filePath, content);
````
