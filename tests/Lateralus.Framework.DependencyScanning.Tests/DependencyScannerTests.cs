namespace Lateralus.Framework.DependencyScanning.Tests;

public sealed class DependencyScannerTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DependencyScannerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task ReportScanException(int degreeOfParallelism)
    {
        await using var directory = TemporaryDirectory.Create();
        await File.WriteAllTextAsync(directory.GetFullPath($"text.txt"), "");

        await new Func<Task>(() => DependencyScanner.ScanDirectoryAsync(directory.FullPath, new ScannerOptions { DegreeOfParallelism = degreeOfParallelism, Scanners = new[] { new ShouldScanThrowScanner() } }, onDependencyFound: _ => { }))
            .Should().ThrowExactlyAsync<InvalidOperationException>();

        await new Func<Task>(() => DependencyScanner.ScanDirectoryAsync(directory.FullPath, new ScannerOptions { DegreeOfParallelism = degreeOfParallelism, Scanners = new[] { new ScanThrowScanner() } }, onDependencyFound: _ => { }))
            .Should().ThrowExactlyAsync<InvalidOperationException>();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task ReportScanException_IAsyncEnumerable(int degreeOfParallelism)
    {
        await using var directory = TemporaryDirectory.Create();
        await File.WriteAllTextAsync(directory.GetFullPath($"text.txt"), "");

        await new Func<Task>(async () =>
        {
            foreach (var item in await DependencyScanner.ScanDirectoryAsync(directory.FullPath, new ScannerOptions { DegreeOfParallelism = degreeOfParallelism, Scanners = new[] { new ShouldScanThrowScanner() } }))
            {
            }
        }).Should().ThrowExactlyAsync<InvalidOperationException>();

        await new Func<Task>(async () =>
        {
            foreach (var item in await DependencyScanner.ScanDirectoryAsync(directory.FullPath, new ScannerOptions { DegreeOfParallelism = degreeOfParallelism, Scanners = new[] { new ScanThrowScanner() } }))
            {
            }
        }).Should().ThrowExactlyAsync<InvalidOperationException>();
    }

    [Fact]
    public void DefaultScannersIncludeAllScanners()
    {
        var scanners = new ScannerOptions().Scanners.Select(t => t.GetType()).OrderBy(t => t.FullName, StringComparer.Ordinal).ToArray();

        var allScanners = typeof(ScannerOptions).Assembly.GetExportedTypes()
            .Where(type => !type.IsAbstract && type.IsAssignableTo(typeof(DependencyScanner)) && type != typeof(RegexScanner))
            .OrderBy(t => t.FullName, StringComparer.Ordinal)
            .ToArray();

        scanners.Should().NotBeEmpty();
        scanners.Should().BeEquivalentTo(allScanners);
    }

    private sealed class ScanThrowScanner : DependencyScanner
    {
        public override ValueTask ScanAsync(ScanFileContext context)
        {
            throw new InvalidOperationException();
        }

        protected override bool ShouldScanFileCore(CandidateFileContext file) => true;
    }

    private sealed class ShouldScanThrowScanner : DependencyScanner
    {
        public override ValueTask ScanAsync(ScanFileContext context)
        {
            return ValueTask.CompletedTask;
        }

        protected override bool ShouldScanFileCore(CandidateFileContext file) => throw new InvalidOperationException();
    }

    private sealed class InMemoryFileSystem : IFileSystem
    {
        private readonly List<(string Path, byte[] Content)> _files = new();

        public void AddFile(string path, byte[] content)
        {
            _files.Add((path, content));
        }

        public void AddFile(string path, string content)
        {
            _files.Add((path, Encoding.UTF8.GetBytes(content)));
        }

        public Stream OpenRead(string path)
        {
            foreach (var file in _files)
            {
                if (file.Path == path)
                {
                    return new MemoryStream(file.Content);
                }
            }

            throw new FileNotFoundException("File not found", path);
        }

        public IEnumerable<string> GetFiles(string path, string pattern, SearchOption searchOptions) => throw new NotSupportedException();
        public Stream OpenReadWrite(string path) => throw new NotSupportedException();
    }
}
